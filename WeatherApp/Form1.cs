using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "338b9134b4b0c86afe88c97b28648d6c";

        private void btn_search_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TbCity.Text, APIKey);
                    var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                pic_icon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                lab_conditions.Text = Info.weather[0].main;
                lab_details.Text = Info.weather[0].description;
                lab_sunset.Text = convertDateTime(Info.sys.sunset).ToString();
                lab_sunrise.Text = convertDateTime(Info.sys.sunrise).ToString();
                lab_windspeed.Text = Info.wind.speed.ToString();
                lab_pressure.Text = Info.main.pressure.ToString();

            }

        }

        DateTime convertDateTime(long milisec) 

        {
            DateTime day = new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc);
            day = day.AddSeconds(milisec).ToLocalTime();
            return day;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lab_detail_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
