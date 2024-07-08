﻿using System;
using System.Collections.Generic;

namespace REMS.Database.AppDbContextModels;

public partial class PropertyImage
{
    public int ImageId { get; set; }

    public int? PropertyId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateUploaded { get; set; }

    public virtual Property? Property { get; set; }
}
