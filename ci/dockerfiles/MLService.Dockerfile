FROM python:3.12-slim

RUN apt-get update && apt-get install ffmpeg libsm6 libxext6  -y

WORKDIR /app

COPY /src/backend/python/requirements.txt ./

RUN pip install -r requirements.txt

COPY /src/backend/python/ ./

ARG POSTGRES_USER
ARG POSTGRES_PASSWORD
ARG POSTGRES_DB
ARG POSTGRES_HOST
ARG POSTGRES_PORT

ENV PG_USERNAME=$POSTGRES_USER
ENV PG_PASSWORD=$POSTGRES_PASSWORD
ENV PG_DB_NAME=$POSTGRES_DB
ENV PG_HOST=$POSTGRES_HOST
ENV PG_PORT=$POSTGRES_PORT

EXPOSE 8080

CMD ["python3", "-u", "main.py"]
