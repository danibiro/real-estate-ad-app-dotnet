// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace bdim1996_dotnet.Controllers.Dto.Outgoing;

public class RealEstateAdDetailsDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int Price { get; set; }
    public DateOnly DateOfCreation { get; set; }
    public bool Negotiable { get; set; } = false;
    public int Area { get; set; }
    public RealEstateAgentDetailsDto Agent { get; set; } = null!;
}
