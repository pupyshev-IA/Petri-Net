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
            tlpOutput = new TableLayoutPanel();
            lblOutput = new Label();
            panelOuterSettingsHolder = new Panel();
            panelSettingsHolder = new Panel();
            tlpSettings = new TableLayoutPanel();
            btnUpdateTokenSequence = new Button();
            lblInput = new Label();
            btnCreateGraph = new Button();
            mTextBoxInputData = new MaskedTextBox();
            tlpNavigation = new TableLayoutPanel();
            btnForward = new Button();
            lblStage = new Label();
            btnBack = new Button();
            tlpInfo = new TableLayoutPanel();
            lblTransitionsMaxCount = new Label();
            lblPlacesMaxCount = new Label();
            lblTokensMaxCount = new Label();
            lblNumberOfFirings = new Label();
            panelOuterView = new Panel();
            panelView = new Panel();
            btnClose = new Button();
            panelOuterMain = new Panel();
            panelMain.SuspendLayout();
            panelOuterSettingsHolder.SuspendLayout();
            panelSettingsHolder.SuspendLayout();
            tlpSettings.SuspendLayout();
            tlpNavigation.SuspendLayout();
            tlpInfo.SuspendLayout();
            panelOuterView.SuspendLayout();
            panelOuterMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.WhiteSmoke;
            panelMain.Controls.Add(tlpOutput);
            panelMain.Controls.Add(lblOutput);
            panelMain.Controls.Add(panelOuterSettingsHolder);
            panelMain.Controls.Add(panelOuterView);
            panelMain.Controls.Add(btnClose);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(2, 2);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(998, 718);
            panelMain.TabIndex = 0;
            panelMain.MouseDown += panelMain_MouseDown;
            // 
            // tlpOutput
            // 
            tlpOutput.ColumnCount = 4;
            tlpOutput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpOutput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpOutput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpOutput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpOutput.ForeColor = Color.Black;
            tlpOutput.Location = new Point(5, 646);
            tlpOutput.Name = "tlpOutput";
            tlpOutput.RowCount = 2;
            tlpOutput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpOutput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpOutput.Size = new Size(983, 62);
            tlpOutput.TabIndex = 6;
            // 
            // lblOutput
            // 
            lblOutput.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblOutput.Location = new Point(400, 608);
            lblOutput.Margin = new Padding(350, 0, 350, 0);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(191, 35);
            lblOutput.TabIndex = 5;
            lblOutput.Text = "Результат:";
            lblOutput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelOuterSettingsHolder
            // 
            panelOuterSettingsHolder.BackColor = Color.FromArgb(120, 120, 120);
            panelOuterSettingsHolder.Controls.Add(panelSettingsHolder);
            panelOuterSettingsHolder.Location = new Point(761, 105);
            panelOuterSettingsHolder.Name = "panelOuterSettingsHolder";
            panelOuterSettingsHolder.Padding = new Padding(1);
            panelOuterSettingsHolder.Size = new Size(227, 500);
            panelOuterSettingsHolder.TabIndex = 4;
            // 
            // panelSettingsHolder
            // 
            panelSettingsHolder.BackColor = Color.WhiteSmoke;
            panelSettingsHolder.Controls.Add(tlpSettings);
            panelSettingsHolder.Dock = DockStyle.Fill;
            panelSettingsHolder.Location = new Point(1, 1);
            panelSettingsHolder.Name = "panelSettingsHolder";
            panelSettingsHolder.Padding = new Padding(15, 10, 15, 10);
            panelSettingsHolder.Size = new Size(225, 498);
            panelSettingsHolder.TabIndex = 0;
            // 
            // tlpSettings
            // 
            tlpSettings.ColumnCount = 1;
            tlpSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSettings.Controls.Add(btnUpdateTokenSequence, 0, 3);
            tlpSettings.Controls.Add(lblInput, 0, 0);
            tlpSettings.Controls.Add(btnCreateGraph, 0, 2);
            tlpSettings.Controls.Add(mTextBoxInputData, 0, 1);
            tlpSettings.Controls.Add(tlpNavigation, 0, 5);
            tlpSettings.Controls.Add(tlpInfo, 0, 4);
            tlpSettings.Dock = DockStyle.Fill;
            tlpSettings.Location = new Point(15, 10);
            tlpSettings.Margin = new Padding(0);
            tlpSettings.Name = "tlpSettings";
            tlpSettings.RowCount = 6;
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tlpSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpSettings.Size = new Size(195, 478);
            tlpSettings.TabIndex = 0;
            // 
            // btnUpdateTokenSequence
            // 
            btnUpdateTokenSequence.BackColor = Color.FromArgb(27, 117, 208);
            btnUpdateTokenSequence.Dock = DockStyle.Fill;
            btnUpdateTokenSequence.Enabled = false;
            btnUpdateTokenSequence.FlatStyle = FlatStyle.Flat;
            btnUpdateTokenSequence.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnUpdateTokenSequence.ForeColor = Color.White;
            btnUpdateTokenSequence.Location = new Point(0, 129);
            btnUpdateTokenSequence.Margin = new Padding(0);
            btnUpdateTokenSequence.Name = "btnUpdateTokenSequence";
            btnUpdateTokenSequence.Size = new Size(195, 43);
            btnUpdateTokenSequence.TabIndex = 7;
            btnUpdateTokenSequence.Text = "Обновить метки";
            btnUpdateTokenSequence.UseVisualStyleBackColor = false;
            btnUpdateTokenSequence.Click += btnUpdateTokenSequence_Click;
            // 
            // lblInput
            // 
            lblInput.Dock = DockStyle.Fill;
            lblInput.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblInput.Location = new Point(0, 0);
            lblInput.Margin = new Padding(0);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(195, 43);
            lblInput.TabIndex = 4;
            lblInput.Text = "Входные данные:";
            lblInput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCreateGraph
            // 
            btnCreateGraph.BackColor = Color.FromArgb(27, 117, 208);
            btnCreateGraph.Dock = DockStyle.Fill;
            btnCreateGraph.FlatStyle = FlatStyle.Flat;
            btnCreateGraph.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCreateGraph.ForeColor = Color.White;
            btnCreateGraph.Location = new Point(0, 86);
            btnCreateGraph.Margin = new Padding(0);
            btnCreateGraph.Name = "btnCreateGraph";
            btnCreateGraph.Size = new Size(195, 43);
            btnCreateGraph.TabIndex = 7;
            btnCreateGraph.Text = "Отобразить граф";
            btnCreateGraph.UseVisualStyleBackColor = false;
            btnCreateGraph.Click += btnCreateGraph_Click;
            // 
            // mTextBoxInputData
            // 
            mTextBoxInputData.Dock = DockStyle.Fill;
            mTextBoxInputData.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            mTextBoxInputData.Location = new Point(0, 43);
            mTextBoxInputData.Margin = new Padding(0);
            mTextBoxInputData.Mask = "0;  0;  0;  0;  0;  0;  0";
            mTextBoxInputData.Name = "mTextBoxInputData";
            mTextBoxInputData.Size = new Size(195, 33);
            mTextBoxInputData.TabIndex = 5;
            mTextBoxInputData.TextAlign = HorizontalAlignment.Center;
            // 
            // tlpNavigation
            // 
            tlpNavigation.ColumnCount = 3;
            tlpNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpNavigation.Controls.Add(btnForward, 2, 0);
            tlpNavigation.Controls.Add(lblStage, 1, 0);
            tlpNavigation.Controls.Add(btnBack, 0, 0);
            tlpNavigation.Dock = DockStyle.Fill;
            tlpNavigation.Location = new Point(0, 434);
            tlpNavigation.Margin = new Padding(0);
            tlpNavigation.Name = "tlpNavigation";
            tlpNavigation.RowCount = 1;
            tlpNavigation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNavigation.Size = new Size(195, 44);
            tlpNavigation.TabIndex = 8;
            // 
            // btnForward
            // 
            btnForward.BackColor = Color.White;
            btnForward.Cursor = Cursors.Hand;
            btnForward.Dock = DockStyle.Fill;
            btnForward.Enabled = false;
            btnForward.Image = (Image)resources.GetObject("btnForward.Image");
            btnForward.Location = new Point(159, 3);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(33, 38);
            btnForward.TabIndex = 3;
            btnForward.UseVisualStyleBackColor = false;
            btnForward.Click += btnForward_Click;
            // 
            // lblStage
            // 
            lblStage.Dock = DockStyle.Fill;
            lblStage.Location = new Point(42, 3);
            lblStage.Margin = new Padding(3);
            lblStage.Name = "lblStage";
            lblStage.Size = new Size(111, 38);
            lblStage.TabIndex = 6;
            lblStage.Text = "label";
            lblStage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Dock = DockStyle.Fill;
            btnBack.Enabled = false;
            btnBack.Image = (Image)resources.GetObject("btnBack.Image");
            btnBack.Location = new Point(3, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(33, 38);
            btnBack.TabIndex = 2;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // tlpInfo
            // 
            tlpInfo.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpInfo.Controls.Add(lblTransitionsMaxCount, 0, 2);
            tlpInfo.Controls.Add(lblPlacesMaxCount, 0, 1);
            tlpInfo.Controls.Add(lblTokensMaxCount, 0, 3);
            tlpInfo.Controls.Add(lblNumberOfFirings, 0, 0);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.ForeColor = Color.Black;
            tlpInfo.Location = new Point(3, 175);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 4;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpInfo.Size = new Size(189, 256);
            tlpInfo.TabIndex = 6;
            // 
            // lblTransitionsMaxCount
            // 
            lblTransitionsMaxCount.Dock = DockStyle.Fill;
            lblTransitionsMaxCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTransitionsMaxCount.ForeColor = Color.Black;
            lblTransitionsMaxCount.Location = new Point(4, 127);
            lblTransitionsMaxCount.Name = "lblTransitionsMaxCount";
            lblTransitionsMaxCount.Size = new Size(181, 62);
            lblTransitionsMaxCount.TabIndex = 29;
            lblTransitionsMaxCount.Text = "Максимальное число переходов:";
            lblTransitionsMaxCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPlacesMaxCount
            // 
            lblPlacesMaxCount.Dock = DockStyle.Fill;
            lblPlacesMaxCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPlacesMaxCount.ForeColor = Color.Black;
            lblPlacesMaxCount.Location = new Point(4, 64);
            lblPlacesMaxCount.Name = "lblPlacesMaxCount";
            lblPlacesMaxCount.Size = new Size(181, 62);
            lblPlacesMaxCount.TabIndex = 26;
            lblPlacesMaxCount.Text = "Максимальное число мест:";
            lblPlacesMaxCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTokensMaxCount
            // 
            lblTokensMaxCount.Dock = DockStyle.Fill;
            lblTokensMaxCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTokensMaxCount.ForeColor = Color.Black;
            lblTokensMaxCount.Location = new Point(4, 190);
            lblTokensMaxCount.Name = "lblTokensMaxCount";
            lblTokensMaxCount.Size = new Size(181, 65);
            lblTokensMaxCount.TabIndex = 23;
            lblTokensMaxCount.Text = "Максимальное число меток:";
            lblTokensMaxCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNumberOfFirings
            // 
            lblNumberOfFirings.Dock = DockStyle.Fill;
            lblNumberOfFirings.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblNumberOfFirings.ForeColor = Color.Black;
            lblNumberOfFirings.Location = new Point(4, 1);
            lblNumberOfFirings.Name = "lblNumberOfFirings";
            lblNumberOfFirings.Size = new Size(181, 62);
            lblNumberOfFirings.TabIndex = 14;
            lblNumberOfFirings.Text = "Максимальное число срабатываний СП:";
            lblNumberOfFirings.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelOuterView
            // 
            panelOuterView.BackColor = Color.FromArgb(170, 170, 170);
            panelOuterView.Controls.Add(panelView);
            panelOuterView.Location = new Point(3, 3);
            panelOuterView.Name = "panelOuterView";
            panelOuterView.Padding = new Padding(1);
            panelOuterView.Size = new Size(752, 602);
            panelOuterView.TabIndex = 1;
            // 
            // panelView
            // 
            panelView.BackColor = Color.FromArgb(235, 235, 235);
            panelView.Dock = DockStyle.Fill;
            panelView.Location = new Point(1, 1);
            panelView.Name = "panelView";
            panelView.Size = new Size(750, 600);
            panelView.TabIndex = 1;
            panelView.Paint += panelView_Paint;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(27, 117, 208);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(953, 10);
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
            panelOuterMain.Size = new Size(1002, 722);
            panelOuterMain.TabIndex = 1;
            // 
            // GraphViewer
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1002, 722);
            Controls.Add(panelOuterMain);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            Name = "GraphViewer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            panelMain.ResumeLayout(false);
            panelOuterSettingsHolder.ResumeLayout(false);
            panelSettingsHolder.ResumeLayout(false);
            tlpSettings.ResumeLayout(false);
            tlpSettings.PerformLayout();
            tlpNavigation.ResumeLayout(false);
            tlpInfo.ResumeLayout(false);
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
        private Label lblInput;
        private MaskedTextBox mTextBoxInputData;
        private Panel panelOuterSettingsHolder;
        private Panel panelSettingsHolder;
        private Label lblStage;
        private Button btnCreateGraph;
        private TableLayoutPanel tlpSettings;
        private TableLayoutPanel tlpNavigation;
        private TableLayoutPanel tlpInfo;
        private Label lblNumberOfFirings;
        private Label lblTokensMaxCount;
        private Label lblTransitionsMaxCount;
        private Label lblPlacesMaxCount;
        private Label lblOutput;
        private TableLayoutPanel tlpOutput;
        private Button btnUpdateTokenSequence;
    }
}
