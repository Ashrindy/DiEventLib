using System.Text.Json.Serialization;

namespace DvSceneLib.IO.Template;

public class DiEventDataBaseJSON
{
    public class BaseJSON
    {
        [JsonPropertyName("name"), JsonPropertyOrder(0)]
        public string Name { get; set; }
        [JsonPropertyName("descriptions"), JsonPropertyOrder(1)]
        public Dictionary<string, string>? Descriptions { get; set; }
    }

    public class EnumJSON
    {
        [JsonPropertyName("name"), JsonPropertyOrder(0)]
        public string Name { get; set; }
        [JsonPropertyName("values"), JsonPropertyOrder(1)]
        public Dictionary<string, int> Values { get; set; }
    }

    public class FlagJSON
    {
        [JsonPropertyName("values"), JsonPropertyOrder(1)]
        public List<string> Values { get; set; }
    }

    public class StructJSON
    {
        [JsonPropertyName("name"), JsonPropertyOrder(0)]
        public string Name { get; set; }
        [JsonPropertyName("fields"), JsonPropertyOrder(1)]
        public List<FieldJSON> Fields { get; set; }
    }

    public class FieldJSON : BaseJSON
    {
        [JsonPropertyName("type"), JsonPropertyOrder(2)]
        public string Type { get; set; }
        [JsonPropertyName("subtype"), JsonPropertyOrder(3)]
        public string? SubType { get; set; }
        [JsonPropertyName("size"), JsonPropertyOrder(4)]
        public int Size { get; set; }
        [JsonPropertyName("enum"), JsonPropertyOrder(5)]
        public EnumJSON? Enum { get; set; }
        [JsonPropertyName("flag"), JsonPropertyOrder(6)]
        public FlagJSON? Flag { get; set; }
        [JsonPropertyName("struct"), JsonPropertyOrder(7)]
        public StructJSON? Struct { get; set; }
        [JsonPropertyName("arraysizefield"), JsonPropertyOrder(8)]
        public string? ArraySizeField { get; set; }
    }

    public class NodeJSON : BaseJSON
    {
        [JsonPropertyName("fullname"), JsonPropertyOrder(2)]
        public string FullName { get; set; }
        [JsonPropertyName("id"), JsonPropertyOrder(3)]
        public int ID { get; set; }
        [JsonPropertyName("fields"), JsonPropertyOrder(4)]
        public List<FieldJSON> Fields { get; set; }
    }

    public class MainJSON
    {
        [JsonPropertyName("version"), JsonPropertyOrder(0)]
        public int Version { get; set; }
        [JsonPropertyName("hasDescriptions"), JsonPropertyOrder(1)]
        public bool HasDescriptions { get; set; }
        [JsonPropertyName("autoAlign"), JsonPropertyOrder(2)]
        public bool AutoAlign { get; set; }
        [JsonPropertyName("nodes"), JsonPropertyOrder(3)]
        public List<NodeJSON> Nodes { get; set; }
        [JsonPropertyName("elements"), JsonPropertyOrder(4)]
        public List<NodeJSON>? Elements { get; set; }
    }
}
