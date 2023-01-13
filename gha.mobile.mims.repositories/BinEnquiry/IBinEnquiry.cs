using System;
using System.Collections.Generic;
using gha.mobile.mims.entities;

namespace gha.mobile.mims.repositories
{
    public interface IBinEnquiry
    {
        public List<BinPart> Get(string warehousecode, string binnum);
    }
}
