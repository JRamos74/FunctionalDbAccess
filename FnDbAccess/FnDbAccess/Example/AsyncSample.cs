using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FnDbAccess.Example
{
    using FnTypes;
    using static FnConnection;
    using FnDbAccess.Extensions;

    public class AsyncSample
    {
        public void AsyncTest()
        {
            ConnectionString connectionString = "YOUR CONNECTION STRING";
            Task<List<SampleModel>> list = ConnectAsync(connectionString, async cn =>
            {
                List<SampleModel> results = new List<SampleModel>();

                await cn.ExecuteReaderAsync(System.Data.CommandType.StoredProcedure, "dbo.SomeDataSP", (reader, set) =>
                {
                    results.Add(new SampleModel
                    {
                        ModelName = reader["modelName"].ToString(),
                        SomeNumber = double.Parse(reader["someNumber"].ToString()),
                        DateAdded = DateTime.Parse(reader["someDate"].ToString())
                    });
                });
                return results;
            });

            foreach (var model in list.Result)
                Console.WriteLine($"Name: {model.ModelName}, date added: {model.DateAdded}, some numer: {model.SomeNumber}");
        }
    }
}
