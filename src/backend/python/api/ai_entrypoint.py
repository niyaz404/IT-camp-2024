import uuid

from fastapi import status, APIRouter
from fastapi.responses import JSONResponse

from services import ai_service
from api.models import magnetogram as m_model

router = APIRouter(prefix="/ai")


@router.post("/load")
async def upload_magnetogram(
    magnetogram: m_model.MagnetogramRequest,
) -> JSONResponse:
    """
    Загрузить магнитограмму
    :param magnetogram: данные о магнитограмме
    :return: обработанная магнитограмма
    """

    service = ai_service.AIService()
    response = await service.load(magnetogram)

    return JSONResponse(
        status_code=status.HTTP_201_CREATED,
        content={
            "commitId": str(response)
        }
    )


@router.get("/commits/{commit_id}")
async def get_commit(
    commit_id: uuid.UUID,
) -> m_model.Commit:
    """
    Получить коммит
    :param commit_id: идентификатор коммита
    :return: данные обработанной магнитограммы
    """

    service = ai_service.AIService()

    return await service.get_commit(commit_id)


@router.post("/edit", status_code=status.HTTP_201_CREATED)
async def edit(
    edit_data: m_model.CommitEdit
) -> None:
    """
    Редактировать магнитограмму
    :param edit_data: данные для обновления
    """

    service = ai_service.AIService()

    await service.update(edit_data)
