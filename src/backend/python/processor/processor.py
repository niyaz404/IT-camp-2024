import base64
import io
import pickle
import uuid
import torch
import torch.nn as nn
import os
import matplotlib.pyplot as plt
import matplotlib.cm as cm

import pywt
from scipy.ndimage import gaussian_filter
from scipy.signal import correlate
from scipy.fft import fft2, ifft2, fftshift, ifftshift

import numpy as np
from PIL import Image

from common import ai_dto, enums
from interfaces import base_processor


class Processor(base_processor.AbstractProcessor):
    """
    Обрабтчик магнитограмм
    """

    def __init__(self) -> None:
        """
        Инициализировать переменные
        """
        self.model: None
        self._magnetogram_pickle: bytes | None = None

    def _get_array_from_pickle(self) -> np.array:
        """
        Конвертировать магнитограмму в файле pickle в массив numpy
        :return: массив numpy
        """

        return pickle.load(io.BytesIO(self.magnetogram_pickle))

    def _truncate(
        self,
        elements: list[ai_dto.Defect | ai_dto.StructuralUnit]
    ) -> list[ai_dto.Defect | ai_dto.StructuralUnit]:
        """
        Отформатировать результат обработчика
        :param elements: список дефектов или структурных элементов
        :return: отформатированный результат обработчика
        """

        def _check_elements(
            element: ai_dto.Defect | ai_dto.StructuralUnit,
            index: int
        ) -> bool:
            """
            Проверить условие включения элементов в список
            :param element: дефект или структурный элемент
            :param index: индекс следующего элемента
            :return: True, если элемент удовлетворяет условиям, иначе - False
            """

            if isinstance(element, ai_dto.Defect):
                return elements[index].startx - 1 == elements[index - 1].startx

            return (
                elements[index].startx - 1 == elements[index - 1].startx
                and elements[index].type_id == elements[index - 1].type_id
            )

        formatted = []

        if not elements:
            return []

        left_index = 0
        right_index = left_index

        while left_index < len(elements):
            current_element = elements[left_index]
            right_index += 1

            for cursor in range(right_index, len(elements)):
                if _check_elements(elements[right_index], right_index):
                    right_index += 1
                else:
                    break

            current_element.startx = (elements[right_index - 1].startx - elements[left_index].startx) // 2 + elements[left_index].startx
            left_index = right_index
            formatted.append(current_element)

        return formatted

    def process(self) -> ai_dto.ProcessorDto:
        """
        Обработать магнитограмму
        :return: списки дефектов и структурных элементов
        """

        # Класс ----------------------------------------------------------------------------
        class MultiTaskModel(nn.Module):
            def __init__(self):
                super(MultiTaskModel, self).__init__()
                # Циклический паддинг по вертикальной оси Y (цикл)
                self.cyclic_padding = nn.ReflectionPad2d((0, 0, 1, 1))

                # Одномерные свертки по оси X (временна���� ось)/
                self.conv1d_1 = nn.Conv1d(130, 64, kernel_size=3, padding=1)  # Увеличили размер входных каналов
                self.conv1d_2 = nn.Conv1d(64, 128, kernel_size=3, padding=1)
                self.conv1d_3 = nn.Conv1d(128, 128, kernel_size=3, padding=1)

                # BatchNorm и Dropout
                self.bn1 = nn.BatchNorm1d(64)
                self.bn2 = nn.BatchNorm1d(128)
                self.dropout = nn.Dropout(p=0.3)
                
                self.lstm = nn.LSTM(128, 128, batch_first=True, bidirectional=True)

                self.fc = nn.Linear(128 * 2, 5)
                self.fc_defects = nn.Linear(128 * 2, 2)
                
                self.elu = nn.ELU()

            def forward(self, x):
                x = self.cyclic_padding(x)  # [batch_size, 1, 130, 4096]
                
                x = x.view(x.size(0), 130, -1)  # [batch_size, 130, 4096]
                
                # Одномерные свертки с активацией и нормализацией
                x = self.elu(self.bn1(self.conv1d_1(x)))  # [batch_size, 64, 4096]
                x = self.elu(self.bn2(self.conv1d_2(x)))  # [batch_size, 128, 4096]
                x = self.elu(self.conv1d_3(x))            # [batch_size, 128, 4096]
                
                # Применение Dropout
                x = self.dropout(x)  # batch_size, 128, 4096]
                
                x, _ = self.lstm(x.permute(0, 2, 1))  # [batch_size, 4096, 256]
                
                out_elements = self.fc(x)
                out_defects = self.fc_defects(x)  
                
                return out_elements, out_defects # [batch_size, 4096, 5]
        # ------------------------------------------------------------------------------------

        model_path = os.path.join(os.path.dirname(__file__), "model_photonchikk.pth")
        self.model = MultiTaskModel()
        self.model.load_state_dict(torch.load(model_path, map_location = torch.device('cpu')))
        self.model.eval()
        print('Модель загружена и готова к предикту модели')

        self.get_processed_image()

        # input_tensor = torch.tensor()
        mag = self._get_array_from_pickle()
        if mag.shape[1] > 4096:
            mag = mag[:, :4096]
        
        mag_den = self.filter_sorted()

        image = mag[np.newaxis, :, :]  # (1, 128, 4096)
        image = image.astype(np.float32)
        image = torch.tensor(image)
        
        ### предикт
        input_image = image.clone().detach().to(torch.float32).to('cpu')
        input_image = input_image.unsqueeze(0)

        self.model.eval()
        with torch.inference_mode():
            output, defects = self.model(input_image)  # torch.Size([1, 4096, 5]) torch.Size([1, 4096, 2])

        ### обработать предсказания

        predicted_labels = torch.argmax(output, dim=-1)  # Размер станет [1, 4096]
        predicted_labels = predicted_labels.squeeze(0)  # Размер станет [4096]

        if predicted_labels.is_cuda:
            predicted_labels = predicted_labels.cpu()

        print(predicted_labels)


        predicted_defects = torch.argmax(defects, dim=-1)  # Размер станет [1, 4096]
        predicted_defects = predicted_defects.squeeze(0)  # Размер станет [4096]

        if predicted_defects.is_cuda:
            predicted_defects = predicted_defects.cpu()
        
        indices = torch.where(predicted_defects == 1)[0]

        if len(indices) > 0:
            # Найти блоки 1: начало и конец
            blocks = []
            start = indices[0]  # Начальный индекс блока
            for i in range(1, len(indices)):
                if indices[i] != indices[i-1] + 1:  # Если не подряд, то конец блока
                    end = indices[i-1]
                    blocks.append((start.item(), end.item()))
                    start = indices[i]  # Новый старт следующего блока
            blocks.append((start.item(), indices[-1].item()))  # Добавляем последний блок
        
        defects = []
        for i in range(0, len(blocks)):
            start,end = blocks[i]
            defects.append(ai_dto.Defect(
                id=uuid.uuid4(),
                description=f"Дефект {i}",
                startx = start,
                endx = end
            ))


        ##########################################################################################################
        # Находим уникальные метки (0, 1, 2, 3, 4)
        unique_labels = torch.unique(predicted_labels)
        print("Уникальные предсказанные метки:", unique_labels.cpu().numpy())

        # Для каждого уникального значения ищем блоки
        blocks = {}

        for label in unique_labels:
            # Находим индексы всех элементов, где predicted_labels == label
            indices = torch.where(predicted_labels == label)[0]

            if len(indices) > 0:
                label_blocks = []
                start = indices[0]  # Начальный индекс блока
                for i in range(1, len(indices)):
                    if indices[i] != indices[i-1] + 1:  # Если не подряд, то конец блока
                        end = indices[i-1]
                        label_blocks.append((start.item(), end.item()))
                        start = indices[i]  # Новый старт следующего блока
                label_blocks.append((start.item(), indices[-1].item()))  # Добавляем последний блок
                
                blocks[label.item()] = label_blocks

        # Вывод блоков для каждого уникального значения
        structural_units = []
        for label, label_blocks in blocks.items():
            if label != 0:
                for element in label_blocks:
                    print(element)
                    start,end = element
                    structural_units.append(ai_dto.StructuralUnit(
                        id=uuid.uuid4(),
                        type_id=label,
                        startx=start,
                        endx=end
                    ))

        formatted_defects = self._truncate(defects)
        formatted_structured_units = self._truncate(structural_units)

        return ai_dto.ProcessorDto(
            defects=formatted_defects,
            structural_units=formatted_structured_units
        )


    # Вейвлет-фильтрация
    def wavelet_filtering(self, data, wavelet='db4', level=1):
        coeffs = pywt.wavedec2(data, wavelet, mode='periodization', level=level)
        
        coeffs_filtered = [coeffs[0]]  # Сохраняем только аппроксимацию
        coeffs_filtered += [tuple(np.zeros_like(detail) for detail in details) for details in coeffs[1:]]

        data_filtered = pywt.waverec2(coeffs_filtered, wavelet, mode='periodization')
        
        return data_filtered

    def estimate_y_shift(self, row1, row2):
        correlation = correlate(row1, row2, mode='full')
        displacement = np.argmax(correlation) - (row1.size - 1)
        return displacement

    def correct_rotation(self, data):
        corrected_data = np.copy(data)
        shifts = []

        for i in range(1, corrected_data.shape[0]):
            shift = self.estimate_y_shift(corrected_data[i - 1], corrected_data[i])
            shifts.append(shift)
            corrected_data[i] = np.roll(corrected_data[i], -shift)

        return corrected_data, shifts

    def remove_periodic_noise(self, data, cutoff_radius=150):
        # Прямое Фурье-преобразование
        data_fft = fft2(data)
        data_fft_shifted = fftshift(data_fft)
        
        # Создание маски для удаления низкочастотных шумов
        rows, cols = data.shape
        crow, ccol = rows // 2, cols // 2
        mask = np.ones((rows, cols), np.uint8)
        
        for u in range(rows):
            for v in range(cols):
                distance = np.sqrt((u - crow) ** 2 + (v - ccol) ** 2)
                if distance < cutoff_radius:
                    mask[u, v] = 0
        
        # Применение маски к данным
        data_fft_shifted_filtered = data_fft_shifted * mask
        
        # Обратное Фурье-преобразование
        data_filtered = ifft2(ifftshift(data_fft_shifted_filtered))
        
        # Преобразование в реальное изображение
        data_filtered_real = np.real(data_filtered)
        
        return data_filtered_real

    def filter_sorted(self, **kwargs) -> bytes:
        # if isinstance(magnetogram_data, np.ndarray):
        data = self._get_array_from_pickle()
        if data.shape[1] > 4096:
            data = data[:, :4096]

        blurred_data = np.array([gaussian_filter(row, sigma=2) for row in data])

        wavelet = 'db4' 
        level = 1  
        filtered_data = self.wavelet_filtering(blurred_data, wavelet, level)
        corrected_data, estimated_shifts = self.correct_rotation(filtered_data)
        final_data = (data-self.remove_periodic_noise(corrected_data))
        mag_normalized = (final_data - np.min(final_data)) / (np.max(final_data) - np.min(final_data))  # Нормализация
        colored_mag = cm.viridis(mag_normalized)  # Применение цветовой карты
    
        # Преобразование в изображение
        colored_mag = (colored_mag[:, :, :3] * 255).astype(np.uint8)  # Удаление альфа-канала и преобразование в uint8
        image = Image.fromarray(colored_mag)
        buffered_image = io.BytesIO()
        image.save(buffered_image, format="PNG")
        image_byte = buffered_image.getvalue()

        return image_byte

    def get_processed_image(self) -> bytes:
        """
        Конвертировать массив из файла pickle в изображение
        :return: изображение в формате base64
        """

        mag = self._get_array_from_pickle()
        if mag.shape[1] > 4096:
            mag = mag[:, :4096]
            
        image = mag[np.newaxis, :, :]  # (1, 128, 4096)
        image = image.astype(np.float32)
        image = torch.tensor(image)
        
        ### предикт
        input_image = image.clone().detach().to(torch.float32).to('cpu')
        input_image = input_image.unsqueeze(0)

        ##################################
        mag_normalized = (mag - np.min(mag)) / (np.max(mag) - np.min(mag))  # Нормализация
        colored_mag = cm.viridis(mag_normalized)  # Применение цветовой карты
    
        # Преобразование в изображение
        colored_mag = (colored_mag[:, :, :3] * 255).astype(np.uint8)  # Удаление альфа-канала и преобразование в uint8
        image = Image.fromarray(colored_mag)
        buffered_image = io.BytesIO()
        image.save(buffered_image, format="PNG")
        image_byte = buffered_image.getvalue()

        return image_byte

    @property
    def magnetogram_pickle(self) -> bytes:
        """
        Свойство magnetogram_pickle
        :return: магнитограмма в формате pickle
        """

        return self._magnetogram_pickle

    @magnetogram_pickle.setter
    def magnetogram_pickle(self, value: bytes) -> None:
        """
        Установить значение для magnetogram_pickle
        :param value: значение для magnetogram_pickle
        """

        self._magnetogram_pickle = value
