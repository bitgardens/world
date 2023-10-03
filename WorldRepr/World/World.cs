using System.Text.Json;
using System.Text.Json.Serialization;
using WorldRepr.Repr;

namespace WorldRepr.World;

public class World
{
    internal ushort Size;
    internal readonly Topology Topology = new(1);
    internal readonly Meta Meta = new(1);

    /// <summary>
    /// Returns an enumerable over the walkable neighbors of the given position.
    /// </summary>
    public IEnumerable<Position> WalkableNeighbors(Position p)
    {
        var d = Topology.Entries[p];
        if (d.Top()) yield return p.WithY(-1);
        if (d.Right()) yield return p.WithX(1);
        if (d.Bottom()) yield return p.WithY(1);
        if (d.Left()) yield return p.WithX(-1);
    }

    /// <summary>
    /// Returns all the walkable nodes in the topology graph.
    /// </summary>
    public IEnumerable<Position> WalkableNodes()
    {
        return Topology.Entries.Keys;
    }

    /// <summary>
    /// Returns the tile at the given position.
    /// </summary>
    public Tile GetMeta(Position p)
    {
        return Meta.Entries[p];
    }

    internal World(ushort size)
    {
        Size = size;
    }

    internal World(Topology topology, Meta meta)
    {
        Topology = topology;
        Meta = meta;
    }

    public static World FromJson(string json)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        var worldJson = JsonSerializer.Deserialize<WorldJsonRepr>(json, options);
        if (worldJson == null)
            throw new NullReferenceException("Deserialization returned null");
        return worldJson.ToWorld();
    }

    public string ToJson()
    {
        var worldJson = new WorldJsonRepr(this);
        return JsonSerializer.Serialize<WorldJsonRepr>(worldJson);
    }
}

class WorldJsonRepr
{
    [JsonInclude]
    public Dictionary<string, string> RawTopology { get; set; }
    [JsonInclude]
    public Dictionary<string, Tile> RawMeta { get; set; }

    public WorldJsonRepr()
    {
        RawTopology = new();
        RawMeta = new();
    }

    internal WorldJsonRepr(World w)
    {
        RawTopology = w.Topology.Entries.ToDictionary(
            kv => kv.Key.ToId().ToString(),
            kv => kv.Value.EncodeAsString());
        RawMeta = w.Meta.Entries.ToDictionary(
            kv => kv.Key.ToId().ToString(),
            kv => kv.Value);
    }

    internal World ToWorld()
    {
        return new World(Topology(), Meta());
    }

    internal Topology Topology()
    {
        var t = new Topology(RawTopology.Count);
        foreach (var item in RawTopology)
        {
            var position = Position.FromId(uint.Parse(item.Key));
            var directions = Directions.DecodeFromString(item.Value);
            t.Entries.Add(position, directions);
        }
        return t;
    }

    internal Meta Meta()
    {
        var m = new Meta(RawMeta.Count);
        foreach (var item in RawMeta)
        {
            var position = Position.FromId(uint.Parse(item.Key));
            m.Entries.Add(position, item.Value);
        }
        return m;
    }
}

internal class Topology
{
    internal Dictionary<Position, Directions> Entries;

    internal Topology(int size)
    {
        Entries = new Dictionary<Position, Directions>(size);
    }
}

class Meta
{
    internal Dictionary<Position, Tile> Entries;
    
    internal Meta(int size)
    {
        Entries = new Dictionary<Position, Tile>(size);
    }
}
