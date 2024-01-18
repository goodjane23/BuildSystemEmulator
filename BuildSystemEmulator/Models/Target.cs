
namespace BuildSystemEmulator.Models;
public class Target
{
    public string Name { get; set; }
    public List<string> Actions { get; set; }
    public List<Target> Dependencies { get; set; }

    public bool HasActions
    {
        get { return Actions != null && Actions.Any();}
    }

    public bool HasDependencies
    {
        get { return Dependencies != null && Dependencies.Any(); }
    }

    public bool IsComplete { get; set; }
 
    public Target(string name)
    {
        Actions = new List<string>();
        Dependencies = new List<Target>();
        IsComplete = false;
        Name = name;
    }

    public void Output()
    {
        Console.WriteLine($"Выполняемая задача: {Name}");

        foreach (var action in Actions)
        {
            Console.WriteLine(action);
        }
        IsComplete = true;
    }
}
