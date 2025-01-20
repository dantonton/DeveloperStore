### README.md

# Sales Management Application

A simple application for managing sales, designed with clean architecture principles and implemented in .NET. The application includes features for creating, updating, and managing sales, with validations and event-driven updates.

## Features

- **Sales Management**: Create, update, and validate sales records.
- **Event-Driven Architecture**: Uses `IMediator` for publishing events like `SaleModifiedEvent` and `ItemCancelledEvent`.
- **Validation**: Ensures business rules are enforced, such as item quantity limits and preventing updates to cancelled sales.
- **Repository Pattern**: Abstracts data access with the `IBaseRepository` interface for flexibility and testability.

## Technologies Used

- **.NET**: Core framework for the application.
- **MediatR**: For implementing CQRS and event-driven design.
- **NSubstitute**: Used for mocking dependencies in unit tests.
- **xUnit**: Testing framework for unit tests.

## Application Flow

1. **Update Sale**:
   - Validates if the sale exists and is not cancelled.
   - Updates the sale details and items.
   - Publishes domain events for modified sales and cancelled items.
   - Returns a response indicating success or validation errors.

2. **Validation Rules**:
   - Item quantities must be 20 or less.
   - Cancelled sales cannot be updated.

3. **Logging**:
   - Events like `SaleCreatedEvent` are logged to a file.

## Running the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/dantonton/DeveloperStore.git
   ```
2. Navigate to the project folder:
   ```bash
   cd DeveloperStore/DeveloperStore
   ```
3. Build and run the application:
   ```bash
   dotnet build
   dotnet run
   ```

## Tests

Unit tests are included for key components like command handlers and domain logic. To run the tests:

```bash
dotnet test
```

## Sample Commands

Example `UpdateSaleCommand` payload:

```json
{
  "SaleId": "ee671554-8a3f-4501-8305-11c04f61968c",
  "Customer": "Dan",
  "Branch": "Main",
  "Items": [
    {
      "ProductName": "Item1",
      "Quantity": 5,
      "UnitPrice": 10
    },
    {
      "ProductName": "Item2",
      "Quantity": 15,
      "UnitPrice": 20
    }
  ]
}
```

## Future Improvements

- Implement a proper database connection for data persistence.
- Add more comprehensive integration tests.
- Improve exception handling and logging.

---

### License

This project is licensed under the [MIT License](LICENSE).