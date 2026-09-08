# Centeva.DomainModeling

This package contains types (classes and interfaces) for building a rich domain
layer for your application using some Domain Driven Design tactical patterns.

## Built With

- [.NET 8](https://dot.net)
- [MediatR](https://github.com/jbogard/MediatR)
- [Mediator](https://github.com/martinothamar/Mediator)
- [Ardalis.Specification](https://github.com/ardalis/Specification)

## Technical Patterns

You can use these coding patterns as part of a Domain Driven Design approach to
building your application. You don't have to be using DDD to benefit from these
patterns, but they are intended to be used together.

To find out more about this approach, here are some resources:

- [Domain Driven Design by Eric
  Evans](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)
  (book)
- [Implementing Domain Driven Design by Vaughn
  Vernon](https://www.amazon.com/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577)
  (book)
- [DDD Part 2: Tactical Domain-Driven
  Design](https://vaadin.com/blog/ddd-part-2-tactical-domain-driven-design)
  (blog post)
- [Centeva Web API Template](https://github.com/Centeva/Centeva.Templates)
  (template project)

### Entities

An _Entity_ is a plain object for which its identity is important. This is
represented by a unique `Id`. When the ID is generated in-process, it is
assigned when the entity is created and is not changed for the lifetime of the
entity. When the database generates the ID, it is assigned when the entity is
saved.

Two persisted instances of an entity type that have the same `Id` should be
considered equivalent.

An entity is mutable and its properties can be changed. However, it is
preferable to avoid having public setters for all of those properties. Instead
you should use methods to update the entity's properties. This allows you to
enforce _invariants_ (validation rules) and to publish _Domain Events_ when the
entity is changed. Use additional measures to protect an entity's invariants
such as constructors, guard clauses, and read-only collections.

In most cases your project will involve persisting entities to some kind of data
storage, such as a database. However, the details of such persistence should not
be contained within the definitions of those entities. (Avoid things like Entity
Framework annotation attributes like `[Table]`.)

The `BaseEntity<TId>` class can be inherited for your project's entities. `TId`
must be a non-nullable value type that implements `IEquatable<TId>`, which
covers all common choices (`int`, `Guid`, custom `readonly record struct`
strongly-typed IDs, etc.).

- The `Id` property (your entity's unique identifier) has an `init`-only setter.
  When using an ID generated in-process, assign it when creating an entity,
  typically in a constructor or static factory method, using a generator such as
  `Guid.NewGuid()` or a strongly-typed ID wrapper. This allows the entity to be
  fully formed before persistence while preventing the ID from being changed
  by application code after creation. If the database generates the ID, such as
  with an auto-incrementing integer, leave it unset and let the persistence
  provider populate it when the entity is saved. Explicit IDs are also useful
  when seeding data in tests and in your application.

### Value Objects

A _Value Object_ represents something in your domain which determines its
identity by its properties. Two value object instances are considered equal if
their relevant properties are equal. Because of this, a value object is ideally
immutable. For example, two Addresses are considered equal if they have the same
street address, city, state, and zip code.

Value object classes can and should contain business logic, especially for
ensuring valid properties.

Entities can (and should) contain value objects, but value objects should never
contain entities.

There are a few ways to implement a value object in C#, and you should pick
whichever fits the value best:

- For small values (roughly 16 bytes or less), such as strongly-typed IDs, money
  amounts, or coordinates, prefer a `readonly record struct`. It gives you value
  equality and immutability with no heap allocations.
- For larger values, or values whose equality components include collections,
  prefer a `record` (class). Copying a large struct on every method call is
  wasteful. Ensure that collection properties have the equality semantics you
  need, because record equality does not automatically compare collection
  contents.
- Inherit from the `ValueObject` class when you need something the built-in
  options can't easily express — most commonly, when only _some_ of the type's
  properties should participate in equality, or when the value object needs to
  participate in an inheritance hierarchy (which structs can't support).

See <https://enterprisecraftsmanship.com/posts/value-objects-explained/> for
more information about this concept.

### Aggregates

An _Aggregate_ is a collection of domain objects (Entities and Value Objects)
that is treated as a single unit for manipulation and enforcement of invariants.
An aggregate should adhere to the following rules:

- The aggregate is created, retrieved, and updated as a whole.
- The aggregate is always in a consistent and valid state.
- One of the entities in an aggregate is the main entity or "root" and holds
  references to the other ones.
- An aggregate should only reference the root of other aggregates.

You can use the `IAggregateRoot` interface to mark the roots of your aggregates.
This is just a marker interface (no properties or methods), but `IRepository<T>`
and `IReadRepository<T>` both constrain `T` to `IAggregateRoot`, so the compiler
enforces that repositories only operate on aggregate roots. (See below for more
on repositories.)

### Domain Events

_Domain Events_ describe things that happen in your domain model. They are
typically used to publish information about changes to your entities. These
events will be of interest to other parts of your model, and can be _handled_ to
produce side effects, such as sending emails or updating other entities.

Domain event dispatch is defined by the `IHasDomainEvents` interface, which
exposes a `DomainEvents` list and a way to clear it after publishing.
`BaseEntity<TId>` (via `ObjectWithEvents`) implements this for you, so any entity
gets a protected `RegisterDomainEvent` method for free. You can also implement
`IHasDomainEvents` directly on an object that isn't an entity if you need to
raise events from somewhere else.

You will use the `IDomainEventDispatcher` in your application to publish and
handle these events, likely inside of your Entity Framework `DbContext` or a
domain service.

### Repositories

_Repository_ is a pattern used to control and constrain access to data. It
defines standard CRUD operations on a set of entities of the same type. If you
are implementing Aggregates, your repositories should only operate on the root
of each Aggregate, as child entities should never be directly accessed. Both
`IRepository<T>` and `IReadRepository<T>` constrain `T` to `IAggregateRoot`, so
this is enforced by the compiler rather than left as a convention.

Read-only operations are defined in `IReadRepository` while `IRepository` adds
update operations to those. This not only better adheres to the Interface
Segregation Principle, but allows implementers to add features such as caching
that would only apply to read operations.

You can inherit from these interfaces in your own project if you need to extend
the default functionality.

The package `Centeva.DomainModeling.EFCore` provides an abstract implementation
of `IRepository` named `BaseRepository`. See below for details.

Note that the provided Repository implementation will call EF Core's
`SaveChangesAsync` method on each operation.  If you need some other behavior
then you may need to modify for your project.

### Specifications

_Specification_ is a pattern used to pull query logic out of other places in an
application and into self-contained, shareable, testable classes. This
eliminates the need to add custom query methods to your Repository, and avoids
other anti-patterns such as leaked `IQueryable` objects.

See the [documentation for the Ardalis.Specification
library](https://ardalis.github.io/Specification/) for more information and
examples.

### Domain Services

_Domain Services_ are used to encapsulate domain logic that doesn't belong in an
entity or value object. They are typically used to coordinate operations between
multiple entities or aggregates. They are also useful for encapsulating domain
logic that is not specific to a single entity or aggregate.

Domain Services can publish Domain Events. For example, a `CustomerService`
might publish a `CustomerDeletedEvent` when a customer is deleted, since the
`Customer` entity itself cannot publish an event when it is deleted.

There is no base implementation of a Domain Service in this library. You can
create regular C# classes and interfaces for these when they have dependencies
on other parts of your application, or use a static class and method for simpler
cases.

### Factories

_Factories_ are used to encapsulate logic for creating new aggregates. They are
useful when:

- Complex business logic is involved in creating an aggregate.
- You need to create an aggregate differently depending on the inputs.
- There is a large amount of input data.
- You need to create multiple aggregates at once.

There is no base implementation of a Factory in this library. You can create one
via a static method on an aggregate class for simple cases, or with a separate
factory class for more complex cases.

## Getting Started

Add a reference to `Centeva.DomainModeling.MediatR` or
`Centeva.DomainModeling.Mediator` in your project, depending on which domain
event dispatcher you want to use.

You should only need to reference this package from the lowest layer of your
solution. If you are using multiple projects to separate Core/Domain,
Application, and Web API layers (i.e., "Clean" or "Ports and Adapters"
architecture) then reference from the Core project.

### Using Entity Framework Core

Reference the `Centeva.DomainModeling.EFCore` package to get a base
implementation of the Repository pattern for this ORM. Reference this package
from your Infrastructure project if your solution separates concerns by project.

Create a derived class in your Infrastructure project that inherits from
`BaseRepository` and implements the interfaces:

```csharp
public class EfRepository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext)
      : base(dbContext) { }
}
```

Register your derived Repository class and EF Core with your application's
dependency injection container:

```csharp
services.AddSingleton<DispatchDomainEventsInterceptor>();

services.AddDbContext<ApplicationDbContext>((sp, options) => options
    .UseSqlServer(connectionString)
    .AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>()));

services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

services.AddMediator();
...
```

Register your desired domain event dispatcher.  Install the package that
contains your chosen implementation, either `Centeva.DomainModeling.MediatR` for
[MediatR](https://mediatr.io/) support, or `Centeva.DomainModeling.Mediator` for
the source-generator based [Mediator](https://github.com/martinothamar/Mediator)
library.

```csharp
services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();
```

__Note:__ You can only register one implementation.  MediatR is a commercially
licensed library, while Mediator is open source and free to use.

See the documentation for the Entity Framework Core and other packages for more
information on how to use them.

Use your repository by injecting it into your application's services. For
example:

```csharp
public class CustomerService
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    //...
}
```

## Running Tests

From Windows, use the `dotnet test` command, or your Visual Studio Test
Explorer. Integration tests will use an in-memory SQLite database.

## Deployment

Use `dotnet pack` to generate a NuGet package. This library is versioned by
[GitVersion](https://gitversion.net/). Create a Git tag for an official release
(e.g., "v1.0.0").

## Contributing

Please use a Pull Request to suggest changes to this library. You should not add
any functionality or dependency that is not appropriate for use at the lowest
level (the "domain" level) of an application.

## Resources

Take a look at <https://bitbucket.org/centeva/centeva.templates> for more ideas
on how to use this library in your application.
