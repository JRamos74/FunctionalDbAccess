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

    public class Sample1
    {
        public void SimpleTest()
        {
            ConnectionString connectionString = "YOUR CONNECTION STRING";

            List<SampleModel> list = Connect(connectionString, cn =>
            {
                List<SampleModel> results = new List<SampleModel>();
                cn.ExecuteReader("dbo.SelectSomething", System.Data.CommandType.StoredProcedure, (reader, set) =>
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

            foreach (var model in list)
                Console.WriteLine($"Name: {model.ModelName}, date added: {model.DateAdded}, some numer: {model.SomeNumber}");

        }
    }

   
}
