# N5 Now - Permissions Management System

A .NET 8 web application that manages employee permissions with SQL Server persistence and Elasticsearch indexing.

## Technology Stack

- .NET 8.0
- SQL Server (Entity Framework Core)
- Elasticsearch 7.17
- Docker & Docker Compose
- xUnit for testing

## Project Structure

The solution follows Clean Architecture principles with these main projects:

- **N5.Now.Api**: REST API endpoints and application configuration
- **N5.Now.Application**: Business logic, commands and handlers using MediatR
- **N5.Now.Domain**: Domain entities and interfaces
- **N5.Now.Infrastructure**: Data access, Elasticsearch integration
- **N5.Now.Test**: Unit tests using xUnit and Moq

## Features

- CRUD operations for employee permissions
- Permission types management (Read/Write)
- Real-time Elasticsearch indexing
- Unit test coverage with mocking

## Prerequisites

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 or compatible IDE
