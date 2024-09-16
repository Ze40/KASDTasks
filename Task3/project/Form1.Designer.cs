namespace project
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.GroupComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ArrayTypeComboBox = new System.Windows.Forms.ComboBox();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(956, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(336, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сгенерировать массивы";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GroupComboBox
            // 
            this.GroupComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupComboBox.FormattingEnabled = true;
            this.GroupComboBox.Items.AddRange(new object[] {
            "Первая группа",
            "Вторая группа",
            "Третья группа"});
            this.GroupComboBox.Location = new System.Drawing.Point(956, 80);
            this.GroupComboBox.Name = "GroupComboBox";
            this.GroupComboBox.Size = new System.Drawing.Size(336, 37);
            this.GroupComboBox.TabIndex = 5;
            this.GroupComboBox.Text = "Выберите группу";
            this.GroupComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Green;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(956, 554);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(336, 55);
            this.button2.TabIndex = 6;
            this.button2.Text = "Начать тесты";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ArrayTypeComboBox
            // 
            this.ArrayTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ArrayTypeComboBox.FormattingEnabled = true;
            this.ArrayTypeComboBox.Items.AddRange(new object[] {
            "Массив случайных чисел",
            "Разбитые на подмассивы",
            "Массивы с перестановками",
            "Массивы с повторениями"});
            this.ArrayTypeComboBox.Location = new System.Drawing.Point(956, 158);
            this.ArrayTypeComboBox.Name = "ArrayTypeComboBox";
            this.ArrayTypeComboBox.Size = new System.Drawing.Size(336, 37);
            this.ArrayTypeComboBox.TabIndex = 7;
            this.ArrayTypeComboBox.Text = "Тестовые данные";
            this.ArrayTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // zedGraph
            // 
            this.zedGraph.Location = new System.Drawing.Point(13, 14);
            this.zedGraph.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(889, 656);
            this.zedGraph.TabIndex = 9;
            this.zedGraph.UseExtendedPrintDialog = true;
            this.zedGraph.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 726);
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.ArrayTypeComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.GroupComboBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox GroupComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox ArrayTypeComboBox;
        private ZedGraph.ZedGraphControl zedGraph;
    }
}

