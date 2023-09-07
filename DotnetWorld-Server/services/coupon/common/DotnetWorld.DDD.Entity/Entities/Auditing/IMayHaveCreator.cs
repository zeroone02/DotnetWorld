namespace DotnetWorld.DDD.Entities.Audited;
public interface IMayHaveCreator<TCreatorKey>
{
    public TCreatorKey? CreatorId { get; set; }
}
