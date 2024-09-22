using System;
using MyLib;

namespace Task9
{

    class Project
    {
        static public int WeightOfOperator(string opertator)
        {
            switch(opertator)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "//":
                    return 2;
                case "^":
                    return 3;
                case "sqrt":
                case "ln":
                case "cos":
                case "sin":
                case "tg":
                case "ctg":
                case "abs":
                case "log":
                case "min":
                case "max":
                case "mod":
                case "exp":
                case "trunc":
                    return 4;
                case "%":
                    return 5;
                default: return 0;
            }
        }

        static public double CalculateByFunc(string opertator, params double[] x) { 
            switch(opertator)
            {
                case "+":
                    return x[0] + x[1];
                case "-":
                    return x[0] - x[1];
                case "*":
                    return x[0] * x[1];
                case "/":
                    return x[0] / x[1];
                case "//":
                    return Math.Floor(x[0] / x[1]);
                case "^":
                    return Math.Pow(x[0], x[1]);
                case "exp":
                    return Math.Exp(x[0]);
                case "sqrt":
                    return Math.Sqrt(x[0]);
                case "ln":
                    return Math.Log(x[0]);
                case "log":
                    return Math.Log10(x[0]);
                case "cos":
                    return Math.Cos(x[0]);
                case "sin":
                    return Math.Sin(x[0]);
                case "tg":
                    return Math.Tan(x[0]);
                case "ctg":
                    return 1 / Math.Tan(x[0]);
                case "abs":
                    return Math.Abs(x[0]);
                case "min":
                    return x[0] < x[1] ? x[0] : x[1];
                case "max":
                    return x[0] > x[1] ? x[0] : x[1];
                case "mod":
                    if ((int)x[0] != x[0] || (int)x[1] != x[1]) throw new Exception("Нельзя искать остаток от деления от не целых чисел");
                    return (int)x[0] % (int)x[1];
                case "trunc":
                    return Math.Truncate(x[0]);

                default: throw new NotImplementedException("Неверное выражение: " + opertator);
            }

        } 

        static private string ReplaceVar(string expression)
        {
            char[] alphabet = new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            
            MyVector<string> variableVector = new MyVector<string>();

            int n = expression.Length;
            int i = 0;
            while (i < n)
            {
                string variable = "";
                while (i < n && alphabet.Contains(expression[i]))
                {
                    variable += expression[i];
                    i++;
                }
                if (variable.Length > 0)
                {
                    switch (variable)
                    {
                        case "sqrt":
                        case "ln":
                        case "cos":
                        case "sin":
                        case "tg":
                        case "ctg":
                        case "abs":
                        case "log":
                        case "min":
                        case "max":
                        case "mod":
                        case "exp":
                        case "trunc":
                            break;
                        default:
                            variableVector.Add(variable);
                            break;
                    }
                }
                i++;
            }

            for (i = 0; i < variableVector.Size(); i++)
            {
                Console.WriteLine("Введте переменную " + variableVector.Get(i) + ": ");
                expression = expression.Replace(variableVector.Get(i), Console.ReadLine());
            }
            return expression;
        }

        static public MyVector<string> ToPostfixForm(string expression) {
            expression = ReplaceVar(expression);
            MyStack<string> stack = new MyStack<string>();
            MyVector<string> answer = new MyVector<string>();

            int n = expression.Length;
            char[] digitals = new char[] {'0','1','2','3','4','5','6','7','8','9','.' };
            char[] alphabet = new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            char[] basicOperator = new char[] { '+', '-', '*', '^', '/', '%' };

            int i = 0;
            while (i < n)
            {
                // Добавляем число
                string number = "";
                if ((answer.IsEmpty() && expression[i] == '-') ||(i>0 && expression[i] == '-' && expression[i-1] == '('))
                {
                    number += expression[i];
                    i++;
                }
                while (i < n && digitals.Contains(expression[i]))
                {
                    number += expression[i];
                    i++;
                }
                if (number.Length > 0) { answer.Add(number); }

                //Добавляем функцию

                string func = "";
                while (i < n && alphabet.Contains(expression[i]))
                {
                    func += expression[i];
                    i++;
                }
                if (func.Length> 0 && func == "pi") answer.Add("3.14");
                else if (func.Length>0) { stack.Push(func); }

                //Добавляем простые операторы
                if (i < n && basicOperator.Contains(expression[i])) {
                    if (stack.Empty()) { stack.Push(expression[i].ToString()); }
                    else
                    {
                        while(!stack.Empty() && (WeightOfOperator(stack.Peek()) > WeightOfOperator(expression[i].ToString())))
                        {
                            string c = stack.Peek();
                            answer.Add(c.ToString());
                            stack.Pop();
                        }
                        stack.Push(expression[i].ToString());
                    }
                } else if (i < n && expression[i] == '(') stack.Push(expression[i].ToString());
                else if (i < n && expression[i] ==  ')')
                {
                    while (!stack.Empty()) { 
                        string c = stack.Peek();
                        if (c == "(")
                        {
                            stack.Pop();
                            break;
                        }
                        answer.Add(c.ToString());
                        stack.Pop();
                    }
                }
                i++;
            }
            while (!stack.Empty())
            {
                string c = stack.Peek();
                if (c != "(") answer.Add(c.ToString());
                if (c == ")") throw new Exception("Некоректный ввод, не свопадает количество скобок: " + c);
                stack.Pop();
            }

            return answer;
        }

        static public double CalculateExpression(string expression)
        {
            if (expression == null) throw new ArgumentNullException("Нет выражения");
            MyVector<string> postfixForm = ToPostfixForm(expression);
            MyStack<double> stack = new MyStack<double>();

            char[] digitals = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] alphabet = new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            char[] basicOperator = new char[] { '+', '-', '*', '^', '/', 'm'};

            int n = postfixForm.Size();
            int i;
            for (i = 0; i < n;)
            {
                string element = postfixForm.Get(i);
                if (digitals.Contains(element[0]) || (element.Length>1 && digitals.Contains(element[1]))) {
                    element = element.Replace('.', ',');
                    double number = Convert.ToDouble(element);
                    stack.Push(number);
                }
                else if (alphabet.Contains(element[0]))
                {
                    if (element == "max" ||  element == "min" || element == "mod")
                    {
                        double number1 = stack.Peek();
                        stack.Pop();
                        double number2 = stack.Peek();
                        stack.Pop();
                        stack.Push(CalculateByFunc(element, number1, number2));
                    }
                    else { 
                        double number = stack.Peek();
                        stack.Pop();
                        stack.Push(CalculateByFunc(element, number));
                    }
                }
                else if (basicOperator.Contains(element[0]))
                {
                    if (i < n-1 && element == "/" && postfixForm.Get(i+1) == "/")
                    {
                        i++;
                        element+= postfixForm.Get(i);
                    }
                    double number2 = stack.Peek();
                    stack.Pop();
                    double number1 = stack.Peek();
                    stack.Pop();
                    stack.Push(CalculateByFunc(element, number1, number2));
                }
                else if (element[0] == '%')
                {
                    double number2 = stack.Peek();
                    stack.Pop();
                    double number1 = stack.Peek();
                    double percent = number1 * number2 / 100;
                    stack.Push(percent);
                }
                i++;
            }
            double answer = stack.Peek();
            stack.Pop();
            return answer;
        }

        static void Main(string[] args)
        {
            string? expression = Console.ReadLine();
            Console.WriteLine(CalculateExpression(expression));
        }
    }
}
