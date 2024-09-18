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

                if (line[0] != '<' || line[line.Length-1] != '>')
                {
                    isTag = false;
                    line = sr.ReadLine();
                    continue;
                }

                for (int i = 1; i<line.Length - 1; i++)
                {
                    if (i == 1)
                    {
                        if (line[i] == '/') isTag = alphabet.Contains(line[i+1]);
                        else isTag = alphabet.Contains(line[i]);
                        continue;
                    }
                    if (isTag == false) break;
                    isTag = alphabet.Contains(line[i]) || number.Contains(line[i]);
                }

                if (isTag) tagList.Add(line);
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