namespace Centeva.DomainModeling.UnitTests;

// From https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.UnitTests/Domain/SeedWork/ValueObjectTests.cs
public class ValueObjectTests
{
    [Theory]
    [MemberData(nameof(EqualValueObjects))]
    public void Equals_EqualValueObjects_ReturnsTrue(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = EqualityComparer<ValueObject>.Default.Equals(instanceA, instanceB);

        // Assert
        result.Should().BeTrue(reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualValueObjects))]
    public void Equals_NonEqualValueObjects_ReturnsFalse(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = EqualityComparer<ValueObject>.Default.Equals(instanceA, instanceB);

        // Assert
        result.Should().BeFalse(reason);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Act
        var result = APrettyValueObject.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(EqualValueObjects))]
    public void EqualOperator_EqualValueObjects_ReturnsTrue(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = ValueObjectOperators.AreEqual(instanceA, instanceB);

        // Assert
        result.Should().BeTrue(reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualValueObjects))]
    public void EqualOperator_NonEqualValueObjects_ReturnsFalse(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = ValueObjectOperators.AreEqual(instanceA, instanceB);

        // Assert
        result.Should().BeFalse(reason);
    }

    [Theory]
    [MemberData(nameof(EqualValueObjects))]
    public void NotEqualOperator_EqualValueObjects_ReturnsFalse(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = ValueObjectOperators.AreNotEqual(instanceA, instanceB);

        // Assert
        result.Should().BeFalse(reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualValueObjects))]
    public void NotEqualOperator_NonEqualValueObjects_ReturnsTrue(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var result = ValueObjectOperators.AreNotEqual(instanceA, instanceB);

        // Assert
        result.Should().BeTrue(reason);
    }

    [Theory]
    [MemberData(nameof(EqualValueObjects))]
    public void GetHashCode_EqualValueObjects_ReturnsSameHash(ValueObject? instanceA, ValueObject? instanceB, string reason)
    {
        // Act
        var hashA = instanceA?.GetHashCode();
        var hashB = instanceB?.GetHashCode();

        // Assert
        hashA.Should().Be(hashB, reason);
    }

    [Fact]
    public void GetCopy_ReturnsEqualButDistinctInstance()
    {
        // Act
        var copy = APrettyValueObject.GetCopy();

        // Assert
        copy.Should().NotBeSameAs(APrettyValueObject);
        copy.Should().Be(APrettyValueObject);
    }

    private static readonly ValueObject APrettyValueObject = new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3"));

    public static readonly TheoryData<ValueObject?, ValueObject?, string> EqualValueObjects = new()
    {
        {
            null,
            null,
            "they should be equal because they are both null"
        },
        {
            APrettyValueObject,
            APrettyValueObject,
            "they should be equal because they are the same object"
        },
        {
            new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3")),
            new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3")),
            "they should be equal because they have equal members"
        },
        {
            new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3"), notAnEqualityComponent: "xpto"),
            new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3"), notAnEqualityComponent: "xpto2"),
            "they should be equal because all equality components are equal, even though an additional member was set"
        },
        {
            new ValueObjectB(1, "2",  1, 2, 3 ),
            new ValueObjectB(1, "2",  1, 2, 3 ),
            "they should be equal because all equality components are equal, including the 'C' list"
        }
    };

    public static readonly TheoryData<ValueObject?, ValueObject?, string> NonEqualValueObjects = new()
    {
        {
            new ValueObjectA(a: 1, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(2, "3")),
            new ValueObjectA(a: 2, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(2, "3")),
            "they should not be equal because the 'A' member on ValueObjectA is different among them"
        },
        {
            new ValueObjectA(a: 1, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(2, "3")),
            new ValueObjectA(a: 1, b: null, c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(2, "3")),
            "they should not be equal because the 'B' member on ValueObjectA is different among them"
        },
        {
            new ValueObjectA(a: 1, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(a: 2, b: "3")),
            new ValueObjectA(a: 1, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(a: 3, b: "3")),
            "they should not be equal because the 'A' member on ValueObjectA's 'D' member is different among them"
        },
        {
            new ValueObjectA(a: 1, b: "2", c: Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), d: new ComplexObject(a: 2, b: "3")),
            new ValueObjectB(a: 1, b: "2"),
            "they should not be equal because they are not of the same type"
        },
        {
            new ValueObjectB(1, "2",  1, 2, 3 ),
            new ValueObjectB(1, "2",  1, 2, 3, 4 ),
            "they should be not be equal because the 'C' list contains one additional value"
        },
        {
            new ValueObjectB(1, "2",  1, 2, 3, 5 ),
            new ValueObjectB(1, "2",  1, 2, 3 ),
            "they should be not be equal because the 'C' list contains one additional value"
        },
        {
            new ValueObjectB(1, "2",  1, 2, 3, 5 ),
            new ValueObjectB(1, "2",  1, 2, 3, 4 ),
            "they should be not be equal because the 'C' lists are not equal"
        },
        {
            APrettyValueObject,
            null,
            "they should not be equal because the second is null"
        },
        {
            null,
            APrettyValueObject,
            "they should not be equal because the first is null"
        }
    };

    /// <summary>
    /// Exposes the protected operator helpers that derived value objects use to implement == and !=.
    /// </summary>
    private class ValueObjectOperators : ValueObject
    {
        public static bool AreEqual(ValueObject? left, ValueObject? right) => EqualOperator(left, right);

        public static bool AreNotEqual(ValueObject? left, ValueObject? right) => NotEqualOperator(left, right);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield break;
        }
    }

    private class ValueObjectA : ValueObject
    {
        public ValueObjectA(int a, string? b, Guid c, ComplexObject d, string? notAnEqualityComponent = null)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            NotAnEqualityComponent = notAnEqualityComponent;
        }

        public int A { get; }
        public string? B { get; }
        public Guid C { get; }
        public ComplexObject D { get; }
        public string? NotAnEqualityComponent { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return A;
            yield return B;
            yield return C;
            yield return D;
        }
    }

    private class ValueObjectB : ValueObject
    {
        public ValueObjectB(int a, string b, params int[] c)
        {
            A = a;
            B = b;
            C = c.ToList();
        }

        public int A { get; }
        public string B { get; }

        public List<int> C { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return A;
            yield return B;

            foreach (var c in C)
            {
                yield return c;
            }
        }
    }

    private class ComplexObject : IEquatable<ComplexObject>
    {
        public ComplexObject(int a, string b)
        {
            A = a;
            B = b;
        }

        public int A { get; set; }

        public string B { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ComplexObject);
        }

        public bool Equals(ComplexObject? other)
        {
            return other != null &&
                    A == other.A &&
                    B == other.B;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }
}
