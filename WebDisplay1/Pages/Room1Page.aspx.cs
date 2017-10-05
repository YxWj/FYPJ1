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
    public partial class Room1Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rm1measurement();
            
        }

        private void rm1measurement()
        {
            string tempconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection tempconnect = new SqlConnection(tempconnstring);

            string tempstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '25' ORDER BY StartDateTime DESC; ";
            SqlCommand tempcmd = new SqlCommand(tempstring, tempconnect);

            tempconnect.Open();

            SqlDataReader tempreader = tempcmd.ExecuteReader();
            if (tempreader.Read())
            {
                tempLblrm1.Text = tempreader["Value"].ToString() + "&#176" + "C";
            }

            tempreader.Close();
            tempconnect.Close();        //Temperature values

            string humidityconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection humidityconnect = new SqlConnection(humidityconnstring);

            string humiditystring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '7' ORDER BY StartDateTime DESC; ";
            SqlCommand humiditycmd = new SqlCommand(humiditystring, humidityconnect);

            humidityconnect.Open();

            SqlDataReader humidityreader = humiditycmd.ExecuteReader();
            if (humidityreader.Read())
            {
                humidityLblrm1.Text = humidityreader["Value"].ToString() + "%";
            }

            humidityreader.Close();
            humidityconnect.Close();            //Humidity Values


            string lightconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection lightconnect = new SqlConnection(lightconnstring);

            string lightstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '13' ORDER BY StartDateTime DESC; ";
            SqlCommand lightcmd = new SqlCommand(lightstring, lightconnect);

            lightconnect.Open();

            SqlDataReader lightreader = lightcmd.ExecuteReader();
            if (lightreader.Read())
            {
                lightLblrm1.Text = lightreader["Value"].ToString() + "lx";
            }

            lightreader.Close();
            lightconnect.Close();            //Light Values

            string motionconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            SqlConnection motionconnect = new SqlConnection(motionconnstring);

            string motionstring = "SELECT TOP 1 SensorId, Value, StartDateTime FROM smartDBallV33 WHERE SensorId = '1' ORDER BY StartDateTime DESC; ";
            SqlCommand motioncmd = new SqlCommand(motionstring, motionconnect);


            motionconnect.Open();

            SqlDataReader motionreader = motioncmd.ExecuteReader();


            if (motionreader.Read())
            {
                //motionLblrm1.Text = motionreader["Value"].ToString() + " Newton";
                string motionvalue = motionreader["Value"].ToString();
                if (motionvalue == "1")
                {
                    motionLblrm1.Text = "\u221A";
                }
                else
                {
                    motionLblrm1.Text = "X";
                }
            }

            motionreader.Close();
            motionconnect.Close();            //Motion Values

            //--------------------------------------1st Room-------------------------------//
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string indexSelected = DropDownList1.SelectedItem.ToString();
            switch (indexSelected)
            {
                case "View past day":
                    DateTime startDate = DateTime.Now.AddDays(-1);
                    DateTime endDate = DateTime.Now.AddDays(+1);
                    string avgtempconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection avgtempconnect = new SqlConnection(avgtempconnstring);

                    string avgtempstring = "SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgT FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '25' AND cast(StartDateTime as datetime) BETWEEN @startDate and @endDate";
                    SqlCommand avgtempcmd = new SqlCommand(avgtempstring, avgtempconnect);

                    avgtempconnect.Open();

                    SqlDataReader avgtempreader = avgtempcmd.ExecuteReader();
                    avgtempcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                    avgtempcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                    if (avgtempreader.Read())
                    {

                        avgtemprm1.Text = avgtempreader["avgT"].ToString() + "&#176" + "C";
                    }

                    avgtempreader.Close();
                    avgtempconnect.Close();         //Temp

                    string avghumidityconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection avghumidityconnect = new SqlConnection(avghumidityconnstring);

                    string avghumiditystring = "SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgH FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '7' AND cast(StartDateTime as datetime) BETWEEN @startDate and @endDate";
                    SqlCommand avghumiditycmd = new SqlCommand(avghumiditystring, avghumidityconnect);

                    avghumidityconnect.Open();
                    avgtempcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                    avgtempcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                    SqlDataReader avghumidityreader = avghumiditycmd.ExecuteReader();
                    if (avghumidityreader.Read())
                    {
                        avghumidityrm1.Text = avghumidityreader["avgH"].ToString() + "%";
                    }

                    avghumidityreader.Close();
                    avghumidityconnect.Close();         //humidity

                    string avgLightconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection avgLightconnect = new SqlConnection(avgLightconnstring);

                    string avgLightstring = "SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgL FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '13' AND cast(StartDateTime as datetime) BETWEEN @startDate and @endDate";
                    SqlCommand avgLightcmd = new SqlCommand(avgLightstring, avgLightconnect);

                    avgLightconnect.Open();
                    avgLightcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                    avgLightcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                    SqlDataReader avgLightreader = avgLightcmd.ExecuteReader();
                    if (avgLightreader.Read())
                    {
                       avglightrm1.Text = avgLightreader["avgL"].ToString() + "lx";
                    }

                    avgLightreader.Close();
                    avgLightconnect.Close();         //light

                    //string bRoom1connstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    //SqlConnection bRoom1connect = new SqlConnection(bRoom1connstring);

                    //string bRoom1string = "";
                    //SqlCommand bRoom1cmd = new SqlCommand(bRoom1string, bRoom1connect);

                    //bRoom1connect.Open();

                    //SqlDataReader bRoom1reader = bRoom1cmd.ExecuteReader();
                    //if (bRoom1reader.Read())
                    //{
                    //    peoplebrm1.Text = bRoom1reader[""].ToString();
                    //}

                    //bRoom1reader.Close();
                    //bRoom1connect.Close();         //Number of people booked the room
                    

                    //string cfmBkconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    //SqlConnection cfmBkconnect = new SqlConnection(cfmBkconnstring);

                    //string cfmBkstring = "";
                    //SqlCommand cfmBkcmd = new SqlCommand(cfmBkstring, cfmBkconnect);

                    //cfmBkconnect.Open();

                    //SqlDataReader cfmBkreader = cfmBkcmd.ExecuteReader();
                    //if (cfmBkreader.Read())
                    //{
                    //    cfmbrm1.Text = cfmBkreader[""].ToString();
                    //}

                    //cfmBkreader.Close();
                    //cfmBkconnect.Close();         //Percentage of people that confirmed booking

                    string lbdconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection lbdconnect = new SqlConnection(lbdconnstring);

                    string lbdstring = "SELECT TOP 1[START_DATE] AS lbrm1 FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' ORDER BY START_DATE DESC;";
                    SqlCommand lbdcmd = new SqlCommand(lbdstring, lbdconnect);

                    lbdconnect.Open();

                    SqlDataReader lbdreader = lbdcmd.ExecuteReader();
                    if (lbdreader.Read())
                    {
                        lbdLbl.Text = lbdreader["lbrm1"].ToString();
                    }

                    lbdreader.Close();
                    lbdconnect.Close();         //last booking date


                    break;
                case "week":
                    DateTime startDatewk = DateTime.Now.AddDays(-7);
                    DateTime endDatewk = DateTime.Now(+1);

                    break;
                case "month":

                    break;
            }
        }

    }
}


