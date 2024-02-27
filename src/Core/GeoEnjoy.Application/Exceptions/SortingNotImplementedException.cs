namespace GeoEnjoy.Application.Exceptions;

public class SortingNotImplementedException : NotImplementedException
{
    public SortingNotImplementedException(string sortingName)
        : base($"Sorting '{sortingName}' not implemented")
    {

    }
}
