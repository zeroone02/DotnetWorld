namespace DotnetWorld.DDD;
public interface IMayHaveCreator<TCreatorKey>
{
    public TCreatorKey? CreatorId { get; set; }
}
