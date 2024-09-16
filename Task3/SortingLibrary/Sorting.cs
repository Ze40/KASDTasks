using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLibrary
{
    public class TreeNode
    {
        public int value { get; set; }
        public TreeNode(int key)
        {
            value = key;
        }

        public TreeNode Right { get; set; }
        public TreeNode Left { get; set; }

        public void Insert(TreeNode root)
        {
            if (root.value < value)
            {
                if (Left == null) Left = root;
                else Left.Insert(root);
            }
            else
            {
                if (Right == null) Right = root;
                else Right.Insert(root);
            }
        }

        public int[] TransformToArray(List<int> elements = null)
        {
            if (elements == null) elements = new List<int>();
            if (Left != null) Left.TransformToArray(elements);
            elements.Add(value);
            if (Right != null) Right.TransformToArray(elements);
            return elements.ToArray();
        }


    }


    public static class Sorting
    {
        public static void UnknownSort(this int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int num1 = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    int num2 = array[j];
                    if (num1 > num2)
                    {
                        int temp = num2;
                        array[j] = num1;
                        array[i] = temp;
                        num1 = temp;
                    }
                }

            }
        }
        public static int[] BubbleSort(int[] array, bool isReverse = false)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (isReverse)
                    {
                        if (array[i] > array[j])
                        {
                            int temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                        continue;
                    }
                    if (array[i] < array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }
        public static int[] ShakerSort(int[] array, bool isReverse = false)
        {
            bool swaped = true;
            int startInddex = 0;
            int endInddex = array.Length;

            while (swaped)
            {
                swaped = false;

                for (int i = startInddex; i < endInddex - 1; i++)
                {
                    if ((!isReverse && array[i] > array[i + 1]) || (isReverse && array[i] < array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swaped = true;
                    }
                }

                if (!swaped) break;

                --endInddex;

                for (int i = endInddex - 1; i >= startInddex; i--)
                {
                    if ((!isReverse && array[i] > array[i + 1]) || (isReverse && array[i] < array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swaped = true;
                    }
                }

                ++startInddex;
            }
            return array;
        }

        private static int GetNextStep(int step)
        {
            step = step * 1000 / 1247;
            return step > 1 ? step : 1;
        }
        public static int[] CombSort(int[] array, bool isReverse = false)
        {
            int length = array.Length;
            int step = length - 1;
            while (step > 1)
            {
                for (int i = 0; i + step < length; i++)
                {
                    if (array[i] < array[i + step])
                    {
                        int temp = array[i];
                        array[i] = array[i + step];
                        array[i + step] = temp;
                    }
                }
                step = GetNextStep(step);
            }
            BubbleSort(array, isReverse);
            return array;
        }

        public static int[] InsertionSort(int[] array, bool isReverse = false)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int swapIndex = i;
                while (swapIndex > 0 && ((!isReverse && array[swapIndex] < array[swapIndex - 1]) || (isReverse && array[swapIndex] > array[swapIndex - 1])))
                {
                    int temp = array[swapIndex];
                    array[swapIndex] = array[swapIndex - 1];
                    array[swapIndex - 1] = temp;
                    swapIndex--;
                }
            }
            return array;
        }
        public static List<double> InsertionSort(List<double> array, bool isReverse = false)
        {
            for (int i = 1; i < array.Count; i++)
            {
                int swapIndex = i;
                while (swapIndex > 0 && ((!isReverse && array[swapIndex] < array[swapIndex - 1]) || (isReverse && array[swapIndex] > array[swapIndex - 1])))
                {
                    double temp = array[swapIndex];
                    array[swapIndex] = array[swapIndex - 1];
                    array[swapIndex - 1] = temp;
                    swapIndex--;
                }
            }
            return array;
        }
        public static int[] ShellSort(int[] array, bool isReverse = false)
        {
            int gap = array.Length / 2;
            for (; gap > 0; gap /= 2)
            {
                for (int i = gap; i < array.Length; i++)
                {
                    int temp = array[i];
                    int j;
                    for (j = i; j >= gap && ((!isReverse && array[j - gap] > temp) || (isReverse && array[j - gap] < temp)); j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
            return array;
        }
        public static int[] TreeSort(int[] array, bool isReverse = false)
        {
            TreeNode root = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++) root.Insert(new TreeNode(array[i]));
            int[] newArray = root.TransformToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (isReverse)
                {
                    array[i] = newArray[array.Length - 1 - i];
                    continue;
                }
                array[i] = newArray[i];
            }
            return array;
        }
        public static int[] GnomeSort(int[] array, bool isReverse = false)
        {
            int index = 0;
            while (index < array.Length)
            {
                if (index == 0) index++;
                if ((!isReverse && array[index] >= array[index - 1]) || (isReverse && array[index] <= array[index - 1])) index++;
                else
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }

            }
            return array;
        }
        public static int[] SelectionSort(int[] array, bool isReverse = false)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int extremum = array[i];
                int indexOfExtremum = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if ((isReverse && array[j] > extremum) || (!isReverse && array[j] < extremum))
                    {
                        indexOfExtremum = j;
                        extremum = array[j];
                    }
                }

                array[indexOfExtremum] = array[i];
                array[i] = extremum;
            }
            return array;

        }

        public static void Heapify(int[] array, int i, int n)
        {
            int largestIndex = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left] > array[largestIndex]) largestIndex = left;
            if (right < n && array[right] > array[largestIndex]) largestIndex = right;

            if (largestIndex != i)
            {
                int temp = array[largestIndex];
                array[largestIndex] = array[i];
                array[i] = temp;

                Heapify(array, largestIndex, n);
            }
        }
        public static int[] HeapSort(int[] array, bool isReverse = false)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--) Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (isReverse)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    int temp = (int)array[i];
                    array[i] = array[n - i - 1];
                    array[n - i - 1] = temp;
                }
            }
            return array;
        }

        private static int QuickSortPart(int[] array, int start, int end, bool isReverse = false)
        {
            int pivot = array[start];
            int pivotIndex = 0;
            int left = start;
            if (start + 1 >= end) return left;

            for (int i = start + 1; i < end; i++)
            {
                if ((!isReverse && array[i] < pivot) || (isReverse && array[i] > pivot))
                {
                    int temp = array[i];
                    array[i] = array[left];
                    array[left] = temp;
                    left++;
                }
                if (array[i] == pivot)
                {
                    pivotIndex = i;
                }
                else if (array[left] == pivot)
                {
                    pivotIndex = left;
                }
            }

            array[pivotIndex] = array[left];
            array[left] = pivot;
            return left;
        }
        public static void QuickSortFor(this int[] array, int start, int end, bool isReverse = false)
        {
            if (start < end)
            {
                int point = QuickSortPart(array, start, end, isReverse);
                QuickSortFor(array, start, point, isReverse);
                QuickSortFor(array, point + 1, end, isReverse);
            }
        }
        public static int[] QuickSort(int[] array, bool isReverse = false)
        {
            QuickSortFor(array, 0, array.Length, isReverse);
            return array;
        }

        private static void Merge(int[] array, int left, int middle, int right, bool isReverse)
        {
            int length1 = middle - left + 1;
            int length2 = right - middle;

            int[] arrayLeft = new int[length1];
            int[] arrayRight = new int[length2];

            for (int i = 0; i < length1; i++) arrayLeft[i] = array[left + i];
            for (int i = 0; i < length2; i++) arrayRight[i] = array[middle + i + 1];

            int indexLeft = 0, indexRight = 0, index = left;

            while (indexLeft < length1 && indexRight < length2)
            {
                if ((!isReverse && arrayLeft[indexLeft] < arrayRight[indexRight]) || (isReverse && arrayLeft[indexLeft] > arrayRight[indexRight]))
                {
                    array[index] = arrayLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    array[index] = arrayRight[indexRight];
                    indexRight++;
                }
                index++;
            }

            while (indexLeft < length1)
            {
                array[index] = arrayLeft[indexLeft];
                indexLeft++;
                index++;
            }
            while (indexRight < length2)
            {
                array[index] = arrayRight[indexRight];
                indexRight++;
                index++;
            }

        }
        public static void MergeSort(this int[] array, int left, int right, bool isReverse = false)
        {
            if (left >= right) return;

            int middle = left + (right - left) / 2;
            array.MergeSort(left, middle, isReverse);
            array.MergeSort(middle + 1, right, isReverse);
            Merge(array, left, middle, right, isReverse);
        }
        public static int[] MergeSort(int[] array, bool isReverse = false)
        {
            int left = 0;
            int right = array.Length - 1;
            array.MergeSort(left, right, isReverse);
            return array;
        }

        public static int[] CountSort(int[] array, bool isReverse = false)
        {

            int max = array[0];
            foreach (int item in array) max = Math.Max(max, item);

            int[] countArray = new int[max + 1];
            foreach (int item in array) countArray[item]++;

            for (int i = 1; i <= max; i++) countArray[i] += countArray[i - 1];

            int[] answerArray = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                answerArray[countArray[array[i]] - 1] = array[i];
                countArray[array[i]]--;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (isReverse)
                {
                    array[i] = answerArray[array.Length - i - 1];
                    continue;
                }
                array[i] = answerArray[i];
            }
            return countArray;
        }
        public static double[] BucketSort(double[] array, bool isReverse = false)
        {
            int length = array.Length;
            List<double>[] bucket = new List<double>[length];
            for (int i = 0; i < length; i++) bucket[i] = new List<double>();

            int bucketIndex;
            foreach (double item in array)
            {
                bucketIndex = (int)(length * item);
                bucket[bucketIndex].Add(item);
            }

            for (int i = 0; i < length; i++) bucket[i] = InsertionSort(bucket[i], isReverse);

            bucketIndex = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < bucket[i].Count; j++)
                {
                    array[bucketIndex++] = bucket[i][j];
                }
            }
            return array;
        }

        static void CountSortByDig(int[] array, int exp, bool isReverse = false)
        {
            int[] answerArray = new int[array.Length];
            int[] countArray = new int[10];
            int index;

            for (index = 0; index < 10; index++) countArray[index] = 0;

            for (index = 0; index < array.Length; index++) countArray[(array[index] / exp) % 10]++;
            for (index = 1; index < 10; index++) countArray[index] += countArray[index - 1];

            for (index = array.Length - 1; index >= 0; index--)
            {
                answerArray[countArray[(array[index] / exp) % 10] - 1] = array[index];
                countArray[(array[index] / exp) % 10]--;
            }

            for (index = 0; index < array.Length; index++)
            {
                if (isReverse)
                {
                    array[index] = answerArray[array.Length - index - 1];
                    continue;
                }
                array[index] = answerArray[index];
            }
        }
        public static int[] RadixSort(int[] array, bool isReverse = false)
        {

            int max = array[0];
            for (int i = 1; i < array.Length; i++) if (array[i] > max) max = array[i];

            for (int exp = 1; max / exp > 0; exp *= 10) CountSortByDig(array, exp, isReverse);
            return array;
        }


        static void BitonicMerge(int[] array, int start, int count, bool dir)
        {

            if (count > 1)
            {
                int k = count / 2;
                for (int i = start; i < start + k; i++)
                {
                    bool flag = false;
                    if (array[i] > array[i + k]) flag = true;
                    if (dir == flag)
                    {
                        int temp = array[i];
                        array[i] = array[i + k];
                        array[i + k] = temp;
                    }
                }
                BitonicMerge(array, start, k, dir);
                BitonicMerge(array, start + k, k, dir);

            }
        }
        static void BitonicSortPart(int[] array, int start, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortPart(array, start, k, true);
                BitonicSortPart(array, start + k, k, false);
                BitonicMerge(array, start, count, dir);
            }
        }
        public static int[] BitonicSort(int[] array, bool isReverse = false)
        {

            double length = array.Length;
            int powOfTwo = 0;
            while (length > 1)
            {
                length /= 2;
                powOfTwo++;
            }
            if (length != 1) powOfTwo++;

            int[] bitonicArray = new int[(int)Math.Pow(2, powOfTwo)];
            for (int i = 0; i < bitonicArray.Length; i++)
            {
                if (i < array.Length)
                {
                    bitonicArray[i] = array[i];
                    continue;
                }
                bitonicArray[i] = -1;
            }

            BitonicSortPart(bitonicArray, 0, bitonicArray.Length, !isReverse);

            for (int i = 0; i < bitonicArray.Length; i++)
            {
                if (isReverse && bitonicArray[i] != -1) array[i] = bitonicArray[i];
                if (!isReverse && bitonicArray[bitonicArray.Length - 1 - i] != -1) array[array.Length - 1 - i] = bitonicArray[bitonicArray.Length - 1 - i];
            }

            return array;
        }
    }
}
