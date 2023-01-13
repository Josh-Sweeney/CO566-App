using gha.mobile.mims.entities;
using System.Collections.Generic;

namespace gha.mobile.mims.repositories
{
    public interface IEmployees
    {
        EpicorResponse<List<Employee>> GetEmployees();
    }
}
