import uuid

from pydantic import Field

from interfaces import base_model


class Defect(base_model.BaseModel):
    """
    Модель дефекта
    """

    id: uuid.UUID = Field(description="Идентификатор дефекта")
    description: str | None = Field(description="Комментарий", default=None)
    x: int = Field(description="Координата по оси X")


class StructuralUnit(base_model.BaseModel):
    """
    Модель структурного элемента
    """

    id: uuid.UUID = Field(description="Идентификатор структурного элемента")
    type_id: int = Field(description="Идентификатор структурного элемента")
    description: str | None = Field(description="Комментарий", default=None)
    x: int = Field(description="Координата по оси X")
    name: str | None = Field(description="Название типа структурного элемента", default=None)


class ProcessorDto(base_model.BaseModel):
    """
    Модель выходных данных, получаемых из обработчика магнитограммы
    """

    defects: list[Defect] = Field(description="Список дефектов")
    structural_units: list[StructuralUnit] = Field(description="Список структурных элементов")
