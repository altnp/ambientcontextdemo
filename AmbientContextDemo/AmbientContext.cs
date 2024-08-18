using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AmbientContextDemo;

public class AmbientContext
{
    public Dictionary<string, object> State { get; } = new();

    public AmbientContext ForCarrier(int carrierId)
    {
        State.TryAdd("mycontext.carrierId", carrierId);
        return this;
    }

    public AmbientContext ForSim(string sim)
    {
        State.TryAdd("mycontext.sim", sim);
        return this;
    }

    public AmbientContext ForAccount(int accountid)
    {
        State.TryAdd("mycontext.accountId", accountid);
        return this;
    }

    public override string ToString()
    {
        if (State.Keys.Count == 0)
            return "empty";

        var sb = new StringBuilder();
        foreach (var kvp in State)
        {
            sb.Append($"{kvp.Key}: {kvp.Value}, ");
        }

        return sb.ToString();
    }

    //Testing for header propagation.
    public string Serialize()
    {
        var json = JsonSerializer.Serialize(State);
        //not ideal how it deserialized into JsonElements...
        var deserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        return json;
    }
}

public static class AmbientContextAccessor
{
    // ReSharper disable once InconsistentNaming
    private static readonly AsyncLocal<AmbientContext> _current = new AsyncLocal<AmbientContext>();

    public static AmbientContext Current
    {
        get => _current.Value ?? (_current.Value = new AmbientContext());
        set => _current.Value = value;
    }
}
