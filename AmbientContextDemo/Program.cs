using AmbientContextDemo;

// AmbientContextAccessor.Current = new AmbientContext().ForCarrier(1);
//
// await new Logic().DoSomething(1);
// await new Logic().DoSomethingElse(1);
// //Persists across async
// var current = AmbientContextAccessor.Current;

Thread[] threads = new Thread[3];

for (int i = 0; i < threads.Length; i++)
{
    threads[i] = new Thread(async () =>
        {
            Console.WriteLine(AmbientContextAccessor.Current);

            AmbientContextAccessor.Current.ForCarrier(Thread.CurrentThread.ManagedThreadId);

            await new Logic().DoSomething(1);
            await new Logic().DoSomethingElse(1);

            var current = AmbientContextAccessor.Current;
            current.Serialize();
            Console.WriteLine(current);
        }
    );
    threads[i].Name = $"Thread-{i + 1}";
    threads[i].Start();
}

foreach (Thread thread in threads)
{
    thread.Join();
}

Console.ReadKey();
