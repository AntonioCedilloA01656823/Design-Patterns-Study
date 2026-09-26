# Repository Pattern

## Category
Enterprise / data-access pattern (Fowler, *Patterns of Enterprise Application
Architecture*). Not one of the original GoF patterns.

## Intent
Give the domain a **collection-like interface** for retrieving and storing
objects, while hiding how they are actually persisted (memory, SQL, files, an
API). Application code talks in domain terms (`get`, `add`, `list_all`), not in
query or connection terms.

The problem it solves: services fill up with storage details (`cursor.execute`,
file paths, ORM session plumbing). Tests then need a real database, and swapping
storage means editing every caller. A repository puts that mapping behind one
abstraction so the rest of the program depends on "a collection of users," not
on a specific store.

## Roles and structure

- **Entity** — a domain object the rest of the app cares about (`User`).
- **Repository** — the collection interface (`add`, `get`, `list_all`, ...).
- **ConcreteRepository** — one persistence mapping (`InMemoryUserRepository`,
  later a SQL implementation).
- **Client** — application code that depends only on the repository interface.

```
Client --> Repository (interface) --> Entity
                ^
     InMemoryRepository, SqlRepository, ...
```

## When to use it

- Domain code should not know about SQL, files, or HTTP persistence.
- You want tests to run against a fake/in-memory store with the same interface.
- You may swap or wrap storage later without rewriting services.

## When not to use it

- A small script with one table and no domain layer — a function that reads
  the store is enough.
- The "repository" would only forward every call 1:1 to an ORM `DbSet` with
  no extra meaning — that extra interface is ceremony, not a pattern win.
- You need transaction/change tracking across several repositories — that is
  **Unit of Work**, often used *with* Repository, not instead of a store.

## Tradeoffs

- **Pro:** Persistence stays in one place; clients stay testable; new storage
  is a new class behind the same interface.
- **Con:** Another layer to design and keep in sync with the database schema.
- **Con:** A "generic `Repository<T>` with full SQL leaking through" often
  becomes a disguised data-access API, which misses the point.
- Repository is not always the best choice. Direct use of an ORM is simpler
  when the app *is* the data model.

## Comparison with Active Record (closest confused pattern)

- **Repository:** persistence sits *outside* the entity. `User` is data +
  domain behavior; a repository loads and saves `User` objects.
- **Active Record:** the entity *is* the persistence API (`user.save()`,
  `User.find(id)`). Convenient for simple CRUD apps; couples the model to
  the database.
- Rule of thumb: Repository = "a collection of domain objects." Active Record
  = "this object knows how to store itself."

**DAO** is a second cousin: it is usually table/data-source shaped. A
repository speaks domain language and may sit on top of one or more DAOs.

## Running the example

Open `example.ipynb` and run all cells. A client function uses `UserRepository`
while `InMemoryUserRepository` holds the data.

Then read `example.cs` for the same example in C# (`IUserRepository` +
`InMemoryUserRepository`). If you have the .NET SDK, paste it into a console
`Program.cs` and run it.

Then work through `exercises.ipynb`. Do not look up solutions until you have
tried each exercise.
