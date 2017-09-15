using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDisplay1.Pages
{
    public partial class TestPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tempconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection tempconnect = new SqlConnection(tempconnstring);

            string tempstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '26' ORDER BY StartDateTime DESC; ";
            SqlCommand tempcmd = new SqlCommand(tempstring, tempconnect);

            tempconnect.Open();

            SqlDataReader tempreader = tempcmd.ExecuteReader();
            if (tempreader.Read())
            {
                tempLbl.Text = tempreader["Value"].ToString();
            }

            tempreader.Close();
            tempconnect.Close();        //Temperature values

            string humidityconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection humidityconnect = new SqlConnection(humidityconnstring);

            string humiditystring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '24' ORDER BY StartDateTime DESC; ";
            SqlCommand humiditycmd = new SqlCommand(humiditystring, humidityconnect);

            humidityconnect.Open();

            SqlDataReader humidityreader = humiditycmd.ExecuteReader();
            if (humidityreader.Read())
            {
                humidityLbl.Text = humidityreader["Value"].ToString();
            }

            humidityreader.Close();
            humidityconnect.Close();            //Humidity Values


            string lightconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection lightconnect = new SqlConnection(lightconnstring);

            string lightstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '5' ORDER BY StartDateTime DESC; ";
            SqlCommand lightcmd = new SqlCommand(lightstring, lightconnect);

            lightconnect.Open();

            SqlDataReader lightreader = lightcmd.ExecuteReader();
            if (lightreader.Read())
            {
               lightLbl.Text = lightreader["Value"].ToString();
            }

            lightreader.Close();
            lightconnect.Close();            //Light Values
            
            string motionconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection motionconnect = new SqlConnection(motionconnstring);

            string motionstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '19' ORDER BY StartDateTime DESC; ";
            SqlCommand motioncmd = new SqlCommand(motionstring, motionconnect);

            motionconnect.Open();

            SqlDataReader motionreader = motioncmd.ExecuteReader();
            if (motionreader.Read())
            {
                motionLbl.Text = motionreader["Value"].ToString();
            }

            motionreader.Close();
            motionconnect.Close();            //Motion Values
        }
    }
}