import base64
import io
import pickle
import uuid
import torch
import torch.nn as nn
import os

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

                # Одномерные свертки по оси X (временная ось)/
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

        defects = [
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Вау, дефект",
                startx=1,
                endx=2
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Еще дефект",
                startx=1,
                endx=2
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                startx=1,
                endx=2
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Пупупу...",
                startx=1,
                endx=2
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                startx=1,
                endx=2
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Дефектик",
                startx=1,
                endx=2
            )
        ]

        structural_units = [
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.JOINT.id,
                startx=5,
                endx=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.JOINT.id,
                startx=5,
                endx=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                startx=5,
                endx=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                startx=5,
                endx=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                startx=5,
                endx=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.BRANCHING.id,
                startx=5,
                endx=6
            )
        ]

        formatted_defects = self._truncate(defects)
        formatted_structured_units = self._truncate(structural_units)

        return ai_dto.ProcessorDto(
            defects=formatted_defects,
            structural_units=formatted_structured_units
        )

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
        input_image = image.clone().detach().to(torch.float32).to(device)
        input_image = input_image.unsqueeze(0)

        self.model.eval()
        with torch.inference_mode():
            output, defects = self.model(input_image)  # torch.Size([1, 4096, 5]) torch.Size([1, 4096, 2])

        ### обработать предсказания

        def to_one_hot(labels, num_classes=5):
            labels = labels.cpu()  
            one_hot_labels = np.zeros((labels.size(0), num_classes), dtype=np.uint8)
            one_hot_labels[np.arange(labels.size(0)), labels.numpy()] = 1
            return one_hot_labels

        predicted_labels = torch.argmax(output, dim=-1)  # Размер станет [1, 4096]
        predicted_labels = predicted_labels.squeeze(0)  # Размер станет [4096]

        if predicted_labels.is_cuda:
            predicted_labels = predicted_labels.cpu()

        predicted_one_hot = to_one_hot(predicted_labels.squeeze(0))

        predicted_defects = torch.argmax(defects, dim=-1)  # Размер станет [1, 4096]
        predicted_defects = predicted_defects.squeeze(0)  # Размер станет [4096]

        if predicted_defects.is_cuda:
            predicted_defects = predicted_defects.cpu()

        predicted_one_hot_defects = to_one_hot(predicted_defects.squeeze(0), num_classes=2)
        print(predicted_one_hot_defects)
        print(predicted_one_hot)
        print(predicted_one_hot_defects.shape)


        ##################################
        image_array = (image_array * 255).astype(np.uint8)
        image = Image.fromarray(image_array)
        buffered_image = io.BytesIO()
        image.save(buffered_image, format="PNG")
        image_byte = buffered_image.getvalue()

        return base64.b64encode(image_byte)

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
