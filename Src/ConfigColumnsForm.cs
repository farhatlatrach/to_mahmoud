using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace Dashboard
{
    public partial class ConfigColumnsForm : Form
    {
        List<DataGridViewColumnCollection> columns_;
        public ConfigColumnsForm(List<DataGridViewColumnCollection> columns)
        {
            columns_ = columns;
            InitializeComponent();

            RefreshGrid();
        }
        private void RefreshGrid()
            {
            DataTable data_source1 = new DataTable();// dataGridView1.DataSource;
            DataTable data_source2 = new DataTable();// (DataTable)dataGridView2.DataSource;
            data_source1.Columns.Add("Hidden Columns");
            data_source2.Columns.Add("Visible Columns");
            foreach (DataGridViewColumnCollection cols in columns_)
            {
                //silly here 
                foreach (DataGridViewColumn col in cols)
                {


                    if (col.Visible == true)
                    {
                        data_source2.Rows.Add(col.Name);
                    }

                    else
                    {
                        data_source1.Rows.Add(col.Name);
                    }
                }
            }
            dataGridView1.DataSource = data_source1;
            dataGridView2.DataSource = data_source2;
            //dataGridView2.ClearSelection();
            //dataGridView1.ClearSelection();
        }

        private void Add_column_Click(object sender, EventArgs e)
        {
            Int32 selected_rows_count = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            for (int i = 0; i < selected_rows_count; ++i)
            {
                foreach (DataGridViewColumnCollection cols in columns_)
                {
                    foreach (DataGridViewColumn col in cols)
                    {
                        if (col.Name == (string)dataGridView1.SelectedRows[i].Cells["Hidden Columns"].Value)
                        {
                            col.Visible = true;
                        }
                    }
                    
                }
            }
            RefreshGrid();
        }

        private void Hide_column_Click(object sender, EventArgs e)
        {
            Int32 selected_rows_count = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Selected);
            for (int i = 0; i < selected_rows_count; ++i)
            {

                foreach (DataGridViewColumnCollection cols in columns_)
                {
                    foreach (DataGridViewColumn col in cols)
                    {
                        if (col.Name == (string)dataGridView2.SelectedRows[i].Cells["Visible Columns"].Value)
                        {
                            col.Visible = false;
                        }

                    }
                }
            }
            RefreshGrid();
        }

        private void Move_up_Click(object sender, EventArgs e)
        {
            DataTable data_table = (DataTable)dataGridView2.DataSource;
           
            foreach (DataGridViewRow selected_row in dataGridView2.SelectedRows)
            {
                int index = selected_row.Index;
                if( index > 0 )
                {
                    foreach (DataGridViewColumnCollection cols in columns_)
                    {
                        DataRow row = data_table.Rows[index];
                        DataRow row_before = data_table.Rows[index - 1];
                        cols[row.Field<string>("Visible Columns")].DisplayIndex = index - 1;
                        cols[row_before.Field<string>("Visible Columns")].DisplayIndex = index;
                        DataRow new_row = data_table.NewRow();
                        new_row.ItemArray = row.ItemArray;
                        data_table.Rows.Remove(row);
                        data_table.Rows.InsertAt(new_row, index - 1);
                    }
                    dataGridView2.Rows[index - 1].Selected = true;
                }
                
            }
            dataGridView2.DataSource = data_table;
            dataGridView2.ClearSelection();
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            DataTable data_table = (DataTable)dataGridView2.DataSource;
           
            foreach (DataGridViewRow selected_row in dataGridView2.SelectedRows)
            {
                int index = selected_row.Index;
                if (index < dataGridView2.RowCount-1)
                {
                    foreach (DataGridViewColumnCollection cols in columns_)
                    {
                        DataRow row = data_table.Rows[index];
                        DataRow row_after = data_table.Rows[index + 1];
                        cols[row.Field<string>("Visible Columns")].DisplayIndex = index + 1;
                        cols[row_after.Field<string>("Visible Columns")].DisplayIndex = index;
                        DataRow new_row = data_table.NewRow();
                        new_row.ItemArray = row.ItemArray;
                        data_table.Rows.Remove(row);
                        data_table.Rows.InsertAt(new_row, index + 1);
                       
                    }
                    dataGridView2.Rows[index+1].Selected = true;
                }
               
            }
            dataGridView2.DataSource = data_table;
           // dataGridView2.ClearSelection();
        }
    }
}
