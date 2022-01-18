using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TestProj;

namespace Logger
{
    class Program
    {
        static async Task Main(string[] args)


        { // define the connection string with the Storage 
            string connectString = "DefaultEndpointsProtocol=https;AccountName=teststoragemiskarantanen;AccountKey=uPuCiXOd/uCm/MzVyHUNf8aCNxN8sXN7moIPLeqami0OObmeXLWRNGAw+A6pKZth/58KY8nJdIznk8AgqVC95w==;EndpointSuffix=core.windows.net";

            // define container's name
            string containerName = "log";

            // define log file's name
            string logFileName = "TestLog.log";

            // define the Cloud Storage Account
            var storageAccount = CloudStorageAccount.Parse(connectString);

            // define the Cloud Blob Client used to manage the containers
            var blobClient = storageAccount.CreateCloudBlobClient();
            string query = "SELECT Email,Date, " +
"attributes = STUFF( " +
"                 (SELECT ',' + attribute FROM Emails emm where em.Email = emm.Email and em.Date = emm.Date FOR XML PATH('')), 1, 1, '' " +
"               )  " +
"FROM emails em " +
"where em.Date = CAST(Getdate() as date) " +
"GROUP BY Email,Date";
            DataTable tbl = new DataTable();
            SqlConnection conn = new SqlConnection("Server = KTLT025\\KTLT025A; Database = JsonTst; Trusted_Connection = True; ");
            conn.Open();
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            ad.Fill(tbl);
            conn.Close();
            foreach (DataRow d in tbl.Rows)
            {
                string value = "attributes added today: " + d["attributes"];
                // write log file

                await WriteLog("log", d["Email"].ToString() + "_" + DateTime.Today.ToString("yyyy_MM_dd") + ".log", blobClient, value);
            }

        }

       static async Task WriteLog(string nameContainer, string nameLogFile, CloudBlobClient objBlobClient, string newValue)
        {
            // take Container's reference
            var container = objBlobClient.GetContainerReference(nameContainer.ToString());

            // take Blob's reference to modify
            var blob = container.GetAppendBlobReference(nameLogFile);

            // verify the existance of blob
            bool isPresent = await blob.ExistsAsync();

            // if blob doesn't exist, the system will create it
            if (!isPresent)
            {
                await blob.CreateOrReplaceAsync();
            }

            // append the new value and new line
            await blob.AppendTextAsync($"{newValue} \n");
        }
    }
}