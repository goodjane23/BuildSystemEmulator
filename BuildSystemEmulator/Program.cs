using BuildSystemEmulator;

internal class Program
{
    private static void Main(string[] args)
    {
        var targets = Parser.GetTargets();

        if (args.Length != 1) 
        {
            Console.WriteLine("Неверный формат ввода");        
        }
        var fTask = targets.FirstOrDefault(x => x.Name.Equals(args[0]));
        if (fTask is null)
        {
            Console.WriteLine("Задачи не существует в makefile");            
        }

        if (fTask.HasDependencies)
        {
            foreach (var dependency in fTask.Dependencies)
            {
                if (dependency.HasActions && !dependency.IsComplete)
                    dependency.Output();
            }
        }

        if (fTask.HasActions && !fTask.IsComplete)
            fTask.Output();

        Console.ReadLine();
    }
}