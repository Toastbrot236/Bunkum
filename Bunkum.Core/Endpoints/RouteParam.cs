namespace Bunkum.Core.Endpoints;

public struct RouteParam
{
    internal readonly int Skip;
    internal readonly string Name;

    internal RouteParam(int skip, string name)
    {
        this.Skip = skip;
        this.Name = name;
    }
}