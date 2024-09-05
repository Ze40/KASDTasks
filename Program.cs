string filePath = "D://Projects//Алгоритмы//05-09-2024//05-09-2024//data.txt";
string? line;

int n;

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

double MatrixMultiplyVector(int[][] matrix, int[] vector)
{
    int[] newMatrix;
    int sum = 0;
    newMatrix = new int[matrix.Length];
    for (int i = 0; (i < matrix.Length); i++)
    {
        sum = 0;
        for (int j = 0;j < matrix[i].Length;j++)
        {
            sum += vector[j] * matrix[j][i];
        }
        newMatrix[i] = sum;
    }
    sum = 0;
    for(int i = 0; i < vector.Length; i++)
    {
        sum += newMatrix[i] * vector[i];
    }
    return Math.Sqrt(sum);
}

try
{
    StreamReader sr = new StreamReader(filePath);

    n = Convert.ToInt32(sr.ReadLine());
    int[][] matrix = new int[n][];

    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        if (line == null) {
            Console.WriteLine("ERR");
            break;
        }

        int[] a = line.Split(' ').Select(x=> Convert.ToInt32(x)).ToArray();
        matrix[i] = a;
    }

    if (IsSimetric(matrix) != true)
    {
        Console.WriteLine("not Sometric");
        sr.Close();
        return;
    }

    line = sr.ReadLine();
    if (line == null)
    {
        Console.WriteLine("Err");
        sr.Close();
        return;
    }
    int[] vector = line.Split(" ").Select(x => Convert.ToInt32(x)).ToArray();

    double ans = MatrixMultiplyVector(matrix, vector);
    Console.WriteLine("Answer: " + ans);

    sr.Close();
}catch(Exception e)
{
    Console.WriteLine("Messege:" + e.Message);
}
