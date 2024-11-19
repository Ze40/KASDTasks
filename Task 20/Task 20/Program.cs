using MyLib;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Task20
{
    static class Project
    {
        private static string? SetPath(string name)
        {
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appDir + @"\" + name;
        }

        public enum VarType
        {
            INT,
            FLOAT,
            DOUBLE, 
        }

        static MyHashMap<string, (VarType, string)> GetVariable(string fileName) { 
            string? path = SetPath(fileName);
            if (path == null) return new MyHashMap<string, (VarType, string)>();

            MyHashMap<string, (VarType, string)> variables = new MyHashMap<string, (VarType, string)>();
            var regular = @"\b(int|float|double)\s+(\w+)\s+=\s+(-?\d+\.?\d*);";

            try
            {
                StreamReader reader = new StreamReader(path);
                string? line = reader.ReadLine();
                int linNumber = 1;
                while (line != null)
                {
                    if (Regex.Match(line, regular).Success)
                    {
                        Match match = Regex.Match(line, regular);
                        string varName = match.Groups[2].Value;
                        string varValue = match.Groups[3].Value;
                        string varTypeStr = match.Groups[1].Value.ToUpper();

                        if (Enum.TryParse(varTypeStr, true, out VarType varType))
                        {
                            if (variables.ContainKey(varName)) {
                                Console.WriteLine($"Variable '{varName}' was rewrite on: type '{varTypeStr}'; value '{varValue}'");
                            }
                            variables.Push(varName, (varType, varValue));
                        }
                    }else {
                        Console.WriteLine($"{linNumber}: {line}\tnot currect");
                    }
                    linNumber++;
                    line = reader.ReadLine();
                }
                reader.Close();
            }catch (Exception ex) { Console.WriteLine($"Messege: {ex.Message}"); }
            return variables;
        }
        static void WriteVarInFile(MyHashMap<string, (VarType, string)> map, string fileName)
        {
            string? path = SetPath(fileName);
            if (path == null) return;

            try
            {
                StreamWriter writer = new StreamWriter(path);
                (string, (VarType, string))[] variables = map.EntrySet();
                for (int i = 0; i < variables.Length; i++)
                {
                    writer.WriteLine($"{variables[i].Item2.Item1} {variables[i].Item1} = {variables[i].Item2.Item2};");
                }
                writer.Close();
            }catch (Exception ex) { Console.WriteLine($"Err Messegge: {ex.Message}"); }
        }

        static void Main()
        {
            MyHashMap<string, (VarType, string)> variables = GetVariable("input.txt");
            WriteVarInFile(variables, "output.txt");
        }
    }
}