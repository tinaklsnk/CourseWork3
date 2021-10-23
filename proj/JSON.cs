using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj
{
    class JSON
    {
        private static String USER_AGENT = "Mozilla/5.0";
        private static String GET_URL_1 = "http://api.openweathermap.org/data/2.5/forecast?q=";
        private static String GET_URL_2 = "&APPID=a17a1c41a27a1c5560aa675c81bd1b2d";

        public string GetJSON(string city)
        {
            string myPageSource = string.Empty;
            try
            {
                string url = GET_URL_1 + city + GET_URL_2;
                HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myWebRequest.Method = "GET";
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                StreamReader myWebSource = new StreamReader(myWebResponse.GetResponseStream());
                myPageSource = myWebSource.ReadToEnd();
                myWebResponse.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "City not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return myPageSource;
        }
    }
}
