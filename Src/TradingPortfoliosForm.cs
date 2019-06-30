using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
namespace Dashboard
{
    public partial class PortfoliosForm : Form
    {
        public static ConcurrentQueue<Object> PortfoliosFormsUpdates = new ConcurrentQueue<Object>();

        public bool IsFormOpen { get; set; }

        public PortfoliosForm()
        {
            InitializeComponent();
            IsFormOpen = false;
           
        }
       
        
       
        //private void LoadFolioPositionsData(DataGridView dataGridView,string folio_name)
        //{

        //    DataTable data_table = NewPositionViewDataTable(dataGridView);
        //    dataGridView.DataSource = data_table;

           
        //}
       
        private void Config_columns_Click(object sender, EventArgs e)
        {
            List<DataGridViewColumnCollection> list = new List<DataGridViewColumnCollection>();
            foreach (TabPage tab in tabControl_portfolios.Controls)
            {
               
                foreach (Control control in tab.Controls)
                {
                    if (control.Name == "dataGridView")
                    {
                        DataGridView view = (DataGridView)control;

                        list.Add(view.Columns);
                       
                    }
                }
            }
            ConfigColumnsForm config_form = new ConfigColumnsForm(list);
            // portfolios_form.MdiParent = this;
            config_form.Text = "Configuration";
            config_form.ShowDialog(this);
        }

        private void PortfoliosForm_Shown(object sender, EventArgs e)
        {
            IsFormOpen = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void PortfoliosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsFormOpen = false;
            
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //runs on a different thread
            while (false == backgroundWorker.CancellationPending)
            {

                if (false == PortfoliosFormsUpdates.IsEmpty)
                {
                    Object obj = new Object();
                    if (PortfoliosFormsUpdates.TryDequeue(out obj))
                    {
                        BackgroundWorker worker = (BackgroundWorker)sender;
                        worker.ReportProgress(0, obj);
                    }
                }
            }
            e.Result = "cancelled";
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //runs on UI thread
            if (e.UserState is PriceUpdate)
            {
                PriceUpdate price = (PriceUpdate)e.UserState;
                UpdatePricesInDataGrids(price);
            }
            else if (e.UserState is Position)
            {
                Position pos = (Position)e.UserState;
                UpdatePositionInDataGrids(pos);
                

            }
            else
            if (e.UserState is Security)
            {
                Security sec = (Security)e.UserState;
                UpdateStaticDataInDataGrids(sec, "Currency");
                UpdateStaticDataInDataGrids(sec, "Previous Close");
                UpdateStaticDataInDataGrids(sec, "Previous Adj Close");
                UpdateStaticDataInDataGrids(sec, "Multiplier");
               
              
            }
            else
            {
                //issue with data type in queue
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void button_eod_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file_dialog = new SaveFileDialog();
           
            save_file_dialog.InitialDirectory = "c:\\";
            save_file_dialog.Filter = "txt files (*.txt)|*.txt";
            save_file_dialog.FilterIndex = 2;
            save_file_dialog.RestoreDirectory = true;
            save_file_dialog.FileName = "EOD_" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss");
            save_file_dialog.ShowDialog();
            string file_name = save_file_dialog.FileName;

            try
            {
                
                if (System.IO.File.Exists(file_name))
                {
                    throw new Exception("File already exists.");
                }

                using (System.IO.StreamWriter sw = System.IO.File.CreateText(file_name))
                {
                    sw.WriteLine("Date|Book|Security|Delta|TdyPnL|BODPnL|TdingPnL|DivPnL|LastPx|PrevCLS|Position|BODPos|Bought|BuyPx|Sold|SellPx|Multiplier|SecType|Curncy|YTDPnL|MTDPnL|WTDPnL");
                    foreach (TabPage tab in tabControl_portfolios.Controls)
                    {
                        string folio_name = tab.Name;
                        foreach (Control control in tab.Controls)
                        {
                            if (control.Name == "dataGridView")
                            {
                                DataGridView view = (DataGridView)control;
                                foreach (DataGridViewRow row in view.Rows)
                                {
          
                              
                                    sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}", 
                                        DateTime.Now.Date.ToString("dd/mm/yyyy"), row.Cells["Portfolio Name"].Value.ToString(),
                                        row.Cells["Security Name"].Value.ToString(), row.Cells["Delta"].Value.ToString(),
                                        row.Cells["Tdy PnL"].Value.ToString(), row.Cells["BOD PnL"].Value.ToString(),
                                        row.Cells["Tding PnL"].Value.ToString(), row.Cells["Div PnL"].Value.ToString(),
                                        row.Cells["Last Price"].Value.ToString(), row.Cells["Previous Close"].Value.ToString(),
                                        row.Cells["Position"].Value.ToString(), row.Cells["BOD Position"].Value.ToString(),
                                        row.Cells["Bought Quantity"].Value.ToString(), row.Cells["Average Bought Price"].Value.ToString(),
                                        row.Cells["Sold Quantity"].Value.ToString(), row.Cells["Average Sold Price"].Value.ToString(),
                                        row.Cells["Multiplier"].Value.ToString(), row.Cells["Security Type"].Value.ToString(),
                                        row.Cells["Security Currency"].Value.ToString(), row.Cells["YTD PnL"].Value.ToString(),
                                        row.Cells["MTD PnL"].Value.ToString(), row.Cells["WTD PnL"].Value.ToString());

                                }
                               
                            }
                        }
                    }
                    MessageBox.Show(String.Format("EOD file {0} saved.", save_file_dialog.FileName), "EOD File saved");
                   
                }
            }
            catch (Exception ex)
            {
                //
                MessageBox.Show(String.Format("EOD file {0} NOT saved. Error: {1}", save_file_dialog.FileName, ex.ToString()), "EOD File NOT saved");
            }
        }
    }
}