#!/bin/bash
set -e

APP_NAME="$1"
TAG="$2"
IMAGE="registry.mait.gq/${APP_NAME}:${TAG}"

# Crear o usar el builder existente
if ! docker buildx inspect mybuilder > /dev/null 2>&1; then
    docker buildx create --use --name mybuilder
else
    docker buildx use mybuilder
fi

# Construir imagen de Docker (las dependencias y licencia se manejan dentro del contenedor)
docker buildx inspect --bootstrap
docker buildx build --platform linux/amd64,linux/arm64 --push -t $IMAGE ./src/mait-apv/

# Verificar si la construcción fue exitosa
if [ $? -ne 0 ]; then
    echo "Error al construir la imagen de Docker"
    exit 1
fi

echo "Imagen de Docker construida y subida exitosamente: $IMAGE"
