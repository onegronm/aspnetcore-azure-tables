using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace aspnetcore_azure_tables.Data
{
    public class Common
    {

        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=omarsstorageaccount;AccountKey=+fUdPe6+Thdjo+AI/FI9gfoOAr7HLVu/2XAOiWj0AEA8Sb6pFE9W3Stumn0NPLS0Z2MdW2ZO2Lo7dMbVNDcNOQ==;EndpointSuffix=core.windows.net";
            CloudStorageAccount cloudStorageAccount;

            if (CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Console.WriteLine("Connection string is valid.");
                CloudTableClient client = cloudStorageAccount.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(tableName);

                if (await table.CreateIfNotExistsAsync())
                {
                    Console.WriteLine($"Created table {tableName}");
                }
                else
                {
                    Console.WriteLine($"Table {tableName} already exists");
                }

                return table;
            }
            else
            {
                Console.WriteLine("Connection string is valid.");
                return null;
            }
        }

    }
}
