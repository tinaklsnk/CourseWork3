using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Excel = Microsoft.Office.Interop.Excel;

namespace proj
{
    public partial class Form1 : Form
    {
        private string fileName = "D:\\Studying\\Coursework\\proj\\database.XLSX";
        private DataTableCollection tableCollection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "MyApp";
            //notifyIcon1.BalloonTipText = "Closed";
            notifyIcon1.Text = "MyApp";
            ShowTable();
            dataGridView1.ReadOnly = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowTable()
        {
            try
            {
                if (fileName != null)
                {
                    Regex regexExcel = new Regex(@"\w*xlsx|XLSX$");
                    MatchCollection matches = regexExcel.Matches(fileName);
                    if (matches.Count > 0)
                    {
                        OpenExcelFile(fileName);
                    }
                    Regex regexCSV = new Regex(@"\w*csv$");
                    matches = regexCSV.Matches(fileName);
                    if (matches.Count > 0)
                    {
                        OpenCSVFile(fileName);
                    }
                }
                else
                {
                    throw new Exception("File not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = tableCollection[Convert.ToString(toolStripComboBox1.SelectedItem)];
            dataGridView1.DataSource = table;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    ShowTable();
                    //OpenExcelFile(fileName);
                }
                else
                {
                    throw new Exception("File not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenExcelFile(string path)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

            DataSet db = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            }
            );

            tableCollection = db.Tables;
            toolStripComboBox1.Text = "";
            toolStripComboBox1.Items.Clear();

            foreach (DataTable table in tableCollection)
            {
                toolStripComboBox1.Items.Add(table.TableName);
            }

            toolStripComboBox1.SelectedIndex = 0;
        }

        private void OpenCSVFile(string path)
        {
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Text = "";
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 0)
            {
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    headerWord.Replace("\"", "");
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }
    }

    public class Songs
    {
        public int Number { get; set; }
        public string Song { get; set; }
        public string Singer { get; set; }
    }
}
