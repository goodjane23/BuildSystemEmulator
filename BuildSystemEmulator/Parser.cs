using BuildSystemEmulator.Models;

namespace BuildSystemEmulator;
public class Parser
{
    private static Target? t;
    private static List<Target> targets = new List<Target>();
    
    public static List<Target> GetTargets()
    {
        try
        {
            var makefile = File.ReadAllLines("makefile");
            Parse(makefile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошбика при чтении файла makefile: {ex.Message} ");
        }
        
        return targets;
    }

    private static void Parse(string[] makefile)
    {
        foreach (var line in makefile)
        {
            if (line.StartsWith(' '))
            {
                if (t is null)
                {
                    Console.WriteLine("Не существует задачи для действия. Проверьте корректность файла");
                    return;
                }
                t.Actions.Add(line);
                continue;
            }
            if (line.Contains(':'))
            {
                var arr = line.Split(' ');
                var name = arr[0].Substring(0, arr[0].Length - 1);
                t = FindOrCreate(name);

                for (int i = 1; i < arr.Length; i++)
                {
                    var td = FindOrCreate(arr[i]);
                    t.Dependencies.Add(td);
                }
                targets.Add(t);
                continue;
            }
            else
            {
                t = FindOrCreate(line);
                targets.Add(t);
                continue;
            }

        }
    }
    private static Target FindOrCreate(string name)
    {
        Target? tFind = null;
        foreach (var t in targets)
        {
            foreach (var td in t.Dependencies)
            {
                if (td.Name == name)
                {
                    tFind = td;
                    break;
                }
            }
        }

        if (tFind is null)
            tFind = new Target(name);

        return tFind;
    }
}
