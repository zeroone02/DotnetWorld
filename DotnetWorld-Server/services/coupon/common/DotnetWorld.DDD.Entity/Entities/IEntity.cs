﻿namespace DotnetWorld.DDD.Entities;
public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
