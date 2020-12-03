##Crear imagen "myimage"
docker build -t myimage -f Dockerfile .

##Crear contenedor desde imagen
docker create myimage

##Ver todas las imagenes docker
docker images
##Ver todos los contenedores docker
docker ps -a
##Ver contenedores activos
docker ps

##iniciar contenedor
docker start [container_name]

##Parar contenedor
docker stop [container_name]

##Conectar a contenedor
docker attach --sig-proxy=false [container_name]

##Eliminar contenedor
docker rm [container_name]

##Ejecución única
docker run -it --rm [image_name]

-crea contenedor desde imagen, lo ejecuta y al pararlo lo elimina

##eliminar imagen
docker rmi [imagen_id]

