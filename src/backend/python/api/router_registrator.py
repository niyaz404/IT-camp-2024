from fastapi import FastAPI

from api import ai_entrypoint


def register_routers(app: FastAPI) -> None:
    """
    Зарегистрировать роутеры
    :param app: приложение FastAPI
    """

    app.include_router(ai_entrypoint.router)
