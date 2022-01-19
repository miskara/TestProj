using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Congratulater
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = "SELECT Email,Date, " +
"attributes = STUFF( " +
"                 (SELECT ',' + attribute FROM Emails emm where em.Email = emm.Email and em.Date = emm.Date FOR XML PATH('')), 1, 1, '' " +
"               ),  " +
"COUNT(1) as AttributeCount " +
"FROM emails em " +
"where em.Date = CAST(Getdate() as date) " +
"GROUP BY Email,Date " +
"having COUNT(1)>=10 ";

            DataTable tbl = new DataTable();
            SqlConnection conn = new SqlConnection("Server = KTLT025\\KTLT025A; Database = JsonTst; Trusted_Connection = True; ");
            conn.Open();
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            ad.Fill(tbl);
            conn.Close();

            foreach (DataRow d in tbl.Rows)
            {
                AddCongratulation(d["attributes"].ToString(),d["Email"].ToString());
            }
        }

        private static void AddCongratulation(string attributelist,string email)
        {
            SqlConnection conn = new SqlConnection("Server = KTLT025\\KTLT025A; Database = JsonTst; Trusted_Connection = True; ");
            conn.Open();
            string body = "Congratulate! \r\n\r\n" +
                          "We have received following unique attributes from you: " + attributelist + "\r\n\r\n" +
                          "Best regards, Millisecond";

            string insert = "INSERT INTO[dbo].[Congrats] " +
           "([EmailAddress] " +
           ",[EmailBody]) " +
     "VALUES " +
           "('"+email+"' " +
           ", '"+body+"') ";
            SqlCommand comm = new SqlCommand(insert,conn);
            comm.ExecuteNonQuery();
        }
    }
}
