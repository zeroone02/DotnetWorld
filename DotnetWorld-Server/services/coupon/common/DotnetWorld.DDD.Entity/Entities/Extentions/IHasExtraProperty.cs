using DotnetWorld.DDD.Entities.Extentions;

namespace DotnetWorld.DDD.Entities;
public interface IHasExtraProperty
{
    ExtraPropertyDictionary ExtraProperties { get; }
}
