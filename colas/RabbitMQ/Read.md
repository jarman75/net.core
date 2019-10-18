#RabbitMQ

Agente de mensajería por colas.

##Enlaces



Tutorial .net =>
[https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html "Tutorial .net")

Docker Image => [https://hub.docker.com/_/rabbitmq/](https://hub.docker.com/_/rabbitmq/ "Docker image")


##Instrcciones
Levantar servidor de colas con docker:

	en host local:	
	docker run -p 5672:5672 --hostname my-rabbit --name some-rabbit rabbitmq:3

	con manager:  
	docker run -p 5672:5672 -p 15672:15672 --hostname my-rabbit --name some-rabbit rabbitmq:3-management

	localhost:15672
	user: guest
	password: guest

