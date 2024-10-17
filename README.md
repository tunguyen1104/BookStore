# BookStore Solution

This solution follows **Clean Architecture** principles with an **N-Layered Architecture** structure. The project is divided into five layers: `Domain`, `Application`, `Infrastructure`, `Web (Client)`, and `Admin`, each with specific responsibilities to ensure maintainability, scalability, and clear separation of concerns.

## Project Structure

### 1. **BookStore.Domain** (Class Library)
- **Purpose**: Contains the core business logic, entities, and domain rules.
- **Contents**:
  - **Entities**: Core entities, such as `User`, `Product`, `Order`, etc.
  - **Interfaces**: Repository contracts (e.g., `IProductRepository`, `IUserRepository`).
  - **Value Objects**: Business-specific objects (e.g., `Money`, `Email`).

### 2. **BookStore.Application** (Class Library)
- **Purpose**: Implements the application logic and use cases, connecting the UI and Domain layers.
- **Contents**:
  - **DTOs**: Data Transfer Objects for communication between layers.
  - **Services**: Service interfaces (e.g., `IProductService`, `IUserService`).
  - **Mappers**: Maps domain entities to DTOs and vice versa.

### 3. **BookStore.Infrastructure** (Class Library)
- **Purpose**: Handles data access, external services, and any infrastructure concerns.
- **Contents**:
  - **Repositories**: Implement repository interfaces (e.g., `ProductRepository`, `UserRepository`) using technologies like Entity Framework, Dapper, etc.
  - **Data Access**: Configure ORM (e.g., EF Core) to interact with the database.
  - **External Services**: External integrations (e.g., email, payment processing).
  - **Migrations**: Database migration files (if using EF Core).

### 4. **BookStore.Client** (ASP.NET MVC - Client)
- **Purpose**: The client-facing web application for end-users (customers).
- **Contents**:
  - **Controllers**: Handle incoming HTTP requests (e.g., `ProductController`, `OrderController`).
  - **Views**: Razor views for the user interface (e.g., product catalog, cart, checkout).
  - **ViewModels**: Models for the UI.
  - **Static Content**: CSS, JS, images, etc.

### 5. **BookStore.Admin** (ASP.NET MVC - Admin)
- **Purpose**: The admin-facing web application for managing the system.
- **Contents**:
  - **Controllers**: Handle admin-specific requests (e.g., `AdminProductController`, `AdminUserController`).
  - **Views**: Razor views for the admin interface (e.g., dashboards, management pages).
  - **ViewModels**: Models for admin UI.
  - **Static Content**: CSS, JS, images, etc.

## How It Works

### Layer Interaction

- **Web (Client) and Admin Projects**: These reference the `Application` and `Domain` layers to interact with the business logic.
- **Application Layer**: Acts as a mediator, accessing entities and repositories from the `Domain` and `Infrastructure` layers.
- **Infrastructure Layer**: Contains the actual implementation of the repository interfaces, interacting with external systems like the database.

### Example Flow

1. A client requests a product list via `BookStore.Client`.
2. The controller in `BookStore.Client` calls a service from `BookStore.Application`.
3. The service fetches data through the repository interface defined in `BookStore.Domain`.
4. The repository implementation in `BookStore.Infrastructure` communicates with the database.
5. Data flows back to the service, then to the controller, and is rendered in the view.

## Benefits of Clean Architecture

- **Separation of Concerns**: Each layer is responsible for a specific part of the application, making the code more maintainable.
- **Scalability**: New features can be added with minimal changes to existing code.
- **Testability**: The structure supports unit testing of each layer independently.
- **Modularity**: Layers are independent and can be modified or replaced without affecting other parts of the system.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)

### Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/Giappp/BookStore.git

2. Navigate to the solution directory:
    ```bash
    cd src

3. Build the solution:
    ```bash
    dotnet Build

4. Apply database migrations
    ```bash
    dotnet ef database update

5. Run the client and admin projects:
    ```bash
    dotnet run --project BookStore.Client
    dotnet run --project BookStore.Admin