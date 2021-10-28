using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace aspnetcore_azure_tables.Data
{
    public class Operations
    {
        public async Task TriggerOperations()
        {
            string tableName = "Employee";

            CloudTable cloudTable = await Common.CreateTableAsync(tableName);

            try
            {
                await AddDataOperations(cloudTable);
                await UpdateDataOperations(cloudTable);
                await RetrieveAllDataOperation(cloudTable);
                var employee = await RetrieveSingleDataOperation(cloudTable, "Consulting", "d26f0d79-cc01-4074-bc5b-dcc21673c202");
                await DeleteDataOperation(cloudTable, employee);
            }
            catch (Exception ex)
            {

            }
        }

        public static Task RetrieveAllDataOperation(CloudTable cloudTable)
        {
            var query = cloudTable.ExecuteQuery(new TableQuery<EmployeeEntity>());

            foreach (var employee in query)
            {
                Console.WriteLine($"RowKey: {employee.RowKey}, PartitionKey: {employee.PartitionKey}, FullName: {employee.FullName}");
            }

            return Task.CompletedTask;
        }

        public static async Task AddDataOperations(CloudTable cloudTable)
        {
            // create entity
            EmployeeEntity newEmployee = new EmployeeEntity("Consulting")
            {
                FullName = "Omar Negron Montero",
                Email = "omar.negronmontero@crowe.com",
                RowKey = Guid.NewGuid().ToString()
            };

            TableOperation addOperation = TableOperation.InsertOrMerge(newEmployee);
            TableResult addResult = await cloudTable.ExecuteAsync(addOperation);
            EmployeeEntity addResponse = addResult.Result as EmployeeEntity;

            if (addResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"addResponse ==> { addResponse.PartitionKey } - {addResponse.RowKey} - {addResponse.FullName}");
            }
        }

        public static async Task UpdateDataOperations(CloudTable cloudTable)
        {
            EmployeeEntity newEmployee = new EmployeeEntity("Consulting")
            {
                FullName = "Omar Negron Montero",
                Email = "omar.negronmontero@crowe.com",
                RowKey = Guid.NewGuid().ToString()
            };

            TableOperation addOperation = TableOperation.InsertOrMerge(newEmployee);
            TableResult addResult = await cloudTable.ExecuteAsync(addOperation);
            EmployeeEntity addResponse = addResult.Result as EmployeeEntity;

            if (addResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"addResponse ==> { addResponse.PartitionKey } - {addResponse.RowKey} - {addResponse.FullName}");
            }

            newEmployee.FullName = "Updated Full Name";

            TableOperation updateOperation = TableOperation.InsertOrMerge(newEmployee);
            TableResult updateResult = await cloudTable.ExecuteAsync(updateOperation);
            EmployeeEntity updateResponse = updateResult.Result as EmployeeEntity;

            if (updateResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"addResponse ==> { updateResponse.PartitionKey } - {updateResponse.RowKey} - {updateResponse.FullName}");
            }
        }

        public static async Task<EmployeeEntity> RetrieveSingleDataOperation(CloudTable cloudTable, string partitionKey,
            string rowKey)
        {
            TableOperation retrieveSingleOp = TableOperation.Retrieve<EmployeeEntity>(partitionKey
                , rowKey);

            TableResult result = await cloudTable.ExecuteAsync(retrieveSingleOp);

            EmployeeEntity employee = result.Result as EmployeeEntity;

            if(employee != null)
            {
                Console.WriteLine($"Employee Data ==> { employee.FullName }");
            }

            return employee;
        }

        public static async Task DeleteDataOperation(CloudTable cloudTable, EmployeeEntity entity)
        {
            if (entity != null)
            {
                TableOperation deleteOp = TableOperation.Delete(entity);

                TableResult result = await cloudTable.ExecuteAsync(deleteOp);

                if (result.HttpStatusCode == (int)HttpStatusCode.NoContent)
                {
                    Console.WriteLine("Deleted successfully");
                }
            }
        }
    }
}
