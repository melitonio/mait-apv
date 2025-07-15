docker buildx create --use --name multiplatform-builder
#docker buildx inspect --bootstrap

docker buildx build ./src/mait-apv/ --platform linux/amd64 -t registry.mait.gq/apv:latest --push
