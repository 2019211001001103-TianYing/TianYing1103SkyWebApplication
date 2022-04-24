using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TianYing1103SkyWebApplication.LOB
{
    public partial class QueryStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnQueryStatus_Click(object sender, EventArgs e)
        {
            lblMessage.Text ="";
            if (txtFlightNo.Text ==""|| txtFlightNo.Text == null){
                lblMessage.Text = "Invalid flight number";
                return;
            }else
            {
                String ConnectionString = ConfigurationManager.ConnectionStrings["ARPDatabaseConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                string selectSql4 = "SELECT FItNo FROM dtFltDetails";
                SqlCommand cmd4 = new SqlCommand(selectSql4, conn);
                SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                DataSet dataSet4 = new DataSet();
                adapter4.Fill(dataSet4, "FlightNo");
                conn.Close();
                Boolean flightStatus = false;
                foreach (DataRow row in dataSet4.Tables["FLightNo"].Rows)
                {
                    if (row[0].ToString().Trim() == txtFlightNo.Text.Trim()){
                        flightStatus=true;
                        break;
                    }
                }
                if (flightStatus)
                {
                    string selectSql = "SELECT FltNo, StatusDate, StatusClass, Status FROM dtFltStatus where FltNo=@fltno and StatusDate=@date and StatusClass=@class";
                    SqlCommand cmd = new SqlCommand(selectSql, conn);
                    cmd.Parameters.AddWithValue("@FltNo", txtFlightNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@date", Calendar1.SelectedDate.ToShortDateString());
                    cmd.Parameters.AddWithValue("@class", lstClass.SelectedItem.Text.Trim());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd) ;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "FltStatus");
                    conn.Close();
                    if(dataSet.Tables["FltStatus"].Rows.Count == 0)
                    {
                        lblMessage.Text = "Status: Available";
                    }
                    else
                    {
                        String strStatus = "";
                        int status;
                        strStatus = dataSet.Tables["FltStatus"].Rows[0][3].ToString();
                        status = Convert.ToInt32(strStatus);
                        if (status > 0)
                        {
                            lblMessage.Text = "Status: Available";
                        }
                        else
                        {
                            lblMessage.Text = "Status: Overbooked";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid Flight Number";
                }
            }
        }
    }
}