namespace WorldRepr.World;

public enum TileKind
{
    Void = 0,
    Grass = 1,
    Water = 2,
    Stone = 3,
    // ...
}

public class Tile
{
    public TileKind Kind { get; set; }

    public override string ToString()
    {
        return $"Tile(kind={Kind})";
    }
}