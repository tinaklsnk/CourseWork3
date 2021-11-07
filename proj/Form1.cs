﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
    }

    public class Songs
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Singer { get; set; }
    }
}
