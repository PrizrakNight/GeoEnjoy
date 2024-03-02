using FluentAssertions;
using GeoEnjoy.Application.Sortings;
using GeoEnjoy.Application.Tests.Data;

namespace GeoEnjoy.Application.Tests.SortingExtensionsTests;

public class EnumerableTests
{
    [Fact]
    public void ApplyTo_OrderByName_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob", 1000),
            new PersonForTest(19, "Alice", 950),
            new PersonForTest(23, "Cero", 9000)
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: nameof(PersonForTest.Name)
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Name)
            .ToArray();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ApplyTo_OrderByBalance_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob", 1000),
            new PersonForTest(19, "Alice", 950),
            new PersonForTest(23, "Cero", 9000)
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: nameof(PersonForTest.Balance)
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Balance)
            .ToArray();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ApplyTo_OrderByNameThenByAge_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob1", 1000),
            new PersonForTest(7, "Bob2", 1000),
            new PersonForTest(19, "Alice1", 950),
            new PersonForTest(11, "Alice2", 950),
            new PersonForTest(23, "Cero1", 9000),
            new PersonForTest(9, "Cero2", 9000)
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: nameof(PersonForTest.Name)
            ),
            new Sorting
            (
                Descending: false,
                PropertyPath: nameof(PersonForTest.Age)
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Name)
            .ThenByDescending(x => x.Age)
            .ToArray();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ApplyTo_OrderByPetName_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob", 1000, new PersonPetForTest(3, "Alpak")),
            new PersonForTest(19, "Alice", 950, new PersonPetForTest(1, "Boris")),
            new PersonForTest(23, "Cero", 9000, new PersonPetForTest(3, "Dell"))
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: "Pet.Name"
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Pet!.Name)
            .ToArray();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ApplyTo_OrderByPetNameThenByDescendingAge_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob", 1000, new PersonPetForTest(3, "Alpak1")),
            new PersonForTest(12, "Blow", 300, new PersonPetForTest(3, "Alpak2")),
            new PersonForTest(19, "Alice", 950, new PersonPetForTest(1, "Boris1")),
            new PersonForTest(19, "Alan", 100, new PersonPetForTest(3, "Booler2")),
            new PersonForTest(23, "Cero", 9000, new PersonPetForTest(3, "Dell1")),
            new PersonForTest(23, "Ceto", 3000, new PersonPetForTest(6, "Doberman2"))
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: "Pet.Name"
            ),
            new Sorting
            (
                Descending: true,
                PropertyPath: "Pet.Age"
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Pet!.Name)
            .ThenByDescending(x => x.Pet!.Age)
            .ToArray();

        // Assert

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ApplyTo_OrderByPetNameThenByAge_OneSorting()
    {
        // Arrange

        var dataForTest = new[]
        {
            new PersonForTest(12, "Bob", 1000, new PersonPetForTest(3, "Alpak1")),
            new PersonForTest(12, "Blow", 300, new PersonPetForTest(3, "Alpak2")),
            new PersonForTest(19, "Alice", 950, new PersonPetForTest(1, "Boris1")),
            new PersonForTest(19, "Alan", 100, new PersonPetForTest(3, "Booler2")),
            new PersonForTest(23, "Cero", 9000, new PersonPetForTest(3, "Dell1")),
            new PersonForTest(23, "Ceto", 3000, new PersonPetForTest(6, "Doberman2"))
        };

        var sortings = new Sorting[]
        {
            new Sorting
            (
                Descending: false,
                PropertyPath: "Pet.Name"
            ),
            new Sorting
            (
                Descending: false,
                PropertyPath: "Pet.Age"
            )
        };

        // Act

        var result = sortings
            .ApplyTo(dataForTest)
            .ToArray();

        var expected = dataForTest
            .OrderBy(x => x.Pet!.Name)
            .ThenBy(x => x.Pet!.Age)
            .ToArray();

        // Assert

        result.Should().BeEquivalentTo(expected);
    }
}
