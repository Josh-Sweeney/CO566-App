using System;
using System.Collections.Generic;
using gha.mobile.mims.entities;

namespace gha.mobile.mims.repositories
{
    public interface IBins
    {
        public List<Bin> Get(Warehouse warehouse);
        public Bin Validate(string warehouse, string bin);
        public List<Bin> GetStocktake(Warehouse warehouse, string countID);
    }
}
