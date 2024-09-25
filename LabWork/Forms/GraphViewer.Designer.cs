namespace LabWork
{
    partial class GraphViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphViewer));
            panelMain = new Panel();
            panelOuterSettingsHolder = new Panel();
            panelSettingsHolder = new Panel();
            maskedTextBox1 = new MaskedTextBox();
            btnForward = new Button();
            btnBack = new Button();
            labelInput = new Label();
            panelOuterView = new Panel();
            panelView = new Panel();
            btnClose = new Button();
            panelOuterMain = new Panel();
            panelMain.SuspendLayout();
            panelOuterSettingsHolder.SuspendLayout();
            panelSettingsHolder.SuspendLayout();
            panelOuterView.SuspendLayout();
            panelOuterMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.WhiteSmoke;
            panelMain.Controls.Add(panelOuterSettingsHolder);
            panelMain.Controls.Add(panelOuterView);
            panelMain.Controls.Add(btnClose);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(2, 2);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(896, 596);
            panelMain.TabIndex = 0;
            panelMain.MouseDown += panelMain_MouseDown;
            // 
            // panelOuterSettingsHolder
            // 
            panelOuterSettingsHolder.BackColor = Color.FromArgb(120, 120, 120);
            panelOuterSettingsHolder.Controls.Add(panelSettingsHolder);
            panelOuterSettingsHolder.Location = new Point(659, 85);
            panelOuterSettingsHolder.Name = "panelOuterSettingsHolder";
            panelOuterSettingsHolder.Padding = new Padding(1);
            panelOuterSettingsHolder.Size = new Size(227, 417);
            panelOuterSettingsHolder.TabIndex = 4;
            // 
            // panelSettingsHolder
            // 
            panelSettingsHolder.BackColor = Color.WhiteSmoke;
            panelSettingsHolder.Controls.Add(maskedTextBox1);
            panelSettingsHolder.Controls.Add(btnForward);
            panelSettingsHolder.Controls.Add(btnBack);
            panelSettingsHolder.Controls.Add(labelInput);
            panelSettingsHolder.Dock = DockStyle.Fill;
            panelSettingsHolder.Location = new Point(1, 1);
            panelSettingsHolder.Name = "panelSettingsHolder";
            panelSettingsHolder.Padding = new Padding(15, 10, 15, 10);
            panelSettingsHolder.Size = new Size(225, 415);
            panelSettingsHolder.TabIndex = 0;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Dock = DockStyle.Top;
            maskedTextBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            maskedTextBox1.Location = new Point(15, 35);
            maskedTextBox1.Margin = new Padding(3, 3, 3, 15);
            maskedTextBox1.Mask = "{0, 0, 0, 0, 0, 0, 0}";
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(195, 33);
            maskedTextBox1.TabIndex = 5;
            maskedTextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // btnForward
            // 
            btnForward.BackColor = Color.White;
            btnForward.Cursor = Cursors.Hand;
            btnForward.Image = (Image)resources.GetObject("btnForward.Image");
            btnForward.Location = new Point(132, 377);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(35, 25);
            btnForward.TabIndex = 3;
            btnForward.UseVisualStyleBackColor = false;
            btnForward.Click += this.btnForward_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Image = (Image)resources.GetObject("btnBack.Image");
            btnBack.Location = new Point(65, 377);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(35, 25);
            btnBack.TabIndex = 2;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += this.btnBack_Click;
            // 
            // labelInput
            // 
            labelInput.Dock = DockStyle.Top;
            labelInput.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelInput.Location = new Point(15, 10);
            labelInput.Name = "labelInput";
            labelInput.Size = new Size(195, 25);
            labelInput.TabIndex = 4;
            labelInput.Text = "Входные данные:";
            labelInput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelOuterView
            // 
            panelOuterView.BackColor = Color.FromArgb(170, 170, 170);
            panelOuterView.Controls.Add(panelView);
            panelOuterView.Location = new Point(3, 3);
            panelOuterView.Name = "panelOuterView";
            panelOuterView.Padding = new Padding(1);
            panelOuterView.Size = new Size(650, 500);
            panelOuterView.TabIndex = 1;
            // 
            // panelView
            // 
            panelView.BackColor = Color.FromArgb(235, 235, 235);
            panelView.Dock = DockStyle.Fill;
            panelView.Location = new Point(1, 1);
            panelView.Name = "panelView";
            panelView.Size = new Size(648, 498);
            panelView.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(27, 117, 208);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(851, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 35);
            btnClose.TabIndex = 0;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // panelOuterMain
            // 
            panelOuterMain.BackColor = Color.FromArgb(120, 120, 120);
            panelOuterMain.Controls.Add(panelMain);
            panelOuterMain.Dock = DockStyle.Fill;
            panelOuterMain.Location = new Point(0, 0);
            panelOuterMain.Name = "panelOuterMain";
            panelOuterMain.Padding = new Padding(2);
            panelOuterMain.Size = new Size(900, 600);
            panelOuterMain.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            Controls.Add(panelOuterMain);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            panelMain.ResumeLayout(false);
            panelOuterSettingsHolder.ResumeLayout(false);
            panelSettingsHolder.ResumeLayout(false);
            panelSettingsHolder.PerformLayout();
            panelOuterView.ResumeLayout(false);
            panelOuterMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Button btnClose;
        private Panel panelView;
        private Button btnForward;
        private Button btnBack;
        private Panel panelOuterView;
        private Panel panelOuterMain;
        private Label labelInput;
        private MaskedTextBox maskedTextBox1;
        private Panel panelOuterSettingsHolder;
        private Panel panelSettingsHolder;
    }
}
