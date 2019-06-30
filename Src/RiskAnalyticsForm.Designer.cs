using System.Data;
using System.Windows.Forms;
namespace Dashboard
{
    partial class RiskAnalyticsForm
    {
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            
        }
        public  void ExplicitDispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            
        }
       
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.datagrid_view = new System.Windows.Forms.DataGridView();
            this.config_columns = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tree_view = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_level3 = new System.Windows.Forms.ComboBox();
            this.combo_level2 = new System.Windows.Forms.ComboBox();
            this.combo_level1 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.button_aggregate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_view)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // datagrid_view
            // 
            this.datagrid_view.AllowUserToAddRows = false;
            this.datagrid_view.AllowUserToDeleteRows = false;
            this.datagrid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_view.Location = new System.Drawing.Point(13, 43);
            this.datagrid_view.Name = "datagrid_view";
            this.datagrid_view.ReadOnly = true;
            this.datagrid_view.Size = new System.Drawing.Size(852, 479);
            this.datagrid_view.TabIndex = 5;
            this.datagrid_view.CellFormatting +=
         new System.Windows.Forms.DataGridViewCellFormattingEventHandler(
         this.dataGridView_CellFormatting);

            //dataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.datagrid_view.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);

            //dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.DarkGray;
            this.datagrid_view.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(58, 56, 56);
            // 
            // config_columns
            // 
            this.config_columns.Location = new System.Drawing.Point(398, 14);
            this.config_columns.Name = "config_columns";
            this.config_columns.Size = new System.Drawing.Size(93, 23);
            this.config_columns.TabIndex = 6;
            this.config_columns.Text = "config. columns";
            this.config_columns.UseVisualStyleBackColor = true;
            this.config_columns.Click += new System.EventHandler(this.Config_columns_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datagrid_view);
            this.groupBox1.Controls.Add(this.config_columns);
            this.groupBox1.Location = new System.Drawing.Point(341, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(870, 528);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tree_view);
            this.groupBox2.Location = new System.Drawing.Point(12, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 528);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tree View";
            // 
            // tree_view
            // 
            this.tree_view.Location = new System.Drawing.Point(6, 19);
            this.tree_view.Name = "tree_view";
            this.tree_view.Size = new System.Drawing.Size(311, 503);
            this.tree_view.TabIndex = 7;
            this.tree_view.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_view_AfterSelect);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_aggregate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.combo_level3);
            this.groupBox3.Controls.Add(this.combo_level2);
            this.groupBox3.Controls.Add(this.combo_level1);
            this.groupBox3.Location = new System.Drawing.Point(406, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(772, 58);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criterions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Level3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Level2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level1";
            // 
            // combo_level3
            // 
            this.combo_level3.FormattingEnabled = true;
            this.combo_level3.Location = new System.Drawing.Point(486, 19);
            this.combo_level3.Name = "combo_level3";
            this.combo_level3.Size = new System.Drawing.Size(121, 21);
            this.combo_level3.TabIndex = 2;
            this.combo_level3.SelectedIndexChanged += new System.EventHandler(this.Combo_level3_SelectedIndexChanged);
            // 
            // combo_level2
            // 
            this.combo_level2.FormattingEnabled = true;
            this.combo_level2.Location = new System.Drawing.Point(278, 19);
            this.combo_level2.Name = "combo_level2";
            this.combo_level2.Size = new System.Drawing.Size(121, 21);
            this.combo_level2.TabIndex = 1;
            this.combo_level2.SelectedIndexChanged += new System.EventHandler(this.Combo_level2_SelectedIndexChanged);
            // 
            // combo_level1
            // 
            this.combo_level1.FormattingEnabled = true;
            this.combo_level1.Location = new System.Drawing.Point(56, 20);
            this.combo_level1.Name = "combo_level1";
            this.combo_level1.Size = new System.Drawing.Size(121, 21);
            this.combo_level1.TabIndex = 0;
            this.combo_level1.SelectedIndexChanged += new System.EventHandler(this.Combo_level1_SelectedIndexChanged);
            this.combo_level1.Validated += new System.EventHandler(this.Combo_level1_Validated);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // button_aggregate
            // 
            this.button_aggregate.Location = new System.Drawing.Point(680, 23);
            this.button_aggregate.Name = "button_aggregate";
            this.button_aggregate.Size = new System.Drawing.Size(75, 23);
            this.button_aggregate.TabIndex = 6;
            this.button_aggregate.Text = "Aggregate";
            this.button_aggregate.UseVisualStyleBackColor = true;
            this.button_aggregate.Click += new System.EventHandler(this.Button_aggregate_Click);
            // 
            // RiskAnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 617);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RiskAnalyticsForm";
            this.Text = "Risk Analytics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RiskAnalyticsForm_FormClosed);
            this.Shown += new System.EventHandler(this.RiskAnalyticsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_view)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView the_view = (DataGridView)sender;
            System.Drawing.Color myGreen = new System.Drawing.Color();
            myGreen = System.Drawing.Color.FromArgb(50, 200, 50);

            System.Drawing.Color myRed = new System.Drawing.Color();
            myRed = System.Drawing.Color.FromArgb(255, 102, 204);


             if (the_view.Columns[e.ColumnIndex].Name.ToString() == "Last Price" 
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Delta"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "BOD PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "YTD PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "WTD PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "MTD PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Tdy PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Tding PnL"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Net"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Long"
                || the_view.Columns[e.ColumnIndex].Name.ToString() == "Short")
            {
                if (e.Value != null)
                {

                    if ((double)e.Value < 0)
                        //    e.CellStyle.ForeColor = System.Drawing.Color.MediumVioletRed;
                        e.CellStyle.ForeColor = myRed;
                    else
                        //e.CellStyle.ForeColor = System.Drawing.Color.Green;
                        e.CellStyle.ForeColor = myGreen;

                }
            }
           
            else if (e.Value != null)
            {
                //e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 153, 0);// Yellow;
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 200, 60);// Yellow;

            }
        }
        #endregion

        private System.Windows.Forms.DataGridView datagrid_view;
        private Button config_columns;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox combo_level3;
        private ComboBox combo_level2;
        private ComboBox combo_level1;
        private TreeView tree_view;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Button button_aggregate;
    }
}