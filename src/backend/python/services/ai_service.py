import base64
import uuid

from api.models import magnetogram
from common import config
from processor import processor
from repositories import db_connector, magnetogram_repository


class AIService:
    """
    Сервис для работы с магнитограммами
    """

    async def load(
        self,
        magnetogram_model: magnetogram.MagnetogramRequest
    ) -> ...:
        """
        Выполнить логику обработки магнитограммы
        :param magnetogram_model: объект магнитограммы
        :return: данные обработанной магнитограммы
        """

        processor_obj = processor.Processor()
        processor_obj.magnetogram_pickle = base64.b64decode(magnetogram_model.file)
        result = processor_obj.process()
        image = processor_obj.get_processed_image()
        image_den = processor_obj.filter_sorted()

        magnetogram_processed = magnetogram.MagnetogramProcessed(
            id=uuid.uuid4(),
            user_name=magnetogram_model.user_name,
            name=magnetogram_model.name,
            file=magnetogram_model.file,
            created_at=magnetogram_model.created_at,
            processed_magnetogram=base64.b64encode(image).decode('utf-8'),
            den_magnetogram=base64.b64encode(image_den).decode('utf-8')
        )

        pool = await db_connector.get_pool(config.config.postgres_dsn)
        repo = magnetogram_repository.MagnetogramRepository(pool)
        commit_id = await repo.create(magnetogram_processed, result.defects, result.structural_units)

        await pool.close()

        return commit_id

    async def get_commit(self, commit_id: uuid.UUID) -> magnetogram.Commit:
        """
        Выполнить логику обработки магнитограммы
        :param commit_id: идентификатор коммита
        :return: данные обработанной магнитограммы
        """

        pool = await db_connector.get_pool(config.config.postgres_dsn)
        repo = magnetogram_repository.MagnetogramRepository(pool)
        result = await repo.retrieve(commit_id)

        await pool.close()

        return result

    async def update(self, commit_data: magnetogram.CommitEdit) -> None:
        """
        Редактировать данные о коммите
        :param commit_data: данные о коммите
        """

        pool = await db_connector.get_pool(config.config.postgres_dsn)
        repo = magnetogram_repository.MagnetogramRepository(pool)
        await repo.update(commit_data)

        await pool.close()
