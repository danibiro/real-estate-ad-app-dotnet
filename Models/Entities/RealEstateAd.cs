// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using bdim1996_dotnet.Models.Attributes;

namespace bdim1996_dotnet.Models.Entities;

public class RealEstateAd : BaseModel
{
    [Required, StringLength(40)]
    public string Title { get; set; } = null!;

    [Required, StringLength(100)]
    public string Description { get; set; } = null!;

    [Required, StringLength(40)]
    public string Address { get; set; } = null!;

    [Required, Range(1, int.MaxValue)]
    public int Price { get; set; }

    [Required, PresentOrFuture]
    public DateOnly DateOfCreation { get; set; }

    [Required]
    public bool Negotiable { get; set; } = false;

    [Required, Range(1, int.MaxValue)]
    public int Area { get; set; }

    [Required, Range(1, long.MaxValue)]
    public long AgentId { get; set; }

    public RealEstateAgent Agent { get; set; } = null!;
}
