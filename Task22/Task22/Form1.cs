using GenerationLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using MyFormLib;

namespace Task22
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
                MyHashMap<int, int> hashMap = new MyHashMap<int,int>();
                MyTreeMap<int, int> treeMap = new MyTreeMap<int,int>();
            for (int i = minSize; i <= maxSize; i += step)
            {

                for (int j = treeMap.Size(); j < i; j++)
                {
                    hashMap.Push(j, j);
                    treeMap.Put(j, j);    
                }

                double timeSumHash = 0;
                double timeSumTree = 0;
                for (int j = 0; j < 1; j++)
                {
                    Random random = new Random();
                    int index = random.Next(0, i - 1);
                    int key = random.Next(i-100,i+100);
                    int value = random.Next(0,100);
                    Stopwatch sw = new Stopwatch();
                    switch (methodIndex)
                    {
                        case 0:
                            sw.Start();
                            hashMap.Push(key, value);
                            sw.Stop();
                            timeSumHash += sw.ElapsedMilliseconds;
                            sw.Start();
                            treeMap.Put(key, value);
                            sw.Stop();
                            timeSumTree += sw.ElapsedMilliseconds;
                            break;
                        case 1:
                            sw.Start();
                            int temp = hashMap.Get(index);
                            sw.Stop();
                            timeSumHash += sw.ElapsedMilliseconds;
                            sw.Start();
                            temp = treeMap.Get(index);
                            sw.Stop();
                            timeSumTree += sw.ElapsedMilliseconds;
                            break;
                        case 2:
                            sw.Start();
                            hashMap.Remove(index);
                            sw.Stop();
                            timeSumHash += sw.ElapsedMilliseconds;
                            sw.Start();
                            treeMap.Remove(index);
                            sw.Stop();
                            timeSumTree += sw.ElapsedMilliseconds;
                            break;
                        default: throw new ArgumentException("Не существует данного метода");
                    }
                }
                //MessageBox.Show(timeSumHash.ToString() + " " + timeSumTree.ToString());
                result.Add(new double[] { timeSumHash / 10, timeSumTree / 10 });
            }
            return result;
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int minSize = 10000;
            int maxSize = 100000;
            int step = 1000;
            List<double[]> time = TimeOfOperation(comboBox1.SelectedIndex, minSize, maxSize, step);
            PointPairList pointHash = new PointPairList();
            PointPairList pointTree = new PointPairList();
            for (int i = 0; i < time.Count; i++)
            {
                pointHash.Add(minSize + step * i, time[i][0]);
                pointTree.Add(minSize + step * i, time[i][1]);
            }


            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("HashMap", pointHash, Color.Red, SymbolType.Default);
            pane.AddCurve("TreeMap", pointTree, Color.Blue, SymbolType.Plus);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
    }
}
