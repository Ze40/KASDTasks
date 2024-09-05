//Создаем необходиые перемменные: путь к файлу, строку в которую будем считывать, размерность пр-ва
using System.Numerics;

string filePath = "D://Projects//Алгоритмы//05-09-2024//05-09-2024//data.txt";
string? line;
int n;
int[][] matrix = null;
int[] vector = null;

//Метод для проверки на симетричность
bool IsSimetric(int[][] matrix)
{
    for (int i = 0; i < matrix.Length; i++)
    {
        for (int j = 0; j < matrix[i].Length; j++)
        {
            if (matrix[i][j] != matrix[j][i]) return false;
        }
    }
    return true;
}

//Метод для вычесления длины вектора через умножении вектора на матрицу
double CalculateLengthOfVector(int[][] matrix, int[] vector)
{
    int[] newMatrix;
    int sum = 0;
    newMatrix = new int[matrix.Length];

    //Умножаем вектор на матрицу
    for (int i = 0; (i < matrix.Length); i++)
    {
        sum = 0;
        for (int j = 0;j < matrix[i].Length;j++)
        {
            sum += vector[j] * matrix[j][i];
        }
        newMatrix[i] = sum;
    }

    //Умножаем получившееся на транспонированный вектор
    sum = 0;
    for(int i = 0; i < vector.Length; i++)
    {
        sum += newMatrix[i] * vector[i];
    }

    //Возвращаем корень из числа 
    return Math.Sqrt(sum);
}

try
{
    //Создаем поток для чтения файла
    StreamReader sr = new StreamReader(filePath);

    //Считываем размерность
    n = Convert.ToInt32(sr.ReadLine());
    
    //Создаем матрицу пространства
    matrix = new int[n][];
    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        matrix[i] = line.Split(' ').Select(x=> Convert.ToInt32(x)).ToArray();
    }

    //Считывание вектора
    line = sr.ReadLine();
    vector = line.Split(" ").Select(x => Convert.ToInt32(x)).ToArray();

    //Закрытие файла
    sr.Close();
}catch(Exception e)
{
    Console.WriteLine("Messege:" + e.Message);
}

//Проверка на симетричность
if (IsSimetric(matrix) != true)
{
    Console.WriteLine("Matrix not Simetric");
} else if (matrix != null && vector!=null)
{
    //Нахождение длины и вывод ответа
    double ans = CalculateLengthOfVector(matrix, vector);
    Console.WriteLine("Answer: " + ans);
}


