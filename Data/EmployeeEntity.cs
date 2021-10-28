using System;
using Microsoft.Azure.Cosmos.Table;

namespace aspnetcore_azure_tables.Data
{
    public class EmployeeEntity : TableEntity
    {
        public EmployeeEntity()
        {

        }

        public EmployeeEntity(string department)
        {
            PartitionKey = department;

        }

        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
