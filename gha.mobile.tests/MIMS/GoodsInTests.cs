using System;
using System.Collections.Generic;
using System.Text;
using gha.mobile.common.entities;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;
using Xunit;

namespace gha.mobile.tests.MIMS
{
	public class GoodsInTests : BaseTest
	{
		//[Fact]
		public void CreatePOReceipt()
		{
			// Arrange
			var POReceiptEpicor = new POReceiptEpicor(GetEpicorConnection(EpicorEnvironment.env102600));

			int poNum = 4245;
			string entryPerson = "epicor";

			// Act
			var response = POReceiptEpicor.CreatePOReceipt(poNum, entryPerson);

			// Assert
			Assert.True(response.Success);
		}

		//[Fact]
		public void AddPOReceiptLine()
		{
			// Arrange
			var POReceiptEpicor = new POReceiptEpicor(GetEpicorConnection(EpicorEnvironment.env102600));

			var poReceipt = new POReceipt()
			{
				PackSlip = 4246,
				PurPoint = "",
				VendorNum = 1
			};

			var poLine = new POLine()
			{
				OrderLine = 1,
				OrderNum = 0,
				OrderQty = 10,
				POLineNum = 1,
				Part = new Part()
				{
					Description = "Pump / Diaphragm Master Config",
					LotTracked = true,
					PartNum = "307-2-4-3-6-8-UF-LVD",
					SerialTracked = true,
					UOM = ""
				}
			};

			var poRelease = new PORelease()
			{ };

			double quantity = 1;

			string warehouseCode = "CHI";
			string binNum = "00-00-00";
			string lotNum = "3";

			var serials = new List<PartSerial>()
            {
                new PartSerial()
                {
                    BinNum = null,
                    JobNum = null,
                    LotNum = "3",
                    MtlSeq = 0,
                    PartNum = "307-2-4-3-6-8-UF-LVD",
                    Selected = false,
                    SerialNum = "04282022AAAA00015",
                    WarehouseCode = null,
                }
            };

			// Act
			var response = POReceiptEpicor.AddLineToReceipt(poReceipt, poLine, poRelease, quantity, warehouseCode, binNum, lotNum, serials);
            var a = response.ErrorMessage;

			// Assert
			Assert.True(response.Success);
		}

		//[Fact]
		public void CreateRMAReceipt()
		{
			// Arrange
			var RMAOrderEpicor = new RMAOrderEpicor(GetEpicorConnection(EpicorEnvironment.env102600));

			var rmaOrder = new RMAOrder()
			{
				RMANum = 1006,
				CustNum = 9
			};

			var rmaLine = new RMAOrderLine()
			{
				RMALine = 1,
				OrderNum = 5385,
				OrderLine = 1,
				OrderRelNum = 1,
				ReturnQty = 10,
				Part = new Part()
				{
					Description = "Metal Plate",
					LotTracked = false,
					PartNum = "8400S-042",
					SerialTracked = true,
					UOM = ""
				}
			};

			double quantity = 1;

			string warehouseCode = "RCV";
			string binNum = "RCV-1";
			string lotNum = "";

			var serials = new List<PartSerial>()
			{
				new PartSerial()
				{
					BinNum = null,
					JobNum = null,
					LotNum = "3",
					MtlSeq = 0,
					PartNum = "307-2-4-3-6-8-UF-LVD",
					Selected = false,
					SerialNum = "04282022AAAA00015",
					WarehouseCode = null,
				}
			};

			// Act
			var response = RMAOrderEpicor.CreateRMAReceipt(rmaOrder, rmaLine, quantity, warehouseCode, binNum, lotNum, null);

			// Assert
			Assert.True(response.Success, response.ErrorMessage);
		}
	}
}
