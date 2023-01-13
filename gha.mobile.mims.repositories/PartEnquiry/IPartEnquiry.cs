using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using gha.mobile.mims.entities;

namespace gha.mobile.mims.repositories
{
    public interface IPartEnquiry
    {
        public List<PartBin> Get(Part part, ObservableCollection<PartBin> PartBins);
    }
}
