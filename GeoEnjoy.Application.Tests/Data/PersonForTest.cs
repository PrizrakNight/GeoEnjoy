namespace GeoEnjoy.Application.Tests.Data;

public record PersonForTest(int Age, string Name, decimal Balance, PersonPetForTest? Pet = null);
