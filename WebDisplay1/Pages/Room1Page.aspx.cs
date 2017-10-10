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
            clearLbls();
        }
        private void clearLbls()
        {
            avgtemprm1.Text = "";
            avghumidityrm1.Text = "";
            avglightrm1.Text = "";
            cfmbrm1.Text = "";
            lbdLbl.Text = "";


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
                    DateTime endDate = DateTime.Now;

                    string avgtempconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avgtempconnect = new SqlConnection(avgtempconnstring))
                    {
                        using (SqlCommand avgtempcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgT FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '25' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avgtempcmd.Connection = avgtempconnect;
                                sda.SelectCommand = avgtempcmd;
                                avgtempconnect.Open();
                                avgtempcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                                avgtempcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                                SqlDataReader avgtempreader = avgtempcmd.ExecuteReader();

                                if (avgtempreader.Read())
                                {

                                    avgtemprm1.Text = avgtempreader["avgT"].ToString() + "&#176" + "C";
                                }
                                avgtempreader.Close();
                                avgtempconnect.Close();
                            }
                        }
                    }                                //Temp

                    string avghumidityconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avghumidityconnect = new SqlConnection(avghumidityconnstring))
                    {
                        using (SqlCommand avghumiditycmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgH FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '7' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avghumiditycmd.Connection = avghumidityconnect;
                                sda.SelectCommand = avghumiditycmd;
                                avghumidityconnect.Open();
                                avghumiditycmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                                avghumiditycmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                                SqlDataReader avghumidityreader = avghumiditycmd.ExecuteReader();

                                if (avghumidityreader.Read())
                                {

                                    avghumidityrm1.Text = avghumidityreader["avgH"].ToString() + "%";
                                }
                                avghumidityreader.Close();
                                avghumidityconnect.Close();
                            }
                        }
                    }                 //humidity
                    string avglightconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avglightconnect = new SqlConnection(avglightconnstring))
                    {
                        using (SqlCommand avglightcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgL FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '13' AND StartDateTime BETWEEN @startDate and @endDate"))
                        //"SELECT COUNT(FAC_CODE) as avgL FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"  //
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avglightcmd.Connection = avglightconnect;
                                sda.SelectCommand = avglightcmd;
                                avglightconnect.Open();
                                avglightcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                                avglightcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                                SqlDataReader avglightreader = avglightcmd.ExecuteReader();

                                if (avglightreader.Read())
                                {

                                    avglightrm1.Text = avglightreader["avgL"].ToString() + "lx";
                                }
                                avglightreader.Close();
                                avglightconnect.Close();
                            }
                        }
                    }               //light

                    string nbrconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection nbrconnect = new SqlConnection(nbrconnstring))
                    {
                        using (SqlCommand nbrcmd = new SqlCommand("SELECT COUNT(FAC_CODE) as avgnbr FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                        // //
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                nbrcmd.Connection = nbrconnect;
                                sda.SelectCommand = nbrcmd;
                                nbrconnect.Open();
                                nbrcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                                nbrcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                                SqlDataReader nbrreader = nbrcmd.ExecuteReader();

                                if (nbrreader.Read())
                                {

                                    peoplebrm1.Text = nbrreader["avgnbr"].ToString();
                                }
                                nbrreader.Close();
                                nbrconnect.Close();
                            }
                        }
                    } //Number of people booked the room
                    try
                    {
                        string cfmbconnstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                        using (SqlConnection cfmbconnect = new SqlConnection(cfmbconnstring))
                        {
                            using (SqlCommand cfmbcmd = new SqlCommand("SELECT COUNT(FAC_CODE) * 100/ (SELECT COUNT(CONFIRMED)) as percentage FROM [TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                            // //
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cfmbcmd.Connection = cfmbconnect;
                                    sda.SelectCommand = cfmbcmd;
                                    cfmbconnect.Open();
                                    cfmbcmd.Parameters.AddWithValue("@startDate", startDate.ToString("s"));
                                    cfmbcmd.Parameters.AddWithValue("@endDate", endDate.ToString("s"));
                                    SqlDataReader cfmbreader = cfmbcmd.ExecuteReader();

                                    if (cfmbreader.Read())
                                    {

                                        cfmbrm1.Text = cfmbreader["percentage"].ToString();
                                    }
                                    cfmbreader.Close();
                                    cfmbconnect.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        cfmbrm1.Text = "0%";
                    }


                    //Percentage of people that confirmed booking

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
                case "View past week":
                    DateTime startDatewk = DateTime.Now.AddDays(-7);
                    DateTime endDatewk = DateTime.Now;
                    string avgtempconnstringwk = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avgtempconnect = new SqlConnection(avgtempconnstringwk))
                    {
                        using (SqlCommand avgtempcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgTw FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '25' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avgtempcmd.Connection = avgtempconnect;
                                sda.SelectCommand = avgtempcmd;
                                avgtempconnect.Open();
                                avgtempcmd.Parameters.AddWithValue("@startDate", startDatewk.ToString("s"));
                                avgtempcmd.Parameters.AddWithValue("@endDate", endDatewk.ToString("s"));
                                SqlDataReader avgtempreader = avgtempcmd.ExecuteReader();

                                if (avgtempreader.Read())
                                {

                                    avgtemprm1.Text = avgtempreader["avgTw"].ToString() + "&#176" + "C";
                                }
                                avgtempreader.Close();
                                avgtempconnect.Close();
                            }
                        }
                    }                                //Temp

                    string avghumidityconnstringwk = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avghumidityconnect = new SqlConnection(avghumidityconnstringwk))
                    {
                        using (SqlCommand avghumiditycmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgHw FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '7' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avghumiditycmd.Connection = avghumidityconnect;
                                sda.SelectCommand = avghumiditycmd;
                                avghumidityconnect.Open();
                                avghumiditycmd.Parameters.AddWithValue("@startDate", startDatewk.ToString("s"));
                                avghumiditycmd.Parameters.AddWithValue("@endDate", endDatewk.ToString("s"));
                                SqlDataReader avghumidityreader = avghumiditycmd.ExecuteReader();

                                if (avghumidityreader.Read())
                                {

                                    avghumidityrm1.Text = avghumidityreader["avgHw"].ToString() + "%";
                                }
                                avghumidityreader.Close();
                                avghumidityconnect.Close();
                            }
                        }
                    }                 //humidity
                    string avglightconnstringwk = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avglightconnect = new SqlConnection(avglightconnstringwk))
                    {
                        using (SqlCommand avglightcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgLw FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '13' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avglightcmd.Connection = avglightconnect;
                                sda.SelectCommand = avglightcmd;
                                avglightconnect.Open();
                                avglightcmd.Parameters.AddWithValue("@startDate", startDatewk.ToString("s"));
                                avglightcmd.Parameters.AddWithValue("@endDate", endDatewk.ToString("s"));
                                SqlDataReader avglightreader = avglightcmd.ExecuteReader();

                                if (avglightreader.Read())
                                {

                                    avglightrm1.Text = avglightreader["avgLw"].ToString() + "lx";
                                }
                                avglightreader.Close();
                                avglightconnect.Close();
                            }
                        }
                    }               //light

                    string lbdconnstringwk = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection lbdconnectwk = new SqlConnection(lbdconnstringwk);

                    string lbdstringwk = "SELECT TOP 1[START_DATE] AS lbrm1w FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' ORDER BY START_DATE DESC;";
                    SqlCommand lbdcmdwk = new SqlCommand(lbdstringwk, lbdconnectwk);

                    lbdconnectwk.Open();

                    SqlDataReader lbdreaderwk = lbdcmdwk.ExecuteReader();
                    if (lbdreaderwk.Read())
                    {
                        lbdLbl.Text = lbdreaderwk["lbrm1w"].ToString();
                    }

                    lbdreaderwk.Close();
                    lbdconnectwk.Close();

                    string nbrconnstringw = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection nbrconnect = new SqlConnection(nbrconnstringw))
                    {
                        using (SqlCommand nbrcmd = new SqlCommand("SELECT COUNT(FAC_CODE) as avgnbrw FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                        // //
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                nbrcmd.Connection = nbrconnect;
                                sda.SelectCommand = nbrcmd;
                                nbrconnect.Open();
                                nbrcmd.Parameters.AddWithValue("@startDate", startDatewk.ToString("s"));
                                nbrcmd.Parameters.AddWithValue("@endDate", endDatewk.ToString("s"));
                                SqlDataReader nbrreader = nbrcmd.ExecuteReader();

                                if (nbrreader.Read())
                                {

                                    peoplebrm1.Text = nbrreader["avgnbrw"].ToString();
                                }
                                nbrreader.Close();
                                nbrconnect.Close();
                            }
                        }
                    } //Number of people booked the room
                    try
                    {
                        string cfmbconnstringw = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                        using (SqlConnection cfmbconnectw = new SqlConnection(cfmbconnstringw))
                        {
                            using (SqlCommand cfmbcmd = new SqlCommand("SELECT COUNT(FAC_CODE) * 100/ (SELECT COUNT(CONFIRMED)) as percentagew FROM [TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                            // //
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cfmbcmd.Connection = cfmbconnectw;
                                    sda.SelectCommand = cfmbcmd;
                                    cfmbconnectw.Open();
                                    cfmbcmd.Parameters.AddWithValue("@startDate", startDatewk.ToString("s"));
                                    cfmbcmd.Parameters.AddWithValue("@endDate", endDatewk.ToString("s"));
                                    SqlDataReader cfmbreader = cfmbcmd.ExecuteReader();

                                    if (cfmbreader.Read())
                                    {

                                        cfmbrm1.Text = cfmbreader["percentagew"].ToString();
                                    }
                                    cfmbreader.Close();
                                    cfmbconnectw.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        cfmbrm1.Text = "0%";
                    }


                    break;

                case "View past month":
                    DateTime startDatemth = DateTime.Now.AddDays(-30);
                    DateTime endDatemth = DateTime.Now;
                    string avgtempconnstringmth = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avgtempconnect = new SqlConnection(avgtempconnstringmth))
                    {
                        using (SqlCommand avgtempcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgTm FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '25' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avgtempcmd.Connection = avgtempconnect;
                                sda.SelectCommand = avgtempcmd;
                                avgtempconnect.Open();
                                avgtempcmd.Parameters.AddWithValue("@startDate", startDatemth.ToString("s"));
                                avgtempcmd.Parameters.AddWithValue("@endDate", endDatemth.ToString("s"));
                                SqlDataReader avgtempreader = avgtempcmd.ExecuteReader();

                                if (avgtempreader.Read())
                                {

                                    avgtemprm1.Text = avgtempreader["avgTm"].ToString() + "&#176" + "C";
                                }
                                avgtempreader.Close();
                                avgtempconnect.Close();
                            }
                        }
                    }                                //Temp

                    string avghumidityconnstringmth = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avghumidityconnect = new SqlConnection(avghumidityconnstringmth))
                    {
                        using (SqlCommand avghumiditycmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgHm FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '7' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avghumiditycmd.Connection = avghumidityconnect;
                                sda.SelectCommand = avghumiditycmd;
                                avghumidityconnect.Open();
                                avghumiditycmd.Parameters.AddWithValue("@startDate", startDatemth.ToString("s"));
                                avghumiditycmd.Parameters.AddWithValue("@endDate", endDatemth.ToString("s"));
                                SqlDataReader avghumidityreader = avghumiditycmd.ExecuteReader();

                                if (avghumidityreader.Read())
                                {

                                    avghumidityrm1.Text = avghumidityreader["avgHm"].ToString() + "%";
                                }
                                avghumidityreader.Close();
                                avghumidityconnect.Close();
                            }
                        }
                    }                 //humidity
                    string avglightconnstringmth = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection avglightconnect = new SqlConnection(avglightconnstringmth))
                    {
                        using (SqlCommand avglightcmd = new SqlCommand("SELECT ROUND(AVG(CAST(Value AS FLOAT)), 2) as avgLm FROM [TestDB].[dbo].[smartDBallV33] WHERE SensorId = '13' AND StartDateTime BETWEEN @startDate and @endDate"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                avglightcmd.Connection = avglightconnect;
                                sda.SelectCommand = avglightcmd;
                                avglightconnect.Open();
                                avglightcmd.Parameters.AddWithValue("@startDate", startDatemth.ToString("s"));
                                avglightcmd.Parameters.AddWithValue("@endDate", endDatemth.ToString("s"));
                                SqlDataReader avglightreader = avglightcmd.ExecuteReader();

                                if (avglightreader.Read())
                                {

                                    avglightrm1.Text = avglightreader["avgLm"].ToString() + "lx";
                                }
                                avglightreader.Close();
                                avglightconnect.Close();
                            }
                        }
                    }               //light

                    string lbdconnstringmth = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                    SqlConnection lbdconnectmth = new SqlConnection(lbdconnstringmth);

                    string lbdstringmth = "SELECT TOP 1[START_DATE] AS lbrm1m FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' ORDER BY START_DATE DESC;";
                    SqlCommand lbdcmdmth = new SqlCommand(lbdstringmth, lbdconnectmth);

                    lbdconnectmth.Open();

                    SqlDataReader lbdreadermth = lbdcmdmth.ExecuteReader();
                    if (lbdreadermth.Read())
                    {
                        lbdLbl.Text = lbdreadermth["lbrm1m"].ToString();
                    }

                    lbdreadermth.Close();
                    lbdconnectmth.Close();

                    string nbrconnstringm = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    using (SqlConnection nbrconnect = new SqlConnection(nbrconnstringm))
                    {
                        using (SqlCommand nbrcmd = new SqlCommand("SELECT COUNT(FAC_CODE) as avgnbrm FROM[TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                        // //
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                nbrcmd.Connection = nbrconnect;
                                sda.SelectCommand = nbrcmd;
                                nbrconnect.Open();
                                nbrcmd.Parameters.AddWithValue("@startDate", startDatemth.ToString("s"));
                                nbrcmd.Parameters.AddWithValue("@endDate", endDatemth.ToString("s"));
                                SqlDataReader nbrreader = nbrcmd.ExecuteReader();

                                if (nbrreader.Read())
                                {

                                    peoplebrm1.Text = nbrreader["avgnbrm"].ToString();
                                }
                                nbrreader.Close();
                                nbrconnect.Close();
                            }
                        }
                    } //Number of people booked the room
                    try
                    {
                        string cfmbconnstringm = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                        using (SqlConnection cfmbconnect = new SqlConnection(cfmbconnstringm))
                        {
                            using (SqlCommand cfmbcmd = new SqlCommand("SELECT COUNT(FAC_CODE) * 100/ (SELECT COUNT(CONFIRMED)) as percentagem FROM [TestDB].[dbo].[FRS$] WHERE FAC_CODE = 'L.431' AND BOOK_DATE BETWEEN @startDate and @endDate"))
                            // //
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cfmbcmd.Connection = cfmbconnect;
                                    sda.SelectCommand = cfmbcmd;
                                    cfmbconnect.Open();
                                    cfmbcmd.Parameters.AddWithValue("@startDate", startDatemth.ToString("s"));
                                    cfmbcmd.Parameters.AddWithValue("@endDate", endDatemth.ToString("s"));
                                    SqlDataReader cfmbreader = cfmbcmd.ExecuteReader();

                                    if (cfmbreader.Read())
                                    {

                                        cfmbrm1.Text = cfmbreader["percentagem"].ToString();
                                    }
                                    cfmbreader.Close();
                                    cfmbconnect.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        cfmbrm1.Text = "0%";
                    }
                    break;
                    //end


            }
        }
    }
}


