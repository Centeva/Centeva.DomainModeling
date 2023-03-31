# Centeva.DomainModeling

This package contains types (classes and interfaces) for building a rich domain
layer for your application.  

## Built With

* [.NET 6](https://dot.net)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [Ardalis.Specification](https://github.com/ardalis/Specification)

## Getting Started

Import the NuGet package `Centeva.DomainModeling` from the Centeva NuGet
repository.  You can add this repository to your own solution by adding a
[nuget.config](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file)
file in the same folder as your solution (.sln):

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
    <add key="Centeva Public" value="https://builds.centeva.com/guestAuth/app/nuget/feed/CentevaPackages/default/v3/index.json" />
  </packageSources>
</configuration>
```

You should only need to reference this package from the lowest layer of your
solution.  If you are using multiple projects to separate Core/Domain,
Application, and Web API layers (i.e., "Clean" or "Ports and Adapters"
architecture) then reference from the Core project.

### Using Entity Framework Core

Import the `Centeva.DomainModeling.EFCore` package to get an implementation of the
Repository pattern for this ORM.  Reference this package from your
Infrastructure project if your solution separates concerns by project.

This requires EF Core and MediatR to be added to your application's services
configuration for dependency injection.  See the documentation for these tools
individually for instructions.

### Using AutoMapper for Projection Support

You can use AutoMapper to provide [projection
support](https://docs.automapper.org/en/latest/Projection.html) for your
Repositories, which will allow you to map your entities to another type and only
request the data you need from the database.

Import the `Centeva.DomainModeling.EFCore.AutoMapper` package, add AutoMapper to
your application's services configuration for dependency injection, and derive
your Repository classes from `BaseProjectedRepository`.  See the [AutoMapper
documentaion](https://docs.automapper.org/en/latest/Dependency-injection.html#asp-net-core)
for information on dependency injection setup.

## Technical Patterns

### Entities

An entity is a plain object that has a unique identifier and contains properties
that are likely independently mutable.  Two instances of an entity type that
have the same identifier should be considered to be equivalent.  

In most cases your project will involve persisting entities to some kind of data
storage, such as an SQL database.  However, the details of such persistence
should not be contained within the definitions of those entities.  (Avoid things
like Entity Framework annotation attributes like `[Table]`.)

The `BaseEntity` class can be inherited for your project's entities.  

* The `Id` property (your entity's unique identifier) has a `public` setter but
  try to avoid using it in application code, especially if your database is
  auto-generating values.  However, it can be helpful when seeding data both in
  tests and in your application.

You should strive to protect an entity's invariants using appropriate measures
such as private setters, read-only collections, constructors, and public methods
for updating the entity's properties.

In addition, register Domain Events within your entity's methods if you expect
to have side effects as a result of calling those methods (such as changes to
other unconnected entities.)

### Value Objects

A value object represents something in your domain that has no unique identity.
A value object is ideally immutable and equality is determined by comparing its
properties.

Entities can (and should) contain value objects, but value objects should never
contain entities.

Your value object classes should inherit from the `ValueObject` class to gain
equality functionality.

See https://enterprisecraftsmanship.com/posts/value-objects-explained/ for more
information about this concept.

### Aggregates

An Aggregate is a collection of domain objects (Entities and Value Objects) that
is treated as a single unit for manipulation and enforcement of invariants
(validation rules).  A good example is an `Order` with its collection of
`OrderItem`s.  

One of the entities in an aggregate is the main entity or "root" and holds
references to the other ones.  An aggregate is retrieved via its root and
outside entities should strive to reference other aggregates only via the root.

You can use the `IAggregateRoot` interface to mark the roots of your aggregates.
This is just a marker interface (no properties or methods) and it's up to you to
enforce the Aggregate pattern.  (See below for information about enforicing in
your repositories.)

### Domain Events

Each entity inheriting from `BaseEntity` contains a `DomainEvents` list which
you can use for storing and later publishing Domain Events.  These are used to
implement "side effects" based on work done to your entities.  These events can
be handled elsewhere in your code to better decouple them from other parts of
the system.  *You will need to use the `DomainEventDispatcher` in your
application to publish and handle these, likely inside of your Entity Framework
`DbContext`.*

### Repositories

Repository is a pattern used to control and constrain access to data.  It
defines standard CRUD operations on a set of entities of the same type.
Read-only operations are defined in `IReadRepository` while `IRepository` adds
update operations to those.  This not only better adheres to the Interface
Segregation Principle, but allows implementers to add features such as caching
that would only apply to read operations.  

If you are implementing Aggregates, your repositories should only operate on the
root of each Aggregate, as child entities should never be directly accessed.

The package `Centeva.DomainModeling.EFCore` provides an abstract implementation of
`IRepository` named `BaseRepository`.  You can use it by creating a derived
class in your project.  Additionally, if you want to enforce that repositories
can only access aggregate roots, then your derived class should look like this:

```csharp
public class EfRepository<T> : BaseRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext) 
      : base(dbContext) { }
}
```

If you are using the AutoMapper project feature, then derive your repository
from the `BaseProjectedRepository` class like this:

```csharp
public class EfRepository<T> : BaseProjectedRepository<T>, IRepository<T>, IProjectedReadRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext, IConfigurationProvider mappingConfigurationProvider) 
      : base(dbContext, mappingConfigurationProvider) { }
}
```

### Specifications

Specification is a pattern used to pull query logic out of other places in an
application and into self-contained, shareable, testable classes.  This
eliminates the need to add custom query methods to your Repository, and avoids
other anti-patterns such as leaked `IQueryable` objects.

See the [documentation for the Ardalis.Specification
library](https://ardalis.github.io/Specification/) for more information and
examples.

## Running Tests

From Windows, use the `dotnet test` command, or your Visual Studio Test
Explorer.  Integration tests will use SQL Server LocalDB.

To run tests in a set of containers with Docker Compose, which is useful for
build pipelines and non-Windows development environments, use the
`ci/run-tests.sh` or `ci/run-tests.bat` scripts.

## Deployment

Use `dotnet pack` to generate a NuGet package.  This library is versioned by
[GitVersion](https://gitversion.net/).  Create a Git tag for an official release
(e.g., "v1.0.0").

## Contributing

Please use a Pull Request to suggest changes to this library.  You should not
add any functionality or dependency that is not appropriate for use at the
lowest level (the "domain" level) of an application.

## Resources

Take a look at <https://bitbucket.org/centeva/centeva.templates> for more
implementation details.
