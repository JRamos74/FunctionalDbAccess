using System;
using System.Collections.Generic;

namespace FnDbAccess.Example
{
    using FnDbAccess.Extensions;
    using FnDbAccess.Utilities;
    using FnTypes;
    using System.Threading.Tasks;
    using static FnConnection;

    public class SampleWithMapper
    {
        public void Example()
        {
            ConnectionString connectionString = "YOUR CONNECTION STRING";

            List<SampleModel> list = Connect(connectionString, cn =>
            {
                List<SampleModel> results = new List<SampleModel>();
                cn.ExecuteReader("dbo.SelectSomething", System.Data.CommandType.StoredProcedure, (reader, set) =>
                {
                    results.Add(DataMapper<SampleModel>.Instance.MapDataRowToObject(reader));
                });
                return results;
            });

            foreach (var model in list)
                Console.WriteLine($"Name: {model.ModelName}, date added: {model.DateAdded}, some numer: {model.SomeNumber}");

        }

        public void ExampleAsync()
        {
            ConnectionString connectionString = "YOUR CONNECTION STRING";
            Task<List<SampleModel>> list = ConnectAsync(connectionString, async cn =>
            {
                List<SampleModel> results = new List<SampleModel>();

                await cn.ExecuteReaderAsync(System.Data.CommandType.StoredProcedure, "dbo.SomeDataSP", (reader, set) =>
                {
                    results.Add(DataMapper<SampleModel>.Instance.MapDataRowToObject(reader));
                });
                return results;
            });

            foreach (var model in list.Result)
                Console.WriteLine($"Name: {model.ModelName}, date added: {model.DateAdded}, some numer: {model.SomeNumber}");
        }
    }
    
}
