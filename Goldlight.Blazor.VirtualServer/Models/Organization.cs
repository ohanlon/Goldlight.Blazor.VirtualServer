﻿using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

[DataContract]
public class Organization
{
  [DataMember(Name = "id")]
  public string? Id { get; set; }

  [DataMember(Name = "name")]
  public string? Name { get; set; }

  [DataMember(Name = "version")]
  public long? Version { get; set; }
}