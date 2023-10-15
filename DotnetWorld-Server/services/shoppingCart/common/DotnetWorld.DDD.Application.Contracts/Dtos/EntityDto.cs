namespace DotnetWorld.DDD.Application.Contracts;
public abstract class EntityDto
{
}

public abstract class EntityDto<TKey> : EntityDto
{
    public TKey Id { get; set; }
}
