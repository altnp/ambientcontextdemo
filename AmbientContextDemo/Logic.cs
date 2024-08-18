namespace AmbientContextDemo;

public class Logic
{
    public async Task DoSomething(int a)
    {
        AmbientContextAccessor.Current.ForSim(a.ToString());
    }

    public async Task DoSomethingElse(int b)
    {
        AmbientContextAccessor.Current.ForAccount(b);
    }
}
