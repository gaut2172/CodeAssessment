using challenge.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;
        private const String COMPENSATION_SEED_DATA_FILE = "resources/CompensationSeedData.json";

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if(!_compensationContext.Compensations.Any())
            {
                // TODO: make sure Compensation.Employee matches an employeeId in Employee table
                List<Compensation> compensations = LoadCompensations();
                _compensationContext.Compensations.AddRange(compensations);

                await _compensationContext.SaveChangesAsync();
            }
        }

        /*
         * Function to parse the JSON seed data into a list of Compensation objects 
         */
        private List<Compensation> LoadCompensations()
        {
            {
                // Use Linq method to parse JSON seed data into a JObject
                JObject jObject = JObject.Parse(File.ReadAllText(COMPENSATION_SEED_DATA_FILE));

                List<Compensation> compensations = new List<Compensation>();

                // for each Compensation in the JObject
                foreach (var currCompensation in jObject["compensations"])
                {
                    string employeeId = (string)currCompensation["employeeId"];
                    decimal salary = (decimal)(JValue)currCompensation["salary"];
                    DateTime dateObject = Convert.ToDateTime((string)currCompensation["effectiveDate"]);

                    // create new C# Compensation instance
                    Compensation newCompensation = new Compensation(
                        employeeId,
                        salary,
                        dateObject);

                    compensations.Add(newCompensation);
                }

                return compensations;
            }
        }
    }
}
