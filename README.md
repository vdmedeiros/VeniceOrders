# 📦 Introdução e objetivo 
- Desenvolver uma pequena aplicação back-end para gerenciar pedidos de um sistema fictício chamado Venice Orders. 

# ⚙ Tech Stack
- .NET Core 8 / C# – API e camada de negócio
- Sql Server – Banco de dados relacional
- Entity Framework Core – ORM
- MongoDb – NoSQL database
- RabbitMQ – Messaging broker
- Repository Pattern – Abstração da camada de acesso a dados
- xUnit + Moq – Tests unitários
- Docker – Containerização

# 🛠 Features
## Criação de Pedidos: 
- Persistência dos Dados do Pedido em Banco Relacional (SQL Server)
- Item de Pedidos: Persistência dos Itens do Pedido em Banco NoSQL (MongoDB)
- Notificações de Pedidos: Envio de mensagens assíncronas para um sistema de notificações via RabbitMQ
## Obter Pedidos: 
- Consulta de Pedidos com detalhes dos Itens do Pedido (SQL Server + MongoDB)
## Testes Unitários:
- Cobertura de testes unitários para a camada de negócio utilizando xUnit e Moq

# Como rodar o projeto localmente
## Pré-requisitos
- .NET Core 8 SDK
- Docker
	- Instalar o Docker Desktop (https://www.docker.com/products/docker-desktop/)
- SQL Server (pode ser local ou via Docker)
	- via docker: 
		```bash
		docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
		```
- MongoDB 
	- via docker:
		docker pull mongo
		docker create mongo
		docker run --name mongo_venice -d -p 27023:27017 mongo
- RabbitMQ 
	- via Docker
		docker pull rabbitmq:3-management
		docker run --rm  -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management
		Acessar RabbitMq do Container >> http://localhost:15672/
		User e Psw >> guest
- Redis 
	- via Docker
		docker pull redis:latest
		docker run -d --name redis-server -p 6379:6379 redis:latest

## 🏗 Architecture

```mermaid
flowchart LR
    Client -->|HTTP POST /orders| API[Order API]
    API -->|CreateOrderAsync| Service[Order Service]
    Service --> RepoOrder[(Order Repository)]
    Service --> RepoItem[(Order Item Repository)]
    Service -->|PublishOrder| MQ[(RabbitMQ)]
    RepoOrder --> SQL[(SQL Server)]
    RepoItem --> MongoDb[(MongoDb)]
