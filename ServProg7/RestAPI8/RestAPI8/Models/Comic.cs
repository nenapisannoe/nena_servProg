using System;
using System.Collections.Generic;

namespace RestAPI8.Models;

public partial class Comic
{
    public int Id { get; set; }

    public string? ComicName { get; set; }

    public int? Issue { get; set; }

    public int? PublishMonth { get; set; }

    public int? PublishYear { get; set; }
}
