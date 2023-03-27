// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using bdim1996_dotnet.Models.Attributes;

namespace bdim1996_dotnet.Controllers.Dto.Incoming;

public class RealEstateAgentCreationDto
{
    [Required, MaxLength(40)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(40)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(40)]
    public string Phone { get; set; } = null!;

    [Required, MaxLength(40)]
    public string Address { get; set; } = null!;

    [Required, Range(1, int.MaxValue)]
    public int Age { get; set; }
}
