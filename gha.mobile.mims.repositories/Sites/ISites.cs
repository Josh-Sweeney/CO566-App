using System;
using System.Collections.Generic;
using gha.mobile.mims.entities;

namespace gha.mobile.mims.repositories
{
    public interface ISites
    {
        public List<Site> GetSites(Employee employee);
    }
}
