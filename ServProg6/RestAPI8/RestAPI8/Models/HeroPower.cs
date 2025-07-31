using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI8.Models;

public partial class HeroPower
{
    public int? HeroId { get; set; }

    public int? PowerId { get; set; }
    [JsonIgnore]
    public virtual Superhero? Hero { get; set; }
    [JsonIgnore]
    public virtual Superpower? Power { get; set; }
}
