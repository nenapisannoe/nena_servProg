using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI8.Models;

public partial class Alignment
{
    public int Id { get; set; }

    public string? AlignmentName { get; set; }

}
