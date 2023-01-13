using System.Collections.Generic;
using System.Linq;
using gha.mobile.mims.entities;
using gha.mobile.mims.entities.Requests;
using gha.mobile.mims.repositories;
using Xunit;

namespace gha.mobile.tests.MIMS.Repositories
{
    public class OrderPickingEpicorTests : BaseTest
    {
        public class PackItemParameters
        {
            public string EmpID { get; set; }

            // Will just get the Order Release by the num, line and rel instead of typing every property out
            public int SalesOrder { get; set; }
            public int SalesOrderLine { get; set; }
            public int SalesOrderRel { get; set; }

            public double Quantity { get; set; }
            public bool ShippedComplete { get; set; }
            public Bin FromBin { get; set; }
            public string LotNum { get; set; }
            public IEnumerable<PartSerial> Serials { get; set; }
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { EpicorEnvironment.env102600, 524, new List<PackItemParameters>() { new PackItemParameters() { EmpID = "300", SalesOrder = 5386, SalesOrderLine = 1, SalesOrderRel = 1, Quantity = 1, FromBin = new Bin() { BinNum = "00-00-00", WarehouseCode="CHI" }, LotNum = "1", Serials = new List<PartSerial>() { new PartSerial() { PartNum = "307-2-4-3-6-8-UF-LVD", SerialNum = "1" } }, ShippedComplete = false } }, true }
            };

        //[Theory]
        [MemberData(nameof(Data))]
        public void EPICOR_PackItems_Works(EpicorEnvironment environment, int packNum, List<PackItemParameters> items, bool expectedResult)
        {
            // Arrange
            var customerShipment = new CustomerShipment() {  PackNum = packNum };
            var OrderPickingEpicor = new OrderPickingEpicor(GetEpicorConnection(environment));

            List<PackItemRequest> request = items
                .Select(r => new PackItemRequest()
                {
                    Item = OrderPickingEpicor.GetOrderReleases(r.EmpID).ReturnObj.FirstOrDefault(g =>
                        g.OrderNum == r.SalesOrder &&
                        g.OrderLine == r.SalesOrderLine &&
                        g.OrderRel == r.SalesOrderRel),
                    Quantity = r.Quantity,
                    ShippedComplete = r.ShippedComplete,
                    FromBin = r.FromBin,
                    LotNum = r.LotNum,
                    Serials = r.Serials,
                }).ToList();

            // Act
            var response = new OrderPickingEpicor(GetEpicorConnection(environment)).PackItems(customerShipment, request);

            // Assert
            Assert.Equal(expectedResult, response.Success);
        }
    }
}