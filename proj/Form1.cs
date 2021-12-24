using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using Formatting = System.Xml.Formatting;
using LiveCharts;
using LiveCharts.Wpf;

namespace proj
{
    public partial class Form1 : Form
    {
        private string fileName = "D:\\Studying\\Coursework\\proj\\database.XLSX";
        private DataTableCollection tableCollection = null;
        XmlSerializer xs;
        static bool darkTheme = false;

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

        private void OpenToolStrip(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    ShowTable();
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
        }

        private void WeatherButton_Click(object sender, EventArgs e)
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

        private void LogOutLabel_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Hide();
        }

        private void CopyAlltoClipboard()
        {
            //to remove the first blank column from datagridview
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
            {
                Clipboard.SetDataObject(dataObj);
            }
        }
        private void SaveAsEXCEL(object sender, EventArgs e)
        {
            CopyAlltoClipboard();
            Excel.Application xlexcel;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = false;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "№";
            xlWorkSheet.Cells[1, 2] = "Song";
            xlWorkSheet.Cells[1, 3] = "Artist";
            xlWorkSheet.Cells[1, 4] = "Views";
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
            if (toolStripComboBox1.SelectedItem.ToString() != null)
            {
                xlWorkSheet.Name = toolStripComboBox1.SelectedItem.ToString();
            }
            else
            {
                xlWorkSheet.Name = "Sheet";
            }
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    inputParameter.fileName = sfd.FileName;
                    xlWorkBook.SaveAs(sfd.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
            }
            xlexcel.Quit();
        }

        private void SaveAsCSV(object sender, EventArgs e)
        {
            int rowCount = dataGridView1.Rows.Count;
            int cellCount = dataGridView1.Rows[0].Cells.Count;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    inputParameter.fileName = sfd.FileName;
                }
            }
            using (StreamWriter sw = new StreamWriter(new FileStream(inputParameter.fileName, FileMode.Create), Encoding.UTF8))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("№,Song,Artist,Views");
                for (int i = 0; i < rowCount - 1; i++)
                {
                    stringBuilder.AppendLine(string.Format("{0},{1},{2},{3}", dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(), dataGridView1.Rows[i].Cells[3].Value.ToString()));
                }
                sw.Write(stringBuilder.ToString());
            }
        }

        private void SaveAsXML(object sender, EventArgs e)
        {
            {
                DataTable dt = new DataTable();
                dt.TableName = "Songs";

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    string headerText = dataGridView1.Columns[i].HeaderText;
                    headerText = Regex.Replace(headerText, "[-/, ]", "_");

                    DataColumn column = new DataColumn(headerText);
                    dt.Columns.Add(column);
                }

                foreach (DataGridViewRow DataGVRow in dataGridView1.Rows)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["№"] = DataGVRow.Cells["№"].Value;
                    dataRow["Song"] = DataGVRow.Cells["Song"].Value;
                    dataRow["Artist"] = DataGVRow.Cells["Artist"].Value;
                    dataRow["Views"] = DataGVRow.Cells["Views"].Value;
                    dt.Rows.Add(dataRow);
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "XML|*.xml", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        inputParameter.fileName = sfd.FileName;
                    }
                }

                XmlTextWriter xmlSave = new XmlTextWriter(inputParameter.fileName, Encoding.UTF8);
                xmlSave.Formatting = Formatting.Indented;
                ds.DataSetName = "Chart";
                ds.WriteXml(xmlSave);
                xmlSave.Close();
            }
        }

        struct DataParameter
        {
            //public List<Songs> list;
            public string fileName { get; set; }
        }
        DataParameter inputParameter;

        private void SmallSize_Click(object sender, EventArgs e)
        {
            Size = new Size(450, 350);
            this.AutoScroll = true;
        }
        private void MediumSize_Click(object sender, EventArgs e)
        {
            Size = new Size(575, 471);
        }

        private void BigSize_Click(object sender, EventArgs e)
        {
            Size = new Size(1280, 720);
        }

        private void beigeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Tan;
            cityLabel.ForeColor = Color.Black;
            logOutLabel.ForeColor = Color.Black;
            dataGridView1.BackgroundColor = Color.Tan;
            weatherLabel.ForeColor = Color.Black;
            darkTheme = false;
        }
        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            cityLabel.ForeColor = Color.White;
            logOutLabel.ForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.Black;
            weatherLabel.ForeColor = Color.White;
            darkTheme = true;
        }

        private void logOutLabel_MouseLeave(object sender, EventArgs e)
        {
            if (darkTheme)
            {
                logOutLabel.ForeColor = Color.White;
            }
            else
            {
                logOutLabel.ForeColor = Color.Black;
            }
        }

        private void logOutLabel_MouseEnter(object sender, EventArgs e)
        {
            if (darkTheme)
            {
                logOutLabel.ForeColor = Color.Red;
            }
            else
            {
                logOutLabel.ForeColor = Color.White;
            }
        }

        public List<Songs> GetData()
        {
            List<Songs> songs = new List<Songs>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                try
                {
                    Songs song = new Songs();
                    song.Number = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    song.Name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    song.Artist = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    song.Views = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    songs.Add(song);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Data Error!");
                }
            }
            return songs;
        }
        private void showChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphik graphik = new Graphik(GetData());
            graphik.ShowDialog();
        }

        private void ShowWeatherButton_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Views"].Index || e.ColumnIndex == dataGridView1.Columns["№"].Index)
                {
                    int newInteger;
                    if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0)
                    {
                        e.Cancel = true;
                        throw new Exception("Data Error!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

public class Songs
{
    public int Number { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public int Views { get; set; }
}

