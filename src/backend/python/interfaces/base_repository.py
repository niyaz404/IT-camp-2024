import abc


class AbstractRepository(abc.ABC):
    """
    Абстрактный класс репозитория
    """

    @abc.abstractmethod
    def create(self, *args, **kwargs) -> any:
        """
        Создать записи
        """

        raise NotImplementedError

    @abc.abstractmethod
    def retrieve(self, *args, **kwargs) -> any:
        """
        Получить записи
        """

        raise NotImplementedError

    @abc.abstractmethod
    def update(self, *args, **kwargs) -> any:
        """
        Обновить записи
        """

        raise NotImplementedError

    @abc.abstractmethod
    def delete(self, *args, **kwargs) -> any:
        """
        Удалить записи
        """

        raise NotImplementedError
