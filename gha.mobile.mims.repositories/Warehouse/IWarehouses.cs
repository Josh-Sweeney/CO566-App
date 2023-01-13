using System;
using System.Collections.Generic;
using gha.mobile.common.repositories;
using gha.mobile.mims.entities;

namespace gha.mobile.mims.repositories
{
    public interface IWarehouses
    {
        public List<Warehouse> Get();
    }
}
