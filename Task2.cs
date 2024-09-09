using System;

namespace Task2
{
    //Класс комплексных
    class Complex
    {
        private double real;
        private double imaginary;

        public Complex(double real = 0, double imaginary = 0) { 
            this.real = real;
            this.imaginary = imaginary;
        }

        //Гетеры и сетеры
        public double[] GetComplex()
        {
            return new double[] {this.real, this.imaginary};
        }

        public double GetReal() { return this.real; }
        public double GetImaginary() {  return this.imaginary; }

        public void SetComplex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        //Вывод
        public void Print()
        {
            Console.WriteLine("("+this.real + ", " + this.imaginary + "i)");
        }
    }

    class Program
    {
        //Нахождение суммы
        static Complex SumComplex(Complex complexFirst, Complex complexSecond)
        {
            double sumReal = complexFirst.GetReal()+complexSecond.GetReal();
            double sumImaginary = complexFirst.GetImaginary() + complexSecond.GetImaginary();
            
            return new Complex(sumReal, sumImaginary);
        }

        //Нахождение разницы
        static Complex SubstrucctionComplex(Complex complexFirst, Complex complexSecond)
        {
            double subReal = complexFirst.GetReal() - complexSecond.GetReal();
            double subImaginary = complexFirst.GetImaginary() - complexSecond.GetImaginary();

            return new Complex(subReal, subImaginary);
        }

        //Нахождение произведения
        static Complex MultiplicationComplex(Complex complexFirst, Complex complexSecond)
        {
            double multipReal = complexFirst.GetReal() * complexSecond.GetReal() - complexFirst.GetImaginary()*complexSecond.GetImaginary();
            double multiImaginary = complexFirst.GetReal() * complexSecond.GetImaginary() + complexFirst.GetImaginary() * complexSecond.GetReal();
            return new Complex(multipReal, multiImaginary);
        }

        //Нахождение отношения
        static Complex DivisionComplex(Complex complexFirst, Complex complexSecond)
        {
            double a = complexFirst.GetReal();
            double b = complexFirst.GetImaginary();
            double c = complexSecond.GetReal();
            double d = complexSecond.GetImaginary();
            double divReal = (a*c+b*d)/(c*c+d*d);
            double divImaginary = (b*c-a*d) / (c*c+d*d);
            return new Complex(divReal, divImaginary);
        }

        //Нахождение модуля
        static double ModuleOfComplex(Complex complex)
        {
            return Math.Sqrt(complex.GetReal() * complex.GetReal() + complex.GetImaginary() * complex.GetImaginary());
        }

        //Нахождение аргумента
        static double ArgumentOfComplex(Complex complex)
        {
            return Math.Atan(complex.GetImaginary() / complex.GetReal());
        }

        static void Main()
        {
            //Инициализация необходимых переменных
            string? symbol;
            string? number;
            Complex complexFirst = new Complex();
            Complex complexSecond = new Complex();

            //Бесконечный цикл программы
            while (true)
            {
                Console.WriteLine("1. Create complex number\n" +
                    "2. Sum of complex number\n" +
                    "3. Substruction of complex number\n" +
                    "4. Multiplication of complex number\n" +
                    "5. Division of complex number\n" +
                    "6. Finding the module of comlex number\n" +
                    "7. Finding the argument of a complex number\n" +
                    "8. Print of complex number");
                symbol = Console.ReadLine();

                //Выбор действия в зависимоти от введенного символа
                switch (symbol)
                {
                    //Ввод чисел
                    case "1":
                        try
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Console.Write("Enter real part:\t");
                                double realPart = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter imaginary part:\t");
                                double imaginary = Convert.ToDouble(Console.ReadLine());
                                if(i == 0) complexFirst.SetComplex(realPart, imaginary);
                                else complexSecond.SetComplex(realPart, imaginary);
                            }
                            
                        }
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;

                    //Сумма
                    case "2":
                        Complex sumComplex = SumComplex(complexFirst, complexSecond);
                        Console.Write("\nSum: ");
                        sumComplex.Print();
                        Console.Write("\n");
                        break;

                    //Разница
                    case "3":
                        Complex subComplex = SubstrucctionComplex(complexFirst, complexSecond);
                        Console.Write("\nSubstruction: ");
                        subComplex.Print();
                        Console.Write("\n");
                        break;

                    //Умножение
                    case "4":
                        Complex multiComplex = MultiplicationComplex(complexFirst, complexSecond);
                        Console.Write("\nMultiplication: ");
                        multiComplex.Print();
                        Console.Write("\n");
                        break;

                    //Отношение
                    case "5":
                        Complex divComplex = DivisionComplex(complexFirst, complexSecond);
                        Console.Write("\nDivision: ");
                        divComplex.Print();
                        Console.Write("\n");
                        break;

                    //Модуль
                    case "6":
                        Console.WriteLine("\ta. First number\n" +
                            "\tb. Second number");
                        number = Console.ReadLine();

                        //Выбор числа для которого искать модуль
                        switch(number)
                        {
                            case "a":
                                Console.WriteLine("\nModule: " + ModuleOfComplex(complexFirst) + "\n");
                                break;
                            case "b":
                                Console.WriteLine("\nModule: " + ModuleOfComplex(complexSecond) + "\n");
                                break;
                            default:
                                //Проверка на окончание программы
                                if (number != "q" || number != "Q") Console.WriteLine("\nUnknown command");
                                break;
                                
                        }
                        break;

                    //Аргумент
                    case "7":
                        Console.WriteLine("\ta. First number\n" +
                            "\tb. Second number");
                        number = Console.ReadLine();
                        //Выбор числа
                        switch (number)
                        {
                            case "a":
                                Console.WriteLine("\nArgument: " + ArgumentOfComplex(complexFirst) + "\n");
                                break;
                            case "b":
                                Console.WriteLine("\nArgument: " + ArgumentOfComplex(complexSecond) + "\n");
                                break;
                            default:
                                //Проверка на окончание программы
                                if (number != "q" || number != "Q") Console.WriteLine("\nUnknown command");
                                break;

                        }
                        break;

                    //Вывод
                    case "8":
                        Console.WriteLine("\ta. First number\n" +
                            "\tb. Second number");
                        number = Console.ReadLine();

                        //Выбор числа
                        switch (number)
                        {
                            case "a":
                                Console.Write("\nComplex: ");
                                complexFirst.Print();
                                Console.Write("\n");
                                break;
                            case "b":
                                Console.Write("\nComplex: ");
                                complexSecond.Print();
                                Console.Write("\n");
                                break;
                            default:
                                //Проверка на окончание программы
                                if (number != "q" || number != "Q") Console.WriteLine("\nUnknown command");
                                break;

                        }
                        break;
                    default:
                        //Проверка на окончание программы
                        if (symbol != "q" && symbol != "Q") Console.WriteLine("\nUnknown command");
                        break;

                }

                //Проверка на завершение программы
                if (symbol == "q" || symbol == "Q") break;

            }
            

        }
    }
}
