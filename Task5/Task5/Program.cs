using System;
using System.Reflection;

namespace Task5
{
    static public class Program
    {
        private static string SetPath(string name)
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appDir + "/" + name;
        }

        private static string[] GetTegArrayFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string? line = sr.ReadLine();
            if (line == null) throw new Exception("Пустой файл");

            List<string> tagList = new List<string>();

            char[] alphabet = new char[] {'a','b','c','d','q','w','e','r','t','y','u','i','o','p','s','f','g','h','j','k','l','z','x','v','n','m','Q','W','E','R','T','Y','U', 'I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M' };
            char[] number = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            while (line != null) 
            { 
                bool isTag = true;
                bool isOpen = false;
                string tag = "";

                for (int i = 0; i<line.Length; i++)
                {
                    if (line[i] == '<' && i+1 < line.Length)
                    {
                        if (line[i+1] == '/' && i+2 < line.Length)
                        {
                            isOpen = true;
                            isTag = alphabet.Contains(line[i+2]);
                        } else if (line[i+1] == '/') isTag = false;
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
                    if (isTag && isOpen) tag += line[i];
                    if (isTag && !isOpen) tagList.Add(tag);
                }

                line = sr.ReadLine();
            }
            return tagList.ToArray();

        }
        private static string[] DeleteDublicateInTag(string[] tagArray) 
        {
            string[] lowerCaseTeg = new string[tagArray.Length];
            for (int i = 0; i < lowerCaseTeg.Length; i++) lowerCaseTeg[i] = tagArray[i].ToLower();

            int dublicateCount = 0;
            for (int i = 0; i < lowerCaseTeg.Length; i++)
            {
                for (int j = i + 1; j < lowerCaseTeg.Length; j++)
                {
                    if (lowerCaseTeg[i] == lowerCaseTeg[j])
                    {
                        lowerCaseTeg[j] = "false";
                        dublicateCount++;
                    }
                }
            }

            string[] answerArray = new string[tagArray.Length-dublicateCount];
            int index = 0;
            for (int i = 0;i < tagArray.Length;i++) 
            {
                if (lowerCaseTeg[i] == "false") continue;
                answerArray[index++] = tagArray[i];
            }

            return answerArray;
        }

        static void Main(string[] args)
        {
            string[] tegArray = null;
            try
            {
                string path = SetPath("input.txt");
                tegArray = GetTegArrayFromFile(path);
            }catch (Exception ex) { Console.WriteLine(ex); };

            string[] answerArray = DeleteDublicateInTag(tegArray);
            for (int i = 0; i < tegArray.Length; i++) Console.WriteLine(i + ": " + answerArray[i]); 
        }
    }
}
