using System.Diagnostics;

namespace Server;
public class Program
{
    class UpCounter
    {
        public void CountUp(int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("\n Count-Up starts");
            for (int i =1; i <= count; i++)
            {
                Console.Write($"i = {i}, ");
            }
            stopwatch.Stop();

            Console.WriteLine($"\nCount-Up ends: {stopwatch.ElapsedMilliseconds}");
        }
        public void CountDown(int count)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("\n Count-Down starts");
            for (int i = count; i>0; i--)
            {
                Console.Write($"j = {i}, ");
            }
            stopwatch.Stop();

            Console.WriteLine($"\nCount-down ends: {stopwatch.ElapsedMilliseconds}");
        }
    }
    static public void Th()
    {
        UpCounter up = new UpCounter();
        CountdownEvent count = new CountdownEvent(2);

        Thread t1 = new Thread(() =>
        {
            up.CountDown(20);
            count.Signal();
        });
        Thread t2 = new Thread(() =>
        {
            up.CountUp(20);
            count.Signal();
        });
        t1.Start();
        t2.Start();

        count.Wait();
    }
    static public void Ts()
    {
        UpCounter up = new UpCounter();
        CountdownEvent count = new CountdownEvent(2);

        Task task1 = Task.Run(() => { up.CountDown(20); count.Signal(); });
        Task task2 = Task.Run(() => { up.CountUp(20); count.Signal(); });

        count.Wait();
    }
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        Ts();
        stopwatch.Stop();
        Console.WriteLine($"Tasks: {stopwatch.ElapsedMilliseconds}");
        stopwatch.Restart();
        Th();
        stopwatch.Stop();
        Console.WriteLine($"Threads: {stopwatch.ElapsedMilliseconds}");
        Console.WriteLine("\nCounting ended");
    }
}
