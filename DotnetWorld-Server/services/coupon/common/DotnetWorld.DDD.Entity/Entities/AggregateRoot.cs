namespace DotnetWorld.DDD.Entities;

public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    public AggregateRoot(TKey id): base(id) { }
    public AggregateRoot() { }
}