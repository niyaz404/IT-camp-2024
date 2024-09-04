from pydantic import BaseModel, ConfigDict


class PydanticBase(BaseModel):
    """
    Базовая модель для моделей pydantic
    """

    model_config = ConfigDict(arbitrary_types_allowed=True)


class BaseModel(PydanticBase):
    """
    Базовый класс для моделей
    """

    class Config:
        """
        Класс конфига
        """

        populate_by_name = True
