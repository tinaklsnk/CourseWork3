using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace proj
{
    public partial class Graphik : Form
    {
        List<Songs> songs;
        public Graphik(List<Songs> songs)
        {
            InitializeComponent();
            this.songs = songs;
        }

        private void Graphik_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < songs.Count; i++)
            {
                chart1.Series[0].Points.AddXY(songs[i].Name, songs[i].Views);
            }
        }
    }
}
