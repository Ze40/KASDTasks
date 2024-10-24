using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyFormLib;
using System.Diagnostics;
using GenerationLibrary;
using ZedGraph;

namespace Task17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива, шт";
            pane.YAxis.Title.Text = "Время выполнения, мс";
            pane.Title.Text = "Зависимость времени от количества элементов в массиве";
        }

        private List<double[]> TimeOfOperation(int methodIndex, int minSize, int maxSize, int step)
        {
            List<double[]> result = new List<double[]>();
            for (int i = minSize; i <= maxSize; i += step)
            {
                int[] array = Generate.Random(i);

                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);

                double timeSumList = 0;
                double timeSumArray = 0;
                for (int j = 0; j < 10; j++)
                {
                    Random random = new Random();
                    int index = random.Next(0, i - 1);
                    int element = -1;
                    Stopwatch sw = new Stopwatch();
                    switch (methodIndex)
                    {
                        case 0:
                            sw.Start();
                            linkedList.Add(element);
                            sw.Stop();
                            timeSumList += sw.ElapsedMilliseconds;
                            sw.Start();
                            arrayList.Add(element);
                            sw.Stop();
                            timeSumArray += sw.ElapsedMilliseconds;
                            break;
                        case 1:
                            sw.Start();
                            linkedList.Add(element);
                            sw.Stop();
                            timeSumList += sw.ElapsedMilliseconds;
                            sw.Start();
                            arrayList.Add(element);
                            sw.Stop();
                            timeSumArray += sw.ElapsedMilliseconds;
                            break;
                        case 2:
                            sw.Start();
                            int temp = linkedList.Get(index);
                            sw.Stop();
                            timeSumList += sw.ElapsedMilliseconds;
                            sw.Start();
                            temp = arrayList.Get(index);
                            sw.Stop();
                            timeSumArray += sw.ElapsedMilliseconds;
                            break;
                        case 3:
                            sw.Start();
                            linkedList.Set(index, element);
                            sw.Stop();
                            timeSumList += sw.ElapsedMilliseconds;
                            sw.Start();
                            arrayList.Set(index, element);
                            sw.Stop();
                            timeSumArray += sw.ElapsedMilliseconds;
                            break;
                        case 4:
                            sw.Start();
                            linkedList.Remove(index: index);
                            sw.Stop();
                            timeSumList += sw.ElapsedMilliseconds;
                            sw.Start();
                            arrayList.Remove(index: index);
                            sw.Stop();
                            timeSumArray += sw.ElapsedMilliseconds;
                            break;
                        default: throw new ArgumentException("Не существует данного метода");
                    }
                }
                result.Add(new double[] {timeSumArray / 10, timeSumList / 10});
            }
            return result;
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minSize = 10000;
            int maxSize = 1000000;
            int step = 10000;
            List<double[]> time = TimeOfOperation(comboBox1.SelectedIndex, minSize, maxSize, step);
            PointPairList pointArray = new PointPairList();
            PointPairList pointList = new PointPairList();
            for (int i = 0; i < time.Count; i++)
            {
                pointArray.Add(minSize + step * i, time[i][0]);
                pointList.Add(minSize + step * i, time[i][1]);
            }


            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("ArrayList", pointArray, Color.Red, SymbolType.Default);
            pane.AddCurve("LinkedList", pointList, Color.Blue, SymbolType.Plus);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
    }
}
