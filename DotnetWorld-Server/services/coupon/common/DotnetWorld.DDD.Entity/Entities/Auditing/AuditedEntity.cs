namespace DotnetWorld.DDD.Entities.Auditing;
public abstract class AuditedEntity<TKey, TCreatorKey> : 
    Entity<TKey>,
    IHasCreationTime,
    IMayHaveCreator<TCreatorKey>
{
    public AuditedEntity()
    {
        CreationTime = DateTime.Now.ToUniversalTime();
    }
    public virtual TCreatorKey? CreatorId { get; set; }
    public virtual DateTime CreationTime { get; set; }
}