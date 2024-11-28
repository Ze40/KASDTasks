using System.Reflection;
using MyLib;

namespace Task25
{
    class Program
    {
        static public string SetPath(string name)
        {
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(appDir, name);
        }

        static public MyHashSet<string> GetStringFromFile(string name)
        {
            string? path = SetPath(name);
            if (path == null) throw new Exception("path is null");

            MyHashSet<string> answer = new MyHashSet<string>();

            try
            {
                StreamReader sr = new StreamReader(path);
                string? line = sr.ReadLine();
                while (line != null)
                {
                    answer.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

            } catch (Exception ex) { Console.WriteLine("Exeption: " + ex.Message); };
            return answer;
        }

        static public void Sorting(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int indexOfmin = i;
                string? minString = array[i];
                for (int j = i+1; j < array.Length; j++)
                {
                    string? current = array[j];
                    bool ifChange = false;
                    for (int k = 0; k < current.Length && k < minString.Length; k++)
                    {
                        if (current[k] == ' ' && minString[k] != ' ') { 
                            minString = current;
                            indexOfmin = j;
                            ifChange = true;
                            break;
                        }
                        if (current[k] != ' ' && minString[k] == ' ')
                        {
                            ifChange = true;
                            break;
                        }
                    }
                    if (!ifChange)
                    {
                        minString = minString.Length < current.Length ? minString : current;
                        indexOfmin = minString.Length < current.Length ? indexOfmin : j;
                    }
                }
                array[indexOfmin] = array[i];
                array[i] = minString;
            }
        }

        static void Main()
        {
            MyHashSet<string> stringSet = GetStringFromFile("input.txt");
            string[] stringArray = stringSet.ToArray();
            Sorting(stringArray);

            foreach(string str in stringArray) Console.WriteLine(str);

            Console.WriteLine(SetPath("input.txt"));
        }
    }
}