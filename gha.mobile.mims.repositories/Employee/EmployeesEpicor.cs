using System;
using System.Collections.Generic;
using EpicorRestAPI;
using gha.mobile.common.entities;
using gha.mobile.mims.entities;
using Newtonsoft.Json;

namespace gha.mobile.mims.repositories
{
    public class EmployeesEpicor : Epicor, IEmployees
    {
        public EmployeesEpicor()
        {
        }

        public EmployeesEpicor(ERPConnection epicorConnection) : base(epicorConnection)
        {

        }

        public EpicorResponse<List<Employee>> GetEmployees()
        {
            try
            {
                var getData = new Dictionary<string, string>
                {
                    // Check the fields exist in epicor or the rest call will fail and nothing will update
                    // particularly important during upgrades/if new fields have been added
                    { "$filter", "EmpStatus eq 'A'" },
                    { "$orderby", "Name" },
                    { "$top", "1000" }
                };

                var response = EpicorRest.DynamicGet("Erp.BO.EmpBasicSvc", "EmpBasics()", getData, statusCode,
                    callContextHeader);

                if (response.ContainsKey("ErrorMessage"))
                    return new EpicorResponse<List<Employee>> { Success = false, ErrorMessage = response.ErrorMessage };

                    var returnObj = new List<Employee>();
        
                foreach (var value in response.value)
                {
                    var employee = new Employee
                    {
                        EmployeeID = value.EmpID,
                        KnownAs = value.FirstName,
                        Name = value.Name
                    };

                    returnObj.Add(employee);
                }

                return new EpicorResponse<List<Employee>> { Success = true, ReturnObj = returnObj };
            }
            catch (Exception ex)
            {
#if DEBUG
                return new EpicorResponse<List<Employee>> { Success = false, ErrorMessage = ex.Message };
#else
                _ = ex.Message;
                return EpicorResponse<List<Employee>>.BadResponse;
#endif
            }
        }
    }
}