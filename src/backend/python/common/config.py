import dotenv
from pydantic import computed_field, Field, PostgresDsn
from pydantic_settings import BaseSettings

dotenv.load_dotenv()


class Config(BaseSettings):
    """
    Класс настроек для работы сервиса
    """

    host: str = Field(
        description="Адрес сервиса", default="0.0.0.0"
    )
    port: int = Field(description="Порт сервиса", default=8080)

    postgres_username: str = Field(
        description="Имя пользователя БД Postgres", default="postgres"
    )
    postgres_password: str = Field(
        description="Пароль пользователя БД Postgres", default="password"
    )
    postgres_host: str = Field(
        description="Хост БД Postgres", default="localhost"
    )
    postgres_port: int = Field(
        description="Порт БД Postgres", default=5432
    )
    postgres_database: str = Field(
        description="Название БД Postgres",
        default="postgres"
    )

    @computed_field
    @property
    def postgres_dsn(self) -> PostgresDsn:
        """
        Собрать Postgres DSN
        """

        return PostgresDsn.build(
            scheme="postgresql",
            username=self.postgres_username,
            password=self.postgres_password,
            host=self.postgres_host,
            port=self.postgres_port,
            path=self.postgres_database,
        )


config = Config()
