using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortingLibrary;
using GenerationLibrary;
using System.IO;
using System.Reflection;

namespace Task13
{
    public partial class Form1 : Form
    {
        public void SetPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\array.txt";
            pathToTime = appDir + @"\time.txt";
        }
        string pathToArray;
        string pathToTime;

        int selectedGroupIndex = -1;
        int selectedArrayTypeIndex = -1;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGroupIndex = GroupComboBox.SelectedIndex;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedArrayTypeIndex = ArrayTypeComboBox.SelectedIndex;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
