#!/bin/bash
set -e

APP_NAME="$1"
TAG="$2"

IMAGE="registry.mait.gq/${APP_NAME}:${TAG}"
CONTEXT_DIR="./src/mait-apv/"

# Crear o usar el builder existente
# if ! docker buildx inspect mybuilder > /dev/null 2>&1; then
#     docker buildx create --use --name mybuilder
# else
#     docker buildx use mybuilder
# fi

echo "🔧 Configurando builder con red mejorada..."
# Eliminar builder existente si hay problemas
docker buildx rm mybuilder 2>/dev/null || true

# Crear builder con configuración de red optimizada
docker buildx create \
    --use \
    --name mybuilder \
    --driver docker-container \
    --buildkitd-flags '--allow-insecure-entitlement network.host' \
    --driver-opt network=host

echo "🚀 Iniciando construcción de imagen multiplataforma..."
docker buildx inspect --bootstrap

echo "📦 Construyendo para AMD64 y ARM64 con configuración de red optimizada..."

docker buildx build \
        --platform linux/amd64,linux/arm64 \
        --push \
        --progress=plain \
        --network=host \
        --build-arg BUILDKIT_INLINE_CACHE=1 \
        -t $IMAGE \
        $CONTEXT_DIR

# Verificar si la construcción fue exitosa
if [ $? -ne 0 ]; then
    echo "Error al construir la imagen de Docker"
    exit 1
fi

echo "✅ Imagen multiplataforma construida y publicada:"
echo "   - ${IMAGE}"
echo "   - Plataformas: linux/amd64, linux/arm64"

echo "🎉 Proceso completado exitosamente!"