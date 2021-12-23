using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel; 

namespace proj
{
    public partial class Form1 : Form
    {
        private string fileName = "D:\\Studying\\Coursework\\proj\\database.XLSX";
        private DataTableCollection tableCollection = null;
        XmlSerializer xs;

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
                    Regex regexXML = new Regex(@"\w*xml$");
                    matches = regexXML.Matches(fileName);
                    if (matches.Count > 0)
                    {
                        OpenXMLFile(fileName);
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
            });

            tableCollection = db.Tables;
            toolStripComboBox1.Text = "";
            toolStripComboBox1.Items.Clear();

            foreach (DataTable table in tableCollection)
            {
                toolStripComboBox1.Items.Add(table.TableName);
            }
            toolStripComboBox1.SelectedIndex = 0;
            stream.Close();
            reader.Close();
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

        private void OpenXMLFile(string path)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            dataGridView1.DataSource = ds.Tables[0];
            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //Songs s = new Songs();
            //List<Songs> list = (List<Songs>)xs.Deserialize(fs);
        }

        private void weatherButton_Click(object sender, EventArgs e)
        {
            try
            {
                string city = cityBox.Text;
                if (city != String.Empty)
                {
                    JSON j = new JSON();
                    string myJsonResponse = j.GetJSON(city);
                    if (myJsonResponse != String.Empty)
                    {
                        Root data = JsonConvert.DeserializeObject<Root>(myJsonResponse);
                        double temp = Convert.ToDouble(data.list[0].main.temp) - 273.15;
                        weatherLabel.Text = Math.Round(temp).ToString() + "°C " + data.list[0].weather[0].main;
                    }
                    else
                    {
                        throw new Exception("City not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Enter the city!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void AllowEditTable()
        {
            dataGridView1.ReadOnly = false;
        }

        private void labelLogOut_MouseEnter(object sender, EventArgs e)
        {
            labelLogOut.ForeColor = Color.White;
        }

        private void labelLogOut_MouseLeave(object sender, EventArgs e)
        {
            labelLogOut.ForeColor = Color.Black;
        }

        private void labelLogOut_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Hide();
        }

        private void copyAlltoClipboard()
        {
            //to remove the first blank column from datagridview
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void eXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Excel.Application xlexcel;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = false;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
            xlWorkSheet.Cells[1, 1] = "№";
            xlWorkSheet.Cells[1, 2] = "Song";
            xlWorkSheet.Cells[1, 3] = "Artist";
            xlWorkSheet.Cells[1, 4] = "Views";
            xlWorkSheet.Name = toolStripComboBox1.SelectedItem.ToString();
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    inputParameter.fileName = sfd.FileName;
                    xlWorkBook.SaveAs(sfd.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
            }
            xlexcel.Quit();
        }
        //private void eXCELToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    worksheet = workbook.Sheets["Sheet1"];
        //    worksheet = workbook.ActiveSheet;
        //    worksheet.Name = "Worksheet Name";
        //    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
        //    {
        //        worksheet.Cells[i, 1] = dataGridView1.Columns[i - 1].HeaderText;
        //    }

        //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < dataGridView1.Columns.Count; j++)
        //        {
        //            worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
        //        }
        //    }


        //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel|*.xlsx"})
        //    {
        //        if(sfd.ShowDialog()== DialogResult.OK)
        //        {
        //            inputParameter.fileName = sfd.FileName;
        //            workbook.SaveAs(sfd.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        }
        //    }
        //}

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        struct DataParameter
        {
            public List<Songs> list;
            public string fileName { get; set; } 
        }

        DataParameter inputParameter;
    }

    public class Songs
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Singer { get; set; }
        public int Views { get; set; }
    }
}
