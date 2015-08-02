using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Testing;

namespace WebApplication1
{
    public partial class TestingApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //testmethod();
        }

        // public int testmethod(string username)
        public DataTable testmethod(string username)
        {

            ItestingClient client = new ItestingClient();
            //int c = 0;
            //c = client.Multiple(a,b);
            //return c;
            return client.databasecall(username);


        }


        protected void btnclick_Click(object sender, EventArgs e)
        {
            databasecall("hunny");
            //int a = Convert.ToInt32(txtnumone.Text);
            //int b = Convert.ToInt32(txtnumtwo.Text);
            DataTable da = new DataTable();
            da = testmethod("hunny");
            lblmultiply.Visible = true;
            //lblmultiply.Text = "The result of Multiplication is: " + c;
            lblmultiply.Text = "Call is successful ";
            btnclick.Visible = false;

        }

        public DataTable databasecall(string username)
        {
            var con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection mycon = new SqlConnection(con);

            try
            {

                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("Select * from Demo where NAME=@NAME", mycon);
                cmd.Parameters.AddWithValue("@NAME", username);
                mycon.Open();
                SqlDataReader DR1 = cmd.ExecuteReader();
                {
                    if (DR1.HasRows)
                        dt.Load(DR1);
                }

                //converting it to  list
                List<DataRow> list = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
                //converting it to  list


                return dt;
            }
            catch (Exception e)
            {
                // cmd.ExecuteScalar();
                mycon.Close();
                return null;
            }




        }


    }
}