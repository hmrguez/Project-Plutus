build:
    docker build --rm -t zealotalgo/project-plutus:latest .

run:
    docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 zealotalgo/project-plutus
