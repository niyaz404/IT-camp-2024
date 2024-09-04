import asyncpg
from pydantic import PostgresDsn


async def get_pool(dsn: PostgresDsn) -> asyncpg.Pool:
    """
    Создать пул соединений
    :param dsn: postgres dsn
    :return: пул
    """

    return await asyncpg.create_pool(str(dsn))
