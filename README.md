# ProjectPulse.Backend

A Clean Architecture based .NET 8 backend for project and task management.

## Layers

- **API**: Handles HTTP requests and responses.
- **Application**: Contains business logic, DTOs, interfaces.
- **Domain**: Core entities and domain rules.
- **Infrastructure**: Implements interfaces for data access, services.
- **Shared**: Cross-cutting concerns like error handling, logging, and results.

# Shared Layer

This layer includes reusable, cross-cutting concerns such as:

- Standardized result response (`Result<T>`)
- Domain-level exception types
- Logger abstraction for infrastructure-agnostic logging
- Time provider for testability
- Centralized error codes and messages

