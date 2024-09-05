import base64
import datetime
import uuid

from pydantic import field_validator, Field

from interfaces import base_model


class Defect(base_model.BaseModel):
    """
    Модель дефекта
    """

    description: str = Field(description="Описание дефекта")
    startx: int = Field(description="Координата по оси X(нач)")    
    endx: int = Field(description="Координата по оси X(кон)")


class DefectRequest(Defect):
    """
    Модель дефекта для запроса
    """

    pass


class DefectResponse(Defect):
    """
    Модель дефекта для запроса
    """

    id: uuid.UUID = Field(description="Идентификатор дефекта")


class StructuralUnit(base_model.BaseModel):
    """
    Модель структурного элемента
    """

    type_id: int = Field(description="Идентификатор типа структурного элемента", alias="typeId")
    startx: int = Field(description="Координата по оси X(нач)", alias="startx")
    endx: int = Field(description="Координата по оси X(конч)", alias="endx")


class StructuralUnitRequest(StructuralUnit):
    """
    Модель структурного элемента для запроса
    """

    pass


class StructuralUnitResponse(StructuralUnit):
    """
    Модель структурного элемента для ответа
    """

    id: uuid.UUID = Field(description="Идентификатор дефекта")
    type_name: str = Field(description="Название типа структурного элемента", alias="typeName")


class Magnetogram(base_model.BaseModel):
    """
    Модель для магнитограммы
    """

    id: uuid.UUID | None = Field(description="Идентификатор магнитограммы", default=None)
    user_name: str = Field(description="Имя пользователя", alias="userName")
    name: str = Field(description="Название файла")
    file: bytes = Field(description="Магнитограмма в формате pickle")
    created_at: datetime.datetime = Field(
        description="Дата создания магнитограммы",
        alias="createdAt",
        default=datetime.datetime.now()
    )


class MagnetogramRequest(Magnetogram):
    """
    Модель для загрузки магнитограммы
    """

    pass


class MagnetogramProcessed(Magnetogram):
    """
    Модель для магнитограммы
    """

    processed_magnetogram: bytes = Field(description="Магнитограмма в формате png")

    @field_validator("processed_magnetogram", mode="after")
    @classmethod
    def convert_magnetogram_to_base64(cls, magnetogram: bytes) -> str:
        """
        Конвертировать магнитограмму в формате байтовой строки в base64
        :param magnetogram: магнитограмма в формате байтовой строки
        :return: магнитограмма в формате base64
        """

        return magnetogram.decode("utf-8")


class Commit(base_model.BaseModel):
    """
    Модель для коммита
    """

    id: uuid.UUID | None = Field(description="Идентификатор коммита", default=None)
    user_name: str = Field(description="Имя пользователя", alias="createdBy")
    name: str = Field(description="Название файла")
    processed_magnetogram: bytes = Field(description="Магнитограмма в формате png", alias="proccessedMagnetogram")
    created_at: datetime.datetime = Field(
        description="Дата создания магнитограммы",
        alias="createdAt"
    )
    defects: list[DefectResponse] = Field(description="Список дефектов")
    structural_units: list[StructuralUnitResponse] = Field(
        description="Список структруных элементов",
        alias="structuralElements"
    )

    @field_validator("processed_magnetogram", mode="after")
    @classmethod
    def convert_magnetogram_to_base64(cls, magnetogram: bytes) -> bytes:
        """
        Конвертировать магнитограмму в формате байтовой строки в base64
        :param magnetogram: магнитограмма в формате байтовой строки
        :return: магнитограмма в формате base64
        """

        return base64.b64decode(base64.b64encode(magnetogram))


class CommitEdit(base_model.BaseModel):
    """
    Модель для редактирования коммита
    """

    magnetogram_id: uuid.UUID | None = Field(description="Идентификатор магнитограммы", alias="magnetogramId")
    user_name: str = Field(description="Имя пользователя", alias="createdBy")
    defects: list[DefectRequest] = Field(description="Список дефектов")
    structural_units: list[StructuralUnitRequest] = Field(
        description="Список структруных элементов",
        alias="structuralElements"
    )
    created_at: datetime.datetime = Field(
        description="Дата создания магнитограммы",
        alias="createdAt"
    )
