using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class TreasureObject
{
    [JsonProperty("objectIdFieldName")]
    public string ObjectIdFieldName { get; set; }

    [JsonProperty("uniqueIdField")]
    public UniqueIdField UniqueIdField { get; set; }

    [JsonProperty("globalIdFieldName")]
    public string GlobalIdFieldName { get; set; }

    [JsonProperty("serverGens")]
    public ServerGens ServerGens { get; set; }

    [JsonProperty("geometryType")]
    public string GeometryType { get; set; }

    [JsonProperty("spatialReference")]
    public SpatialReference SpatialReference { get; set; }

    [JsonProperty("fields")]
    public List<Field> Fields { get; set; }

    [JsonProperty("exceededTransferLimit")]
    public bool ExceededTransferLimit { get; set; }

    [JsonProperty("features")]
    public List<Feature> Features { get; set; }
}

public partial class Feature
{
    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; }
}

public partial class Attributes
{
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("timestamp")]
    public string Timestamp { get; set; }

    [JsonProperty("collected_coins")]
    public long CollectedCoins { get; set; }

    [JsonProperty("OBJECTID")]
    public long Objectid { get; set; }
}

public partial class Field
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("alias")]
    public string Alias { get; set; }

    [JsonProperty("sqlType")]
    public string SqlType { get; set; }

    [JsonProperty("domain")]
    public object Domain { get; set; }

    [JsonProperty("defaultValue")]
    public object DefaultValue { get; set; }

    [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
    public long? Length { get; set; }
}

public partial class ServerGens
{
    [JsonProperty("minServerGen")]
    public long MinServerGen { get; set; }

    [JsonProperty("serverGen")]
    public long ServerGen { get; set; }
}

public partial class SpatialReference
{
    [JsonProperty("wkid")]
    public long Wkid { get; set; }

    [JsonProperty("latestWkid")]
    public long LatestWkid { get; set; }
}

public partial class UniqueIdField
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("isSystemMaintained")]
    public bool IsSystemMaintained { get; set; }
}