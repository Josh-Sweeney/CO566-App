using System;
using Xunit;
using gha.mobile.common.helpers;
using gha.mobile.common.resolver;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;
using System.Collections.ObjectModel;
using System.Linq;

namespace gha.mobile.tests
{
    public class MiscMaterialTests
    {
        //[Fact]
        public void Issue()
        {
            var _partBins = new ObservableCollection<PartBin>();
            _partBins.Add(new PartBin { Bin = new Bin { WarehouseCode = "FST", BinNum = "a-101"}, LotNum = "", UOM = "EA", SelectedQuantity = 2 });

            var miscmaterial = new MiscMaterialEpicor();
            var success = miscmaterial.Issue(new MiscMaterial {
                JobNum = "2394",
                AssemblySeq = 0,
                OperationSeq = 70,
                PartNum = "516X075B",
                Description = "Bolt 5/16\" X 3/4\"",
                PartBins = _partBins.ToList(),
                IUM = "EA"
            }, "AJW");

            Assert.Equal(true, success);
        }

      
    }
}
