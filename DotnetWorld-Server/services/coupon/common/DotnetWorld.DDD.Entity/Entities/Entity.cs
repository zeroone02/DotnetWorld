namespace DotnetWorld.DDD;
public abstract class Entity<T> : IEntity<T>
{
    public Entity() { }
    public Entity(T id)
    {
        Id = id;
    }

    public virtual T Id { get; set; }
}