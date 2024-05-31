namespace GeoEnjoy.Application.Exceptions;

public class SortingNotImplementedException(string sortingName) : NotImplementedException($"Sorting '{sortingName}' not implemented")
{
}
