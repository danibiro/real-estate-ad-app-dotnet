// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using bdim1996_dotnet.Controllers.Dto.Incoming;
using bdim1996_dotnet.Controllers.Dto.Outgoing;
using bdim1996_dotnet.Models.Entities;

namespace bdim1996_dotnet.Controllers.Mappers.Profiles;

public class RealEstateAdProfile : Profile
{
    public RealEstateAdProfile()
    {
        CreateMap<RealEstateAdCreationDto, RealEstateAd>();
        CreateMap<RealEstateAd, RealEstateAdDetailsDto>();
    }
}
