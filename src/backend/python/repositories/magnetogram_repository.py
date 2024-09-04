import uuid

import asyncpg

from api.models import magnetogram
from common import ai_dto
from interfaces import base_repository


class MagnetogramRepository(base_repository.AbstractRepository):
    """
    Репозиторий Postgres
    """

    def __init__(self, pool: asyncpg.Pool) -> None:
        """
        Инициализировать переменные
        :param pool: пул
        """

        self.pool = pool

    async def create(
        self,
        magnetogram_model: magnetogram.MagnetogramProcessed,
        defects: list[ai_dto.Defect],
        structural_elements: list[ai_dto.StructuralUnit]
    ) -> uuid.UUID:
        """
        Создать запись магнитограммы
        :param magnetogram_model: модель магнитограммы
        :param defects: дефекты
        :param structural_elements: структруные элементы
        :return: идентификатор коммита
        """

        commit_id = uuid.uuid4()

        insert_magnetogram_query = """
            INSERT INTO magnetogram (id, name, objectname, file, createdat, createdby, processedmagnetogram)
            VALUES ($1, $2, $3, $4, $5, $6, $7)
        """
        insert_commit_query = """
            INSERT INTO commit (id, magnetogramid, createdat, createdby)
            VALUES ($1, $2, $3, $4)
        """
        insert_defects_query = """
            INSERT INTO defect (id, description, x)
            VALUES ($1, $2, $3)
        """
        insert_structural_elements_query = """
            INSERT INTO structural_element (id, typeid, description, x)
            VALUES ($1, $2, $3, $4)
        """
        insert_structural_elements_commit_query = """
            INSERT INTO structural_element_commit (id, commitid, structuralelementid)
            VALUES ($1, $2, $3)
        """
        insert_defects_commit_query = """
            INSERT INTO defect_commit (id, commitid, defectid)
            VALUES ($1, $2, $3)
        """

        defect_params = [
            (uuid.uuid4(), defect.description, defect.x) for defect in defects
        ]
        structural_element_params = [
            (
                uuid.uuid4(),
                structural_element.type_id,
                structural_element.description,
                structural_element.x
            ) for structural_element in structural_elements
        ]
        structural_elements_commit_params = [
            (
                uuid.uuid4(),
                commit_id,
                structural_element[0]
            ) for structural_element in structural_element_params
        ]
        defects_commit_params = [
            (
                uuid.uuid4(),
                commit_id,
                defect[0]
            ) for defect in defect_params
        ]

        async with self.pool.acquire() as connection:
            async with connection.transaction():
                await connection.execute(
                    insert_magnetogram_query,
                    magnetogram_model.id,
                    magnetogram_model.name,
                    magnetogram_model.object_name,
                    magnetogram_model.file,
                    magnetogram_model.created_at,
                    magnetogram_model.user_name,
                    magnetogram_model.processed_magnetogram
                )

                await connection.execute(
                    insert_commit_query,
                    commit_id,
                    magnetogram_model.id,
                    magnetogram_model.created_at,
                    magnetogram_model.user_name
                )

                await connection.executemany(
                    insert_defects_query,
                    defect_params
                )

                await connection.executemany(
                    insert_structural_elements_query,
                    structural_element_params
                )

                await connection.executemany(
                    insert_structural_elements_commit_query,
                    structural_elements_commit_params
                )

                await connection.executemany(
                    insert_defects_commit_query,
                    defects_commit_params
                )

        return commit_id

    async def retrieve(self, commit_id: uuid.UUID) -> magnetogram.Commit:
        """
        Получить данные о коммите
        :param commit_id: идентификатор коммита
        :return: данные о коммите
        """

        query = """
            SELECT c.id,
                c.createdat,
                c.createdby,
                c.magnetogramid,
                m.objectname,
                m.name file_name,
                m.processedmagnetogram,
                d.id defect_id,
                d.description defect_description,
                d.x defect_x,
                se.id structural_element_id,
                se.description structural_element_description,
                se.x structural_element_x,
                se.typeid,
                set.name
            FROM commit c
            JOIN magnetogram m
                ON c.magnetogramid = m.id
            JOIN defect_commit dc
                ON dc.commitid = c.id
            JOIN structural_element_commit sec
                ON sec.commitid = c.id
            JOIN defect d
                ON dc.defectid = d.id
            JOIN structural_element se
                ON sec.structuralelementid = se.id
            JOIN structural_element_type set
                ON set.id = se.typeid
            WHERE c.id = $1
        """

        async with self.pool.acquire() as connection:
            rows = await connection.fetch(query, commit_id)

        unique_defects = dict()
        unique_structural_elements = dict()

        for row in rows:
            unique_defects[row["defect_id"]] = unique_defects.get(
                row["defect_id"],
                magnetogram.DefectResponse(
                    id=row["defect_id"],
                    description=row["defect_description"],
                    x=row["defect_x"]
                )
            )
            unique_structural_elements[row["structural_element_id"]] = unique_structural_elements.get(
                row["structural_element_id"],
                magnetogram.StructuralUnitResponse(
                    id=row["structural_element_id"],
                    description=row["structural_element_description"],
                    x=row["structural_element_x"],
                    type_id=row["typeid"],
                    type_name=row["name"]
                )
            )

        return magnetogram.Commit(
            id=rows[0]["id"],
            user_name=rows[0]["createdby"],
            name=rows[0]["file_name"],
            object_name=rows[0]["objectname"],
            processed_magnetogram=rows[0]["processedmagnetogram"],
            created_at=rows[0]["createdat"],
            defects=list(unique_defects.values()),
            structural_units=list(unique_structural_elements.values())
        )

    async def update(self, commit_data: magnetogram.CommitEdit) -> None:
        """
        Редактировать данные о коммите
        :param commit_data: данные о коммите
        """

        commit_id = uuid.uuid4()

        insert_commit_query = """
            INSERT INTO commit (id, magnetogramid, createdat, createdby)
            VALUES ($1, $2, $3, $4)
        """
        insert_defects_query = """
            INSERT INTO defect (id, description, x)
            VALUES ($1, $2, $3)
        """
        insert_structural_elements_query = """
            INSERT INTO structural_element (id, typeid, description, x)
            VALUES ($1, $2, $3, $4)
        """
        insert_structural_elements_commit_query = """
            INSERT INTO structural_element_commit (id, commitid, structuralelementid)
            VALUES ($1, $2, $3)
        """
        insert_defects_commit_query = """
            INSERT INTO defect_commit (id, commitid, defectid)
            VALUES ($1, $2, $3)
        """

        defect_params = [
            (uuid.uuid4(), defect.description, defect.x) for defect in commit_data.defects
        ]
        structural_element_params = [
            (
                uuid.uuid4(),
                structural_element.type_id,
                structural_element.description,
                structural_element.x
            ) for structural_element in commit_data.structural_units
        ]
        structural_elements_commit_params = [
            (
                uuid.uuid4(),
                commit_id,
                structural_element[0]
            ) for structural_element in structural_element_params
        ]
        defects_commit_params = [
            (
                uuid.uuid4(),
                commit_id,
                defect[0]
            ) for defect in defect_params
        ]

        async with self.pool.acquire() as connection:
            async with connection.transaction():
                await connection.execute(
                    insert_commit_query,
                    commit_id,
                    commit_data.magnetogram_id,
                    commit_data.created_at,
                    commit_data.user_name
                )

                await connection.executemany(
                    insert_defects_query,
                    defect_params
                )

                await connection.executemany(
                    insert_structural_elements_query,
                    structural_element_params
                )

                await connection.executemany(
                    insert_structural_elements_commit_query,
                    structural_elements_commit_params
                )

                await connection.executemany(
                    insert_defects_commit_query,
                    defects_commit_params
                )

    async def delete(self, *args, **kwargs) -> any:
        super().delete(*args, **kwargs)
