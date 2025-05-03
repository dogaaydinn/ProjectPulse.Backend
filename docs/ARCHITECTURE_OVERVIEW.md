# 🏗️ Architecture Overview

## 1. Project Purpose
A modern, scalable Project Management Tool designed with Clean Architecture principles and advanced enterprise practices.

## 2. Layers

- **Domain**: Pure business logic, Entities, ValueObjects, Enums
- **Application**: UseCases, DTOs, Services, Validators, Mapping
- **Infrastructure**: EF Core, Repositories, Persistence, Factory implementations
- **API**: Web layer (to be added later)
- **Shared**: Cross-cutting concerns (Results, Exceptions, Constants, etc.)

## 3. Key Practices

- SOLID Principles
- DDD-lite approach
- Manual CQRS (No MediatR)
- Custom Validation
- Error abstraction (Result<T>)
- Factory Pattern
- ValueObjects for safety
