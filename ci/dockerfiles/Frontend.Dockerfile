FROM node:latest as build-stage
WORKDIR /app

COPY ["src/frontend/package.json", "src/frontend/package-lock.json", "./"]

RUN npm install
COPY src/frontend/ .

RUN npm run build
FROM nginx:stable-alpine3.20-perl

COPY --from=build-stage /app/dist /usr/share/nginx/html

COPY src/frontend/nginx/nginx.conf /etc/nginx/conf.d/default.conf

CMD ["nginx", "-g", "daemon off;"]