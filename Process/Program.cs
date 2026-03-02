using System.Diagnostics;
using System.Text;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();

            var processes = Process.GetProcesses().OrderBy(p => p.ProcessName);

            foreach (Process process in processes)
            {
                try
                {
                    Console.WriteLine($"Id: {process.Id} \t Name: {process.ProcessName} \t Priority: {process.PriorityClass}");
                }
                catch
                {
                    Console.WriteLine($"Id: {process.Id} \t Access denied");
                }
            }

            Console.WriteLine("\nEnter Id of a process (0 to update):");

            int processId;

            try
            {
                processId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong number");
                continue;
            }

            if (processId == 0)
                continue;

            Process selectedProcess;

            try
            {
                selectedProcess = Process.GetProcessById(processId);
            }
            catch
            {
                Console.WriteLine("Process not found");
                continue;
            }

            Console.WriteLine("\nChoose an action");
            Console.WriteLine("1. End a process");
            Console.WriteLine("2. Change the priority");

            char action = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (action == '1')
            {
                try
                {
                    selectedProcess.Kill();
                    Console.WriteLine("The process is over");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ending error: {ex.Message}");
                }
            }
            else if (action == '2')
            {
                Console.WriteLine("1. High");
                Console.WriteLine("2. Normal");
                Console.WriteLine("3. Idle");

                char prio = Console.ReadKey().KeyChar;
                Console.WriteLine();

                try
                {
                    switch (prio)
                    {
                        case '1':
                            selectedProcess.PriorityClass = ProcessPriorityClass.High;
                            break;
                        case '2':
                            selectedProcess.PriorityClass = ProcessPriorityClass.Normal;
                            break;
                        case '3':
                            selectedProcess.PriorityClass = ProcessPriorityClass.Idle;
                            break;
                        default:
                            Console.WriteLine("Wrong choice");
                            break;
                    }

                    Console.WriteLine("Priority changed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Changing priority error: {ex.Message}");
                }
            }

            Console.WriteLine("\n10 sec update");
            Thread.Sleep(10000);
        }
    }
}