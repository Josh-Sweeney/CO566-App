using System;
using Xunit;
using gha.mobile.common.helpers;
using gha.mobile.common.resolver;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;

namespace gha.mobile.tests
{
    public class StocktakeTests
    {
        //[Fact]
        //public void UpdateStocktake()
        //{
        //    var stocktake = new StocktakeEpicor();
        //    var success = stocktake.Update("AJW", "2101154-001", "CC", "CC2", "Part 1", "", 5);

        //    Assert.Equal(true, success);
        //}

        //[Fact]
        public void TestFlowPart()
        {
            var stocktake = new StocktakeEpicor();
            var flow = stocktake.Flow("210115-001", string.Empty, "CC", "CC1");

            Assert.Equal(StocktakeFlow.Part, flow);
        }

        //[Fact]
        public void TestFlowBin()
        {
            var stocktake = new StocktakeEpicor();
            var flow = stocktake.Flow("210115-001", string.Empty, "CC", "CC2");

            Assert.Equal(StocktakeFlow.Bin, flow);
        }

        //[Fact]
        public void TestFlowWhse()
        {
            var stocktake = new StocktakeEpicor();
            var flow = stocktake.Flow("210115-001", string.Empty, "CHI", "00-00-00");

            Assert.Equal(StocktakeFlow.Whse, flow);
        }
    }
}
