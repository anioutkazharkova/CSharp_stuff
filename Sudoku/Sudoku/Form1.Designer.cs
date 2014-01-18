namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.solvePage = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.levelBox = new System.Windows.Forms.GroupBox();
            this.hardRadioButton = new System.Windows.Forms.RadioButton();
            this.medRadioButton = new System.Windows.Forms.RadioButton();
            this.easyRadioButton = new System.Windows.Forms.RadioButton();
            this.timerBox = new System.Windows.Forms.GroupBox();
            this.timerTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.autoSolverPage = new System.Windows.Forms.TabPage();
            this.solveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.solvingProgressBar = new System.Windows.Forms.ProgressBar();
            this.gameField = new Sudoku.GameField();
            this.sudokuField = new Sudoku.GameField();
            this.labelProgress = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.solvePage.SuspendLayout();
            this.levelBox.SuspendLayout();
            this.timerBox.SuspendLayout();
            this.autoSolverPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sudokuField)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.solvePage);
            this.tabControl1.Controls.Add(this.autoSolverPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(526, 488);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // solvePage
            // 
            this.solvePage.Controls.Add(this.saveButton);
            this.solvePage.Controls.Add(this.clearButton);
            this.solvePage.Controls.Add(this.levelBox);
            this.solvePage.Controls.Add(this.timerBox);
            this.solvePage.Controls.Add(this.startButton);
            this.solvePage.Controls.Add(this.gameField);
            this.solvePage.Location = new System.Drawing.Point(4, 22);
            this.solvePage.Name = "solvePage";
            this.solvePage.Padding = new System.Windows.Forms.Padding(3);
            this.solvePage.Size = new System.Drawing.Size(518, 462);
            this.solvePage.TabIndex = 0;
            this.solvePage.Text = "Решать вручную";
            this.solvePage.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(9, 75);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save Game";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(8, 46);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear field";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // levelBox
            // 
            this.levelBox.Controls.Add(this.hardRadioButton);
            this.levelBox.Controls.Add(this.medRadioButton);
            this.levelBox.Controls.Add(this.easyRadioButton);
            this.levelBox.Location = new System.Drawing.Point(3, 110);
            this.levelBox.Name = "levelBox";
            this.levelBox.Size = new System.Drawing.Size(95, 91);
            this.levelBox.TabIndex = 3;
            this.levelBox.TabStop = false;
            this.levelBox.Text = "Уровни";
            // 
            // hardRadioButton
            // 
            this.hardRadioButton.AutoSize = true;
            this.hardRadioButton.Location = new System.Drawing.Point(6, 65);
            this.hardRadioButton.Name = "hardRadioButton";
            this.hardRadioButton.Size = new System.Drawing.Size(64, 17);
            this.hardRadioButton.TabIndex = 4;
            this.hardRadioButton.TabStop = true;
            this.hardRadioButton.Text = "Сложно";
            this.hardRadioButton.UseVisualStyleBackColor = true;
            this.hardRadioButton.CheckedChanged += new System.EventHandler(this.hardRadioButton_CheckedChanged);
            // 
            // medRadioButton
            // 
            this.medRadioButton.AutoSize = true;
            this.medRadioButton.Location = new System.Drawing.Point(6, 42);
            this.medRadioButton.Name = "medRadioButton";
            this.medRadioButton.Size = new System.Drawing.Size(68, 17);
            this.medRadioButton.TabIndex = 4;
            this.medRadioButton.TabStop = true;
            this.medRadioButton.Text = "Средний";
            this.medRadioButton.UseVisualStyleBackColor = true;
            this.medRadioButton.CheckedChanged += new System.EventHandler(this.medRadioButton_CheckedChanged);
            // 
            // easyRadioButton
            // 
            this.easyRadioButton.AutoSize = true;
            this.easyRadioButton.Location = new System.Drawing.Point(6, 19);
            this.easyRadioButton.Name = "easyRadioButton";
            this.easyRadioButton.Size = new System.Drawing.Size(77, 17);
            this.easyRadioButton.TabIndex = 4;
            this.easyRadioButton.TabStop = true;
            this.easyRadioButton.Text = "Несложно";
            this.easyRadioButton.UseVisualStyleBackColor = true;
            this.easyRadioButton.CheckedChanged += new System.EventHandler(this.easyRadioButton_CheckedChanged);
            // 
            // timerBox
            // 
            this.timerBox.Controls.Add(this.timerTextBox);
            this.timerBox.Location = new System.Drawing.Point(3, 253);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(95, 74);
            this.timerBox.TabIndex = 2;
            this.timerBox.TabStop = false;
            this.timerBox.Text = "Время";
            // 
            // timerTextBox
            // 
            this.timerTextBox.Location = new System.Drawing.Point(5, 29);
            this.timerTextBox.Name = "timerTextBox";
            this.timerTextBox.ReadOnly = true;
            this.timerTextBox.Size = new System.Drawing.Size(84, 20);
            this.timerTextBox.TabIndex = 3;
            this.timerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(8, 17);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "New Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // autoSolverPage
            // 
            this.autoSolverPage.Controls.Add(this.labelProgress);
            this.autoSolverPage.Controls.Add(this.solvingProgressBar);
            this.autoSolverPage.Controls.Add(this.solveButton);
            this.autoSolverPage.Controls.Add(this.loadButton);
            this.autoSolverPage.Controls.Add(this.sudokuField);
            this.autoSolverPage.Location = new System.Drawing.Point(4, 22);
            this.autoSolverPage.Name = "autoSolverPage";
            this.autoSolverPage.Padding = new System.Windows.Forms.Padding(3);
            this.autoSolverPage.Size = new System.Drawing.Size(518, 462);
            this.autoSolverPage.TabIndex = 1;
            this.autoSolverPage.Text = "Решать автоматически";
            this.autoSolverPage.UseVisualStyleBackColor = true;
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(9, 46);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(75, 23);
            this.solveButton.TabIndex = 6;
            this.solveButton.Text = "Solve game";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(9, 17);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 5;
            this.loadButton.Text = "Load Game";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // solvingProgressBar
            // 
            this.solvingProgressBar.Location = new System.Drawing.Point(104, 411);
            this.solvingProgressBar.Name = "solvingProgressBar";
            this.solvingProgressBar.Size = new System.Drawing.Size(365, 23);
            this.solvingProgressBar.TabIndex = 7;
            // 
            // gameField
            // 
            this.gameField.BackColor = System.Drawing.Color.GhostWhite;
            this.gameField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameField.Location = new System.Drawing.Point(104, 17);
            this.gameField.Name = "gameField";
            this.gameField.Size = new System.Drawing.Size(365, 365);
            this.gameField.TabIndex = 0;
            this.gameField.TabStop = false;
            
            this.gameField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gameField_MouseClick);
            // 
            // sudokuField
            // 
            this.sudokuField.BackColor = System.Drawing.Color.GhostWhite;
            this.sudokuField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sudokuField.Location = new System.Drawing.Point(104, 17);
            this.sudokuField.Name = "sudokuField";
            this.sudokuField.Size = new System.Drawing.Size(365, 365);
            this.sudokuField.TabIndex = 1;
            this.sudokuField.TabStop = false;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(101, 385);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(106, 13);
            this.labelProgress.TabIndex = 8;
            this.labelProgress.Text = "Прогресс решения:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 469);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Классическое Судоку";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.solvePage.ResumeLayout(false);
            this.levelBox.ResumeLayout(false);
            this.levelBox.PerformLayout();
            this.timerBox.ResumeLayout(false);
            this.timerBox.PerformLayout();
            this.autoSolverPage.ResumeLayout(false);
            this.autoSolverPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sudokuField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage solvePage;
        private System.Windows.Forms.TabPage autoSolverPage;
        private GameField gameField;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox timerBox;
        private System.Windows.Forms.TextBox timerTextBox;
        private System.Windows.Forms.GroupBox levelBox;
        private System.Windows.Forms.RadioButton easyRadioButton;
        private System.Windows.Forms.RadioButton medRadioButton;
        private System.Windows.Forms.RadioButton hardRadioButton;
        private System.Windows.Forms.Button clearButton;
        private GameField sudokuField;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ProgressBar solvingProgressBar;
        private System.Windows.Forms.Label labelProgress;

    }
}

