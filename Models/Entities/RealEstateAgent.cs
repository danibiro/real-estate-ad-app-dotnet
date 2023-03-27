// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace bdim1996_dotnet.Models.Entities;

public class RealEstateAgent : BaseModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int Age { get; set; }
    public ICollection<RealEstateAd> RealEstateAds { get; set; } = null!;
}
