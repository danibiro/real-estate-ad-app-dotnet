// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace bdim1996_dotnet.Models.Entities;

[SerializableAttribute]
public abstract class BaseModel
{
    public long Id { get; set; }
}
