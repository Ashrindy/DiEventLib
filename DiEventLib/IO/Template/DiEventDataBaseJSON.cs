using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiEventLib.IO.Template;

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
        [JsonPropertyName("struct"), JsonPropertyOrder(6)]
        public StructJSON? Struct { get; set; }
        [JsonPropertyName("arraysizefield"), JsonPropertyOrder(7)]
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
        [JsonPropertyName("nodes"), JsonPropertyOrder(2)]
        public List<NodeJSON> Nodes { get; set; }
        [JsonPropertyName("elements"), JsonPropertyOrder(3)]
        public List<NodeJSON>? Elements { get; set; }
    }
}
