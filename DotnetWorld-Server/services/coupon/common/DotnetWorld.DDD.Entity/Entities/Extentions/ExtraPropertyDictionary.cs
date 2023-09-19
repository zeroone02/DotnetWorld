namespace DotnetWorld.DDD;

[Serializable]
public class ExtraPropertyDictionary : Dictionary<string, object>
{
    public ExtraPropertyDictionary() : base()
    {

    }

    public ExtraPropertyDictionary(Dictionary<string, object> dictionary) : base(dictionary)
    {

    }
}
