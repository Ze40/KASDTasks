using System;
using System.Reflection;
using MyArrayListLib;

namespace Task7
{
    class Project
    {
        private static string SetPath(string name)
        {
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appDir + @"\" + name;
        }
        public static string[] GetIPFromFile(string? name)
        {
            string path = SetPath(name);
            MyArrayList<string> ipList = new MyArrayList<string>();

            try
            {
                StreamReader sr = new StreamReader(path);
                string? line = sr.ReadLine();
                while (line != null)
                {
                    string[] ipArray = line.Split(' ');
                    foreach (string ip in ipArray)
                    {
                        bool isIp = true;
                        int[] ipBlock = ip.Split('.').Select(x => Convert.ToInt32(x)).ToArray();
                        foreach (int block in ipBlock)
                        {
                            if (!(block >= 0 && block <= 255)) isIp = false;
                        }

                        if (isIp && ipBlock.Length == 4) ipList.Add(ip);
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return ipList.ToArray();
        }
        public static void SetIPToFile(string? name, string[] array)
        {
            string path = SetPath(name);
            try
            {
                StreamWriter sw = new StreamWriter(path);
                foreach (string ip in array) sw.WriteLine(ip);
                sw.Close();
            }catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        static void Main(string[] args)
        {
            string[] ipList = GetIPFromFile("input.txt");
            SetIPToFile("output.txt", ipList);
        }
    }
}