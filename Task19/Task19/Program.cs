using System.Reflection;
using MyLib;

namespace Task19
{
    static class Project
    {
        private static string SetPath(string name)
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appDir + "/" + name;
        }

        private static MyHashMap<string, int> GetTegArrayFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string? line = sr.ReadLine();
            if (line == null) throw new Exception("Пустой файл");

            MyHashMap<string, int> tagList = new MyHashMap<string, int>();

            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 's', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'v', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' };
            char[] number = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            while (line != null)
            {
                bool isTag = true;
                bool isOpen = false;
                string tag = "";

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && i + 1 < line.Length)
                    {
                        if (line[i + 1] == '/' && i + 2 < line.Length)
                        {
                            isOpen = true;
                            isTag = alphabet.Contains(line[i + 2]);
                        }
                        else if (line[i + 1] == '/') isTag = false;
                        else
                        {
                            isTag = alphabet.Contains(line[i + 1]);
                            isOpen = true;
                        }
                    }
                    if (line[i] == '>' && isOpen)
                    {
                        isOpen = false;
                        tag += line[i];
                    }
                    else if (line[i] == '>')
                    {
                        isOpen = false;
                        isTag = false;
                    }
                    if (isTag == false) break;
                    if (line[i] != '<' && line[i] != '>' && line[i] != '/') isTag = alphabet.Contains(line[i]) || number.Contains(line[i]);
                    if (isTag && isOpen)
                    {
                        if (line[i] != '/')
                        {
                            tag += line[i];
                        }
                    }
                    if (isTag && !isOpen)
                    {
                        if (tagList.ContainKey(tag))
                        {
                            tagList.Push(tag, tagList.Get(tag) + 1);
                        }
                        else
                        {
                            tagList.Push(tag, 1);
                        }
                    }
                }

                line = sr.ReadLine();
            }
            return tagList;

        }

        static void Main()
        {
            MyHashMap<string, int> tags = GetTegArrayFromFile("file.txt");
            (string, int)[] tagsSet = tags.EntrySet();
            foreach((string name, int cnt) in tagsSet)
            {
                Console.WriteLine(name + ": " + cnt);
            }
        }
    }
}