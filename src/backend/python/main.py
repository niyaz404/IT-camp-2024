from fastapi import FastAPI
import uvicorn

from common import config
from api import router_registrator

app_config = config.config

app = FastAPI()
router_registrator.register_routers(app)


if __name__ == "__main__":
    uvicorn.run(app, host=app_config.host, port=app_config.port)
