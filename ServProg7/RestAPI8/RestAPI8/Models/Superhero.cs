using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI8.Models;

public partial class Superhero
{
    public int Id { get; set; }

    public string? SuperheroName { get; set; }

    public string? FullName { get; set; }

    public int? GenderId { get; set; }

    public int? EyeColourId { get; set; }

    public int? HairColourId { get; set; }

    public int? SkinColourId { get; set; }

    public int? RaceId { get; set; }

    public int? PublisherId { get; set; }

    public int? AlignmentId { get; set; }

    public int? HeightCm { get; set; }

    public int? WeightKg { get; set; }
    [JsonIgnore]
    public virtual Alignment? Alignment { get; set; }
    [JsonIgnore]
    public virtual Colour? EyeColour { get; set; }
    [JsonIgnore]
    public virtual Gender? Gender { get; set; }
    [JsonIgnore]
    public virtual Colour? HairColour { get; set; }
    [JsonIgnore]
    public virtual Publisher? Publisher { get; set; }
    [JsonIgnore]
    public virtual Race? Race { get; set; }
    [JsonIgnore]
    public virtual Colour? SkinColour { get; set; }
}
