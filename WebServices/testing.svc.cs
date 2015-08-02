using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace WebApplication1.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "testing" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select testing.svc or testing.svc.cs at the Solution Explorer and start debugging.
    public class testing : Itesting
    {
        public void DoWork()
        {
        }

        public int Multiple(int a, int b)
        {
            return (a * b);
        }
        public int Addition(int a, int b)
        {
            return (a + b);
        }



        public string databasecall(string username)
        //public List<DataRow> databasecall(string username)
        
            //public object databasecall(string username)
        {
           
            var con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection mycon = new SqlConnection(con);

            try
            {
                
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("Select * from Demo where NAME=@NAME", mycon);
                cmd.Parameters.AddWithValue("@NAME", "hunny");
                mycon.Open();
                SqlDataReader DR1 = cmd.ExecuteReader();
                {
                    if (DR1.HasRows)
                        dt.Load(DR1);
                }
                //return "success";
               // string result = DataTableToJSON(dt);
               // return DatatableToDictionary(dt, "hunny");
               // return DataTableToJSON(dt).ToString();

                //calling datatable to json method

                return DataTableToJsonObj(dt);

                //converting it to  list
                List<DataRow> list = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
                //converting it to  list

                //return list;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
                ////return dt.ToString();

               

            }
            catch (Exception e)
            {
                // cmd.ExecuteScalar();
                mycon.Close();
                //return null;
                return "unsuccessful";
            }




        }

        public Dictionary<string, Dictionary<string, object>> DatatableToDictionary(DataTable dt, string id)
        {
            var cols = dt.Columns.Cast<DataColumn>().Where(c => c.ColumnName != id);
            return dt.Rows.Cast<DataRow>()
                     .ToDictionary(r => r[id].ToString(),
                                   r => cols.ToDictionary(c => c.ColumnName, c => r[c.ColumnName]));
        }

        public static object DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(list);
        }


        public string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               // JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            //JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            JsonString.Append( ds.Tables[0].Columns[j].ColumnName.ToString() + ":" + ds.Tables[0].Rows[i][j].ToString() + ",");

                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            //JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                            JsonString.Append( ds.Tables[0].Columns[j].ColumnName.ToString() + ":" +  ds.Tables[0].Rows[i][j].ToString());

                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
               // JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }

    
}
