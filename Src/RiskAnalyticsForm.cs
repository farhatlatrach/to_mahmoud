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
    public partial class RiskAnalyticsForm : Form
    {
        public enum E_AggregationUpdateType
        {
            AggregatedDataUpdateType,
            AggregationCrteriaUpdateType,
            UnknownType
        }
        public static ConcurrentQueue<E_AggregationUpdateType> AggregationsUpdates = new ConcurrentQueue<E_AggregationUpdateType>();
        enum E_criterion
        {
            CriterionPortfolioType,
            CriterionCountryType,
            CriterionSectorType,
            CriterionUnknown
        }
        private RiskAnalyticsForm()
        {

        }
        private PortfoliosForm portfolios=null;
        public bool IsFormOpen { get; set; }
        private string[] criterions_list = { "Portfolio Name", "Security Country", "Security Sector" };
        private Dictionary<string, AggregationRow> level1_data_tables = new Dictionary<string, AggregationRow>();
        private Dictionary<string, AggregationRow> level2_data_tables = new Dictionary<string, AggregationRow>();
        private Dictionary<string, AggregationRow> level3_data_tables = new Dictionary<string, AggregationRow>();
       

        public RiskAnalyticsForm( ref PortfoliosForm portfolios_form)
        {
            InitializeComponent();

            
            CreateCombosCriterions();
            CreateTreeView();
            CreateAggregationDataGridView();
            portfolios = portfolios_form;
            
        }
        private void CreateTreeView()
        {
            tree_view.BeginUpdate();
            tree_view.Nodes.Add("Root");
            tree_view.EndUpdate();
        }
        private void CreateCombosCriterions()
        {
            //later on should be dynamic from config 
            combo_level2.Enabled = true;
            combo_level3.Enabled = true;
            combo_level1.Items.AddRange(criterions_list);
            combo_level2.Items.AddRange(criterions_list);
            combo_level3.Items.AddRange(criterions_list);
        }
        private void Config_columns_Click(object sender, EventArgs e)
        {
            List<DataGridViewColumnCollection> list = new List<DataGridViewColumnCollection>();
            list.Add(datagrid_view.Columns);
                ConfigColumnsForm config_form = new ConfigColumnsForm(list);
                // portfolios_form.MdiParent = this;
                config_form.Text = "Configuration";
                config_form.ShowDialog(this);
               
            
        }

        private void RiskAnalyticsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            IsFormOpen = false;
        }

        private void RiskAnalyticsForm_Shown(object sender, EventArgs e)
        {
            IsFormOpen = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void Combo_level1_Validated(object sender, EventArgs e)
        {
           
        }

        private void Combo_level1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
           
        }
        public class AggregationRow
        {
            public AggregationRow(string aggreg_col)
            {
                AggregationColumn = aggreg_col;
                TodayPnL = 0;
                Net = 0;
                Long = 0;
                Short = 0;
                TradingPnL = 0;
                DivPnL = 0;
                YTDPnL = 0;
                MTDPnL = 0;
                WTDPnL = 0;
            }
            public string AggregationColumn { get; set; }
            public double TodayPnL { get; set; }
            public double Net { get; set; }
            public double Long { get; set; }
            public double Short { get; set; }
            public double BODPnL { get; set; }
            public double TradingPnL { get; set; }
            public double DivPnL { get; set; }
            public double YTDPnL { get; set; }
            public double MTDPnL { get; set; }
            public double WTDPnL { get; set; }
           
        }
        private void CreateAggregationDataGridView()
        {
            DataTable data_table = new DataTable();
            

            data_table.Columns.Add("              ", typeof(string));

            data_table.Columns.Add("Tdy PnL", typeof(double));

            data_table.Columns.Add("Net", typeof(double));
            data_table.Columns.Add("Long", typeof(double));
            data_table.Columns.Add("Short", typeof(double));

            data_table.Columns.Add("BOD PnL", typeof(double));

            data_table.Columns.Add("Tding PnL", typeof(double));

            data_table.Columns.Add("Div PnL", typeof(double));
            data_table.Columns.Add("YTD PnL", typeof(double));
            data_table.Columns.Add("MTD PnL", typeof(double));
            data_table.Columns.Add("WTD PnL", typeof(double));

            datagrid_view.DataSource = data_table;

            //rounding
            datagrid_view.Columns["BOD PnL"].DefaultCellStyle.Format = "N0";
            datagrid_view.Columns["YTD PnL"].DefaultCellStyle.Format = "N0";
            datagrid_view.Columns["WTD PnL"].DefaultCellStyle.Format = "N0";
            datagrid_view.Columns["MTD PnL"].DefaultCellStyle.Format = "N0";

            datagrid_view.Columns["Tdy PnL"].DefaultCellStyle.Format = "N0";
            datagrid_view.Columns["Tding PnL"].DefaultCellStyle.Format = "N0";
            datagrid_view.Columns["Div PnL"].DefaultCellStyle.Format = "N0";

        }
        private void CreateAgrerationDatagridView(int level, string key)
        {
            DataTable data_table = (DataTable)datagrid_view.DataSource;
            data_table.Clear();
            Dictionary<string, AggregationRow> data = new Dictionary<string, AggregationRow>();

            if (level == 0)
                data = level1_data_tables;
            else if (level == 1)
                data = level2_data_tables;
            else if (level == 2)
                data = level3_data_tables;
            else
                return;
            
            AggregationRow row_total = new AggregationRow("TOTAL");
            foreach (var aggreg in data)
            {
               
                if (level > 0)
                {
                    string[] values_selected = key.Split('|');
                    string[] values_aggreg = aggreg.Key.Split('|');

                    if (level == 1)
                        if (values_selected[0] != values_aggreg[0])
                            continue;
                    if (level == 2)
                        if (values_selected[0] != values_aggreg[0]
                            || values_selected[1] != values_aggreg[1])
                            continue;
                   
                }
                AggregationRow row = aggreg.Value;
                data_table.Rows.Add(row.AggregationColumn, row.TodayPnL, row.Net, row.Long, row.Short, row.BODPnL,
                    row.TradingPnL, row.DivPnL, row.YTDPnL,
                    row.MTDPnL, row.WTDPnL);

                row_total.TodayPnL += row.TodayPnL;
                row_total.Net += row.Net;
                row_total.Long += row.Long;
                row_total.Short += row.Short;
                row_total.BODPnL += row.BODPnL;
                row_total.TradingPnL += row.TradingPnL;
                row_total.DivPnL += row.DivPnL;
                row_total.YTDPnL += row.YTDPnL;

                row_total.MTDPnL += row.MTDPnL;
                row_total.WTDPnL += row.WTDPnL;
            }
            data_table.Rows.Add(row_total.AggregationColumn, row_total.TodayPnL, row_total.Net, row_total.Long, row_total.Short, row_total.BODPnL,
                    row_total.TradingPnL, row_total.DivPnL, row_total.YTDPnL,
                    row_total.MTDPnL, row_total.WTDPnL);

            datagrid_view.DataSource = data_table;
            this.BindingContext[datagrid_view.DataSource].EndCurrentEdit();
        }
        public enum E_AggregationLevel
        {
            Level1=1,
            Level2,
            Level3,
            UnknownLevel,
        }
    private void SumUpAnAggregationRow(AggregationRow row, string key, E_AggregationLevel level, string tree_node)
        {
            tree_view.BeginUpdate();
           
            AggregationRow updated_row = new AggregationRow(key);
            bool is_new = false;
            if(level == E_AggregationLevel.Level1)
            {
                is_new = !level1_data_tables.TryGetValue(key, out updated_row);
               
            }else if (level == E_AggregationLevel.Level2)
            {
                is_new = !level2_data_tables.TryGetValue(key, out updated_row);
            }
            else if (level == E_AggregationLevel.Level3)
            {
                is_new = !level3_data_tables.TryGetValue(key, out updated_row);
            }
            if (is_new)
                updated_row = new AggregationRow(row.AggregationColumn);
            updated_row.TodayPnL += row.TodayPnL;
            updated_row.Net += row.Net;
            updated_row.Long += row.Long;
            updated_row.Short += row.Short;
            updated_row.BODPnL += row.BODPnL;
            updated_row.TradingPnL += row.TradingPnL;
            updated_row.DivPnL += row.DivPnL;
            updated_row.YTDPnL += row.YTDPnL;
            updated_row.MTDPnL += row.MTDPnL;
            updated_row.WTDPnL += row.WTDPnL;
            if (is_new)
            {
                if (level == E_AggregationLevel.Level1)
                {
                   level1_data_tables.Add(key,  updated_row);
                    TreeNode node = new TreeNode(key);
                    node.Tag = key;
                    tree_view.Nodes[0].Nodes.Add(node);
                   
                }
                else if (level == E_AggregationLevel.Level2)
                {   
                    level2_data_tables.Add(key, updated_row);

                    string[] values = key.Split('|');
                    TreeNode new_node = new TreeNode(values[1]);
                    new_node.Tag = key;
                    foreach( TreeNode  node in tree_view.Nodes[0].Nodes)
                    {
                        if (node.Text.ToString() == values[0])
                        {
                            tree_view.Nodes[0].Nodes[node.Index].Nodes.Add(new_node);
                            break;
                        }
                    }

                }
                else if (level == E_AggregationLevel.Level3)
                {
                    level3_data_tables.Add(key, updated_row);
                    string[] values = key.Split('|');
                    TreeNode new_node = new TreeNode(values[2]);
                    new_node.Tag = key;
                    int index_level2 = 0;
                    int index_level1 = 0;
                    foreach (TreeNode node in tree_view.Nodes[0].Nodes)
                    {
                       
                        if (node.Text.ToString() == values[0])
                        {
                            index_level1 = node.Index;

                            foreach (TreeNode child in node.Nodes)
                            {
                                if (child.Text.ToString() == values[1])
                                {
                                    
                                    index_level2 = child.Index;
                                    tree_view.Nodes[0].Nodes[index_level1].Nodes[index_level2].Nodes.Add(new_node);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                 
                }
            }
            tree_view.EndUpdate();

        }
        private void CreateAggregation(string aggreg_col1, string aggreg_col2="", string aggreg_col3="")
        {
            if (aggreg_col1 == "")
                return;
            level1_data_tables.Clear();
            level2_data_tables.Clear();
            level3_data_tables.Clear();
          
            foreach (TabPage tab in portfolios.PortfoliosTabs.Controls)
                {

                    foreach (Control control in tab.Controls)
                    {
                        if (control.Name == "dataGridView")
                        {
                            DataGridView view = (DataGridView)control;

                        
                        foreach (DataGridViewRow row in view.Rows)
                            {
                               
                            AggregationRow aggreg_row = new AggregationRow("");

                            aggreg_row.TodayPnL = (double)row.Cells["Tdy PnL"].Value;

                                aggreg_row.Net = (double)row.Cells["Position"].Value;
                                aggreg_row.Long = (double)row.Cells["Bought Quantity"].Value;
                                aggreg_row.Short = (double)row.Cells["Sold Quantity"].Value;
                                aggreg_row.BODPnL = (double)row.Cells["BOD PnL"].Value;

                                aggreg_row.TradingPnL = (double)row.Cells["Tding PnL"].Value;

                                aggreg_row.DivPnL = (double)row.Cells["Div PnL"].Value;
                                aggreg_row.YTDPnL = (double)row.Cells["YTD PnL"].Value;
                                aggreg_row.MTDPnL = (double)row.Cells["MTD PnL"].Value;
                                aggreg_row.WTDPnL = (double)row.Cells["WTD PnL"].Value;
                            if (aggreg_col1 != "" && row.Cells[aggreg_col1].Value != null)
                            {
                                string aggreg_col1_value = (string)row.Cells[aggreg_col1].Value.ToString();
                                aggreg_row.AggregationColumn = aggreg_col1_value;
                                SumUpAnAggregationRow(aggreg_row, aggreg_col1_value, E_AggregationLevel.Level1, aggreg_col1_value);
                                if (aggreg_col2 != "" && row.Cells[aggreg_col2].Value != null)
                                {
                                    string aggreg_col2_value = (string)row.Cells[aggreg_col2].Value.ToString();
                                    aggreg_row.AggregationColumn = aggreg_col2_value;
                                    SumUpAnAggregationRow(aggreg_row, aggreg_col1_value+ "|"+aggreg_col2_value, E_AggregationLevel.Level2, aggreg_col2_value);
                                    if (aggreg_col3 != "" && row.Cells[aggreg_col3].Value != null)
                                    {
                                        string aggreg_col3_value = (string)row.Cells[aggreg_col3].Value.ToString();
                                        aggreg_row.AggregationColumn = aggreg_col3_value;
                                        SumUpAnAggregationRow(aggreg_row, aggreg_col1_value + "|" + aggreg_col2_value + "|" + aggreg_col3_value, E_AggregationLevel.Level3, aggreg_col3_value);
                                    }
                                }
                            }
                           


                        }

                    }
                }
            }


        }
     
       
        private void Combo_level2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void Combo_level3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (false == backgroundWorker.CancellationPending)
            {

                if (false == AggregationsUpdates.IsEmpty)
                {
                    
                    E_AggregationUpdateType update;
                    if (AggregationsUpdates.TryDequeue(out update))
                    {
                        BackgroundWorker worker = (BackgroundWorker)sender;
                        worker.ReportProgress(0, update);
                    }
                }
            }
            e.Result = "cancelled";
        }
        private void UpdateAggregationData()
        {
            
        }
        private void UpdateAggregationCreteria()
        {
            if (combo_level1.SelectedItem == null)
                return;
           DataTable data_table = (DataTable)datagrid_view.DataSource;
            string aggreg_col1 = "";
            string aggreg_col2 = "";
            string aggreg_col3 = "";
            if (combo_level1.SelectedItem != null)
                aggreg_col1 = (string)combo_level1.SelectedItem;
            string aggre_colname = aggreg_col1;
            if (combo_level2.SelectedItem != null)
            {
                aggreg_col2 = (string)combo_level2.SelectedItem;
                aggre_colname += "/" + aggreg_col2;
            }
            if (combo_level3.SelectedItem != null)
            {
                aggreg_col3 = (string)combo_level3.SelectedItem;
                aggre_colname += "/" + aggreg_col3;
            }

            data_table.Columns[0].ColumnName = aggre_colname;
            tree_view.BeginUpdate();
            tree_view.Nodes[0].Nodes.Clear();
            tree_view.EndUpdate();
            data_table.Clear();
            datagrid_view.DataSource = data_table;

            CreateAggregation(aggreg_col1, aggreg_col2, aggreg_col3);
        }
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.UserState is E_AggregationUpdateType)
            {
                E_AggregationUpdateType update = (E_AggregationUpdateType)e.UserState;
                if (update == E_AggregationUpdateType.AggregatedDataUpdateType)
                {
                   CreateAgrerationDatagridView(selected_level,selected_aggreg_node_tag);
                } else if (update == E_AggregationUpdateType.AggregationCrteriaUpdateType)
                {
                    UpdateAggregationCreteria();
                }

            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private int selected_level = -1;
        private string selected_aggreg_node_tag = "";
        private void Tree_view_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CreateAgrerationDatagridView(e.Node.Level, (string)e.Node.Tag);
            selected_level = e.Node.Level;
            selected_aggreg_node_tag = (string)e.Node.Tag;


        }

        private void Button_aggregate_Click(object sender, EventArgs e)
        {
           
            AggregationsUpdates.Enqueue(RiskAnalyticsForm.E_AggregationUpdateType.AggregationCrteriaUpdateType);
        }
    }
}
