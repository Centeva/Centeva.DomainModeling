# Centeva Shared Kernel

This package contains types (classes and interfaces) that are likely to be
shared between multiple solutions.  As a result, it should be kept as simple and
with as few dependencies as possible.

## Built With

* [.NET 6](https://dot.net)
* [MediatR](https://github.com/jbogard/MediatR)
* [Ardalis.Specification](https://github.com/ardalis/Specification)

## Getting Started

Import the NuGet package `Centeva.SharedKernel` from this Centeva NuGet
repository.  You can add this repository to your own solution by adding a
[nuget.config](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file)
file at the root level:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="Centeva Public" value="https://builds.centeva.com/guestAuth/app/nuget/feed/CentevaPackages/default/v3/index.json" />
  </packageSources>
</configuration>
```

## Usage

### Entities

An entity is a plain object that has a unique identifier and contains properties
that are likely independently mutable.  Two instances of an entity type that
have the same identifier should be considered to be equivalent.  In most cases
your project will involve persisting entities to some kind of data storage, such
as an SQL database.  However, the details of such persistence should not be
contained within the definitions of those entities.  (Avoid things like Entity
Framework class attributes like `[Table]`.)

The `BaseEntity` class can be inherited for your project's entities.  

* The `Id` property (your unique identities) has a `private` setter since you
  should not let your developers set this manually.  Frameworks such as Entity
  Framework Core will set this when persisting.  (Yes it can get around the
  `private` modifier.)

You should strive to protect an entity's invariants using appropriate measures
such as private setters, read-only collections, and public methods for updating
the entity's properties.

### Value Objects

A value object represents something in your domain that has no unique identity.
They are ideally immutable and equality is determined by comparing its
properties.

Entities can (and should) contain value objects, but value objects should never
contain entities.  

Your value object classes should inherit from the `ValueObject` class to gain
equality functionality.

See https://enterprisecraftsmanship.com/posts/value-objects-explained/ for
more information about this concept.

### Domain Events

Each entity will contain an `Events` list which you can use for storing and
later publishing Domain Events.  These are used to implement "side effects"
based on work done to your entities.  These events can be handled elsewhere in
your code to better decouple them from other parts of the system.  *You will
need to use MediatR in your application to publish and handle these, likely
inside of your Entity Framework `DbContext`.*

### Aggregate Roots

Your entities can use the `IAggregateRoot` interface to implement the Domain
Driven Design "Aggregate" pattern.  An Aggregate is a collection of objects
(typically Entities) that is treated as a single unit for manipulation and
enforcement of invariants (validation rules).  A good example is an `Order` with 
its collection of `OrderItem`s.  

This is just a marker interface (no properties or methods) and it's up to you to
enforce the Aggregate pattern.

### Repositories

A Repository is a pattern used to control and constrain access to data such as
your persisted Entities.  It defines standard CRUD operations on a set of
objects.  Read-only operations are defined in `IReadRepository` while
`IRepository` adds update operations to those.  This not only better adheres to
the Interface Segregation Principle, but allows implementers to add features
such as caching that would only apply to read operations.  

Note that implementations of these interfaces can only provide access to objects
that implement the `IAggregateRoot` marker interface.

### Specifications

The Specification pattern is used to pull query logic out of other places in an
application and into self-contained, shareable, testable classes.  This
eliminates the need to add custom query methods to your Repository, and avoids
other anti-patterns such as leaked IQueryable objects.

See the [documentation for the Ardalis.Specification
library](https://ardalis.github.io/Specification/) for more information and
examples.

### Other Interfaces

`IDateTimeProvider` can be used as an abstraction for the system clock, which
should be considered an external dependency in your application.  Your time-
based tests will be happier if you mock this interface instead of using Date
methods directly.

## Running Tests

Use the standard `dotnet test` command your your Visual Studio Test Explorer.

## Deployment

Use `dotnet pack` to generate a NuGet package.  This library is versioned by
[GitVersion](https://gitversion.net/).  Create a Git tag for an official release
(e.g., "v1.0.0").

## Contributing

Please use a Pull Request to suggest changes to this library.  You should not
add any functionality or dependency that is not appropriate for use at the
lowest level (the "domain" level) of an application.

## Resources

Take a look at https://bitbucket.org/centeva/centevaarchitecturetemplate for
more implementation details.
