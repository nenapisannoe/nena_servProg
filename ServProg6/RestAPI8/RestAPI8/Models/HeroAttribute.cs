using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI8.Models;

public partial class HeroAttribute
{
    public int? HeroId { get; set; }

    public int? AttributeId { get; set; }

    public int? AttributeValue { get; set; }
    [JsonIgnore]
    public virtual Attribute? Attribute { get; set; }
    [JsonIgnore]
    public virtual Superhero? Hero { get; set; }
}
