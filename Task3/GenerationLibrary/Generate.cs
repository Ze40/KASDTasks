using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationLibrary
{
    public static class Generate
    {
        //Случайные числа по модулю 1000
        public static int[] Random(int length)
        {
            int[] array = new int[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(0, 1000);
            }
            return array;
        }
        public static double[] RandomDouble(int length)
        {
            double[] array = new double[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = (double)random.Next(1, 100) / 100;
            }
            return array;
        }

        //Разбитые на несколько отсортированных подмасивов разного размера
        public static int[] RandomSub(int length)
        {
            Random random = new Random();
            int modul = random.Next(0, length);
            int newLength = random.Next(2, length) % modul;
            if (newLength < 2) newLength = 2;
            int[] array = new int[length];
            int countOfArray = 0;

            int i = 0;
            while (i < length)
            {
                int exp = random.Next(0, 1000);
                int elementBase = 0;
                countOfArray++;

                while (i < length && i < newLength * countOfArray)
                {
                    elementBase++;
                    array[i] = elementBase * exp;
                    i++;
                }
            }

            return array;
        }

        //Изначально отсортированные с некторым количеством перестановок
        public static int[] RandomBySwap(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++) array[i] = i;

            Random random = new Random();
            int countOfSwap = random.Next(0, length/3);
            for (int i = 0; i < countOfSwap; i++)
            {
                int firstIndex = random.Next(0, array.Length - 1);
                int secondIndex = random.Next(0, array.Length - 1);
                int temp = array[firstIndex];
                array[firstIndex] = array[secondIndex];
                array[secondIndex] = temp;
            }
            return array;
        }

        public static int[] RandomBySwapAndRepeat(int length)
        {
            int[] array = RandomBySwap(length);
            Random random = new Random();
            int indexOfRepeat = random.Next(0, length - 1);
            int countOfRepeat = random.Next(0, length / 3);

            while (countOfRepeat > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[indexOfRepeat])
                {
                    array[randomIndex] = array[indexOfRepeat];
                    countOfRepeat--;
                }

            }
            return array;
        }
    }
}
