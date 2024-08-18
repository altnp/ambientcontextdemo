using AmbientContextDemo;

var initial = AmbientContextAccessor.Current;
AmbientContextAccessor.Current = new AmbientContext().ForCarrier(0);
//Persists across async
await new Logic().DoSomething(0);
await new Logic().DoSomethingElse(0);
var current = AmbientContextAccessor.Current;
Console.WriteLine(initial);
Console.WriteLine(current);

var threads = new Thread[3];
for (var i = 0; i < threads.Length; i++)
{
    threads[i] = new Thread(async () =>
        {
            Console.WriteLine(AmbientContextAccessor.Current);

            AmbientContextAccessor.Current.ForCarrier(Thread.CurrentThread.ManagedThreadId);

            await new Logic().DoSomething(Thread.CurrentThread.ManagedThreadId);
            await new Logic().DoSomethingElse(Thread.CurrentThread.ManagedThreadId);

            var current = AmbientContextAccessor.Current;
            current.Serialize();
            Console.WriteLine(current);
        }
    );

    threads[i].Name = $"Thread-{i + 1}";
    threads[i].UnsafeStart(); //don't capture execution context
}

foreach (var thread in threads)
{
    thread.Join();
}

Console.WriteLine(AmbientContextAccessor.Current);

Console.ReadKey();
