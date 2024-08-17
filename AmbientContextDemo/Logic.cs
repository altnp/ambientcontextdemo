namespace AmbientContextDemo;

public class Logic
{
    public async Task DoSomething(int a)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
        AmbientContextAccessor.Current.ForSim(Thread.CurrentThread.ManagedThreadId.ToString());
    }

    public async Task DoSomethingElse(int b)
    {

        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
        AmbientContextAccessor.Current.ForAccount(Thread.CurrentThread.ManagedThreadId);
    }
}
