using MyLib;
using System.Reflection;

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
                    string?[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word == null || word == " ") continue;
                        answer.Add(word.ToLower());
                    }
                    line = sr.ReadLine();
                }
                sr.Close();

            }
            catch (Exception ex) { Console.WriteLine("Exeption: " + ex.Message); };
            return answer;
        }


        static void Main(string[] args)
        {
            MyHashSet<string> set = GetStringFromFile("input.txt");
            foreach (string word in set.ToArray()) Console.WriteLine(word);
        }
    }
}