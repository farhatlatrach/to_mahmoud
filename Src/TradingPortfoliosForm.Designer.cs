using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Dashboard
{
    partial class PortfoliosForm
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
            //call explicit dispose

        }
        public void ExplicitDispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }
        private DataTable NewPositionViewDataTable()
        {

            DataTable data_table = new DataTable();

            data_table.Columns.Add("Portfolio Name", typeof(string));
            data_table.Columns.Add("Security Name", typeof(string));

            data_table.Columns.Add("Tdy PnL", typeof(double));
            data_table.Columns.Add("BOD PnL", typeof(double));
            data_table.Columns.Add("Tding PnL", typeof(double));
            data_table.Columns.Add("Div PnL", typeof(double));

            data_table.Columns.Add("Last Price", typeof(double));
            data_table.Columns.Add("Previous Close", typeof(double));
            data_table.Columns.Add("Previous Adj Close", typeof(double));

            data_table.Columns.Add("BOD Position", typeof(double));
            data_table.Columns.Add("Bought Quantity", typeof(double));
            data_table.Columns.Add("Average Bought Price", typeof(double));
            data_table.Columns.Add("Sold Quantity", typeof(double));
            data_table.Columns.Add("Average Sold Price", typeof(double));
            data_table.Columns.Add("Position", typeof(double));
            data_table.Columns.Add("Forex Rate", typeof(double));
            data_table.Columns.Add("Delta", typeof(double));

            data_table.Columns.Add("Multiplier", typeof(double));
            data_table.Columns.Add("Security Type", typeof(string));
            data_table.Columns.Add("YTD PnL", typeof(double));
            data_table.Columns.Add("MTD PnL", typeof(double));
            data_table.Columns.Add("WTD PnL", typeof(double));

            data_table.Columns.Add("Security Quotation Factor", typeof(double));
            data_table.Columns.Add("Portfolio Currency", typeof(string));
            data_table.Columns.Add("Security Currency", typeof(string));
            data_table.Columns.Add("Forex", typeof(string));//pair = Portfolio ccy/security ccy

            data_table.Columns.Add("Security Sector", typeof(string));
            data_table.Columns.Add("Security Country", typeof(string));

            data_table.Columns["Delta"].Expression = "`Last Price` * `Position` * `Forex Rate` * `Security Quotation Factor` * `Multiplier`";

            data_table.Columns["Tdy PnL"].Expression = "`BOD PnL` + `Tding PnL`";
          
            data_table.Columns["BOD PnL"].Expression = "`BOD Position` * ( `Last Price` - `Previous Close`) * `Multiplier` * `Security Quotation Factor` * `Forex Rate`";

            data_table.Columns["Div PnL"].Expression = "`BOD Position` * ( `Previous Close` - `Previous Adj Close`) * `Multiplier` * `Security Quotation Factor` * `Forex Rate`";

            data_table.Columns["Tding PnL"].Expression = "`Forex Rate` * `Multiplier` * " +
                                                        " ( `Bought Quantity` * ( `Last Price` * `Security Quotation Factor` - `Average Bought Price`)  " +
                                                        " - `Sold Quantity` * ( `Last Price` * `Security Quotation Factor` - `Average Sold Price`)  )";



            return data_table;

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
               || the_view.Columns[e.ColumnIndex].Name.ToString() == "Position"
               || the_view.Columns[e.ColumnIndex].Name.ToString() == "BOD Position")
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


        private void AddPortfolioTab(string folio_name)
        {

            int tab_index = tabControl_portfolios.Controls.Count;



            TabPage tab = new TabPage();
            tab.Location = new System.Drawing.Point(18, 20);
            tab.Name = folio_name;
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.Size = new System.Drawing.Size(1439, 587);
            tab.TabIndex = tab_index;
            tab.Text = "   " + folio_name + "   (USD)";
            tab.UseVisualStyleBackColor = true;
            // data grid
            DataGridView dataGridView = new DataGridView();

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;

            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new System.Drawing.Point(20, 22);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.Size = new System.Drawing.Size(1250, 520);
            dataGridView.TabIndex = tab_index;
            dataGridView.CellFormatting +=
          new System.Windows.Forms.DataGridViewCellFormattingEventHandler(
          this.dataGridView_CellFormatting);

            //dataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.Black;
            dataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);

            //dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.DarkGray;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(58, 56, 56);

            DataTable data_table = NewPositionViewDataTable();
            dataGridView.DataSource = data_table;



            tab.Controls.Add(dataGridView);
            this.tabControl_portfolios.Controls.Add(tab);

            //rounding
            dataGridView.Columns["Position"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["BOD Position"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["Delta"].DefaultCellStyle.Format = "N0";

            dataGridView.Columns["BOD PnL"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["YTD PnL"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["WTD PnL"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["MTD PnL"].DefaultCellStyle.Format = "N0";

            dataGridView.Columns["Tdy PnL"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["Tding PnL"].DefaultCellStyle.Format = "N0";
            dataGridView.Columns["Div PnL"].DefaultCellStyle.Format = "N0";

        }
        public void UpdatePricesInDataGrids(PriceUpdate price)
        {


            foreach (TabPage tab in tabControl_portfolios.Controls)
            {
                string folio_name = tab.Name;
                foreach (Control control in tab.Controls)
                {
                    if (control.Name == "dataGridView")
                    {
                        DataGridView view = (DataGridView)control;
                        DataTable data_table = (DataTable)view.DataSource;
                        foreach (DataRow row in data_table.Rows)
                        {
                            
                    
                       
                            if ((string)row["Security Name"].ToString() == price.Name)
                            {
                                row["Last Price"] = price.Price;

                            }
                            else if ((string)row["Forex"].ToString() == price.Name)
                            {
                                row["Forex Rate"] = price.Price;

                            }
                        }
                        view.DataSource = data_table;
                        this.BindingContext[view.DataSource].EndCurrentEdit();

                    }
                }
            }


        }
        public void UpdateStaticDataInDataGrids(Security sec , string col_name)
        {
         
            foreach (TabPage tab in tabControl_portfolios.Controls)
            {

               
                    foreach (Control control in tab.Controls)
                    {
                        if (control.Name == "dataGridView")
                        {
                           
                            DataGridView view = (DataGridView)control;



                            DataTable data_table = (DataTable)view.DataSource;
                            foreach (DataRow row in data_table.Rows)
                            {
                                if ((string)row["Security Name"].ToString() == sec.Name)
                                {


                                    if (col_name == "Currency")
                                    {
                                        row.SetField("Portfolio Currency", "USD");

                                        row.SetField("Security Currency", sec.Currency);

                                        row.SetField("Security Country", sec.Country);

                                        row.SetField("Security Sector", sec.Sector);

                                        row.SetField("Forex", sec.Currency + "/USD");
                                        if (sec.Currency != "USD")
                                        {
                                            Security forex_sec = Security.CreateForexSecurity(sec.Currency, "USD");
                                            DataSource.WatchedTickers.Enqueue(forex_sec);
                                        }

                                        if (sec.Currency == "USD")
                                            row.SetField("Forex Rate", 1);

                                        row.SetField("Security Quotation Factor", sec.QuotationFactor);

                                        if (sec.Currency == "GBp")
                                            row.SetField("Security Quotation Factor", 0.01);
                                        else
                                        {
                                            if (sec.Currency == "ZAr")
                                                row.SetField("Security Quotation Factor", 0.01);
                                            else
                                                row.SetField("Security Quotation Factor", 1);
                                        }
                                    }
                                    else if (col_name == "Multiplier")
                                    {

                                        row.SetField("Multiplier",sec.Multiplier);
                                    }
                                    else if (col_name == "Previous Close")
                                    {

                                        row.SetField("Previous Close", sec.PreviousClose);
                                    }
                                    else if (col_name == "Previous Adj Close")
                                    {
                                        row.SetField("Previous Adj Close", sec.PreviousAdjClose);
                                    }
                                   
                                 
                                    


                                    
                                   
                                    view.DataSource = data_table;
                                  
                                }
                            }

                            this.BindingContext[view.DataSource].EndCurrentEdit();
                            
                        }
                    }
                   
                
            }

        }
        public void UpdatePositionInDataGrids(Position pos)
        {


            bool found_folio = false;

            foreach (TabPage tab in tabControl_portfolios.Controls)
            {
                if (tab.Name == pos.PortfolioName)
                    found_folio = true;
                if (found_folio)
                {
                    foreach (Control control in tab.Controls)
                    {
                        if (control.Name == "dataGridView")
                        {
                            DataGridView view = (DataGridView)control;
                            bool found = false;
                            DataTable data_table = (DataTable)view.DataSource;
                            foreach (DataRow row in data_table.Rows)
                            {
                                if (row["Security Name"].ToString() == pos.Underlying)
                                {
                                    row["Average Sold Price"] = pos.SoldAveragePrice;
                                    row["Average Bought Price"] = pos.BoughtAveragePrice;
                                    row["Sold Quantity"] = pos.SoldQuantity;
                                    row["Bought Quantity"] = pos.BoughtQuantity;
                                    row["Position"] = pos.Quantity;
                                    row["BOD Position"] = pos.BeginOfDayQuantity;

                                    found = true;
                                    break;
                                }
                            }
                            if (false == found
                                && (string)tab.Name == pos.PortfolioName)
                            {
                              
                                // adding a new row here                                
                                data_table.Rows.Add(pos.PortfolioName, pos.Underlying,
                                0, 0, 0, 0, /* pnls */
                                0, 0, 0, /* last - prev and Adj Prev */
                                pos.BeginOfDayQuantity, pos.BoughtQuantity, pos.BoughtAveragePrice, pos.SoldQuantity, pos.SoldAveragePrice, pos.Quantity,
                                1, 0, 1, pos.UnderlyingType, // FXRate - Delta - Multiplier - SecType
                                pos.YTDPnL, pos.MTDPnL, pos.WTDPnL, // YTD - MTD - WTD
                                1, "USD", "USD", "USD",
                                "SECTOR", "COUNTRY");

                              
                               
                                // lets push ask for static data update here
                                Security s = Security.GetNewSecurityFromOMSPosition(pos.Underlying, pos.Underlying, pos.UnderlyingType);
                                DataSource.WatchedTickers.Enqueue(s);
                            }

                            view.DataSource = data_table;
                            this.BindingContext[view.DataSource].EndCurrentEdit();
                        }
                    }
                }
            }
            if (false == found_folio)
            {
                AddPortfolioTab(pos.PortfolioName);
                UpdatePositionInDataGrids(pos);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.config_columns = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.button_eod = new System.Windows.Forms.Button();
            this.tabControl_portfolios = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // config_columns
            // 
            this.config_columns.Location = new System.Drawing.Point(343, 8);
            this.config_columns.Name = "config_columns";
            this.config_columns.Size = new System.Drawing.Size(108, 21);
            this.config_columns.TabIndex = 4;
            this.config_columns.Text = "config. columns";
            this.config_columns.UseVisualStyleBackColor = true;
            this.config_columns.Click += new System.EventHandler(this.Config_columns_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // button_eod
            // 
            this.button_eod.Location = new System.Drawing.Point(457, 6);
            this.button_eod.Name = "button_eod";
            this.button_eod.Size = new System.Drawing.Size(120, 23);
            this.button_eod.TabIndex = 5;
            this.button_eod.Text = "EOD";
            this.button_eod.UseVisualStyleBackColor = true;
            this.button_eod.Click += new System.EventHandler(this.button_eod_Click);
            // 
            // tabControl_portfolios
            // 
            this.tabControl_portfolios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_portfolios.Location = new System.Drawing.Point(3, 16);
            this.tabControl_portfolios.Name = "tabControl_portfolios";
            this.tabControl_portfolios.SelectedIndex = 0;
            this.tabControl_portfolios.Size = new System.Drawing.Size(1533, 648);
            this.tabControl_portfolios.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl_portfolios);
            this.groupBox1.Location = new System.Drawing.Point(8, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1539, 667);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Portfolios";
            // 
            // PortfoliosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 522);
            this.Controls.Add(this.button_eod);
            this.Controls.Add(this.config_columns);
            this.Controls.Add(this.groupBox1);
            this.Name = "PortfoliosForm";
            this.Text = "Dashboard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PortfoliosForm_FormClosed);
            this.Shown += new System.EventHandler(this.PortfoliosForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public TabControl PortfoliosTabs
        {
            get
            {
                return tabControl_portfolios;
            }
        }

        private Button config_columns;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Button button_eod;
        private TabControl tabControl_portfolios;
        private GroupBox groupBox1;
    }
}

