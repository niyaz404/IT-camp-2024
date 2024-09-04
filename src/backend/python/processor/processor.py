import base64
import io
import pickle
import uuid

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
                return elements[index].x - 1 == elements[index - 1].x

            return (
                elements[index].x - 1 == elements[index - 1].x
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

            current_element.x = (elements[right_index - 1].x - elements[left_index].x) // 2 + elements[left_index].x
            left_index = right_index
            formatted.append(current_element)

        return formatted

    def process(self) -> ai_dto.ProcessorDto:
        """
        Обработать магнитограмму
        :return: списки дефектов и структурных элементов
        """

        defects = [
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Вау, дефект",
                x=1
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Еще дефект",
                x=5
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                x=6
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Пупупу...",
                x=7
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                x=8
            ),
            ai_dto.Defect(
                id=uuid.uuid4(),
                description="Дефектик",
                x=11
            )
        ]

        structural_units = [
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.JOINT.id,
                description="Соединение",
                x=5
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.JOINT.id,
                description="Чо",
                x=6
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                description="Хз",
                x=8
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                description="Соединение",
                x=9
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.PATCH.id,
                description="Кек",
                x=10
            ),
            ai_dto.StructuralUnit(
                id=uuid.uuid4(),
                type_id=enums.StructuralElement.BRANCHING.id,
                description="Разветвление",
                x=13
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

        image_array = self._get_array_from_pickle()
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
