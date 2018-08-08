## Matrix

This project is an attempt to create message-oriented microservices sans containers with boilerplate modules.

Garrett of [NewStack](https://thenewstack.io) says [that](https://thenewstack.io/miniservices-a-realistic-alternative-to-microservices/), "In a true microservices architecture, each service should have zero awareness of the services around it and, in order to achieve that, a very particular communication pattern must exist of publish-subscribe, published in a messaging queue so other people can retrieve them, the loosest coupling". In order to facilitate better loose coupling, Matrix exposes its microservices are AMQP endpoints using RabbitMQ.