namespace DotnetWorld.DDD.Entities.Audited;

public class AuditedAggregateRoot<TKey, TCreatorKey> : 
    AggregateRoot<TKey>,
    IHasCreationTime,
    IMayHaveCreator<TCreatorKey>
{
    public AuditedAggregateRoot(TKey id) : base(id)
    {
        CreationTime = DateTime.Now.ToUniversalTime();
    }
    public AuditedAggregateRoot() : base()
    {
        CreationTime = DateTime.Now.ToUniversalTime();
    }

    public virtual DateTime CreationTime { get; set; }
    public virtual TCreatorKey CreatorId { get; set; }
}
