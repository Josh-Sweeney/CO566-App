using System.Collections.Generic;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;
using Xunit;

namespace gha.mobile.tests.MIMS
{
	public class NCRTests : BaseTest
	{
		#region Test Data
		
		public static IEnumerable<object[]> TestData =>
			new List<object[]>
			{
				//? Environment dependent

				// 1. Correct parameters (No lot / serial)
				new object[] { "100", "00C1", 1, "CHI", "00-00-00", "BLEM", null, null, true },
				// 2. Correct parameters (With lot, no serial)
				new object[] { "100", "00S1", 1, "CHI", "00-00-00", "BLEM", "2457", null, true },
				// 3. Correct parameters (With lot and serial)
				new object[] { "100", "307-2-4-3-6-8-UF-LVD", 1, "CHI", "00-00-00", "BLEM", "3", new List<PartSerial>() { new PartSerial() { PartNum = "307-2-4-3-6-8-UF-LVD", SerialNum = "04282022AAAA00017"}}, true },
				// 4. Correct parameters (With serial, no lot)
				new object[] { "100", "0SC1", 1, "CHI", "01-01-03", "BLEM", null, new List<PartSerial>() { new PartSerial() { PartNum = "0SC1", SerialNum = "0035"}}, true },
				
				//? 5. EmpId doesn't exist - Epicor allows this
				//new object[] { "", "00C1", 1, "CHI", "00-00-00", "BLEM", null, null, false },

				//? 6. Part doesn't exist
				new object[] { "100", "", 1, "CHI", "00-00-00", "BLEM", null, null, false },
				// 7. Part isn't serial tracked - Epicor allows this
				//new object[] { "100", "00C1", 1, "CHI", "00-00-00", "BLEM", null, new List<PartSerial>() { new PartSerial() { PartNum = "00C1", SerialNum = "1"}}, false },
				// 8. Part isn't lot tracked - Epicor allows this
				//new object[] { "100", "00C1", 1, "CHI", "00-00-00", "BLEM", "1", null, false },
				
				// 9. Quantity is negative - Epicor allows this
				//new object[] { "100", "00C1", -1, "CHI", "00-00-00", "BLEM", null, null, false },
				// 10. Quantity is not equal to serial count
				new object[] { "100", "0SC1", 2, "CHI", "01-01-03", "BLEM", null, new List<PartSerial>() { new PartSerial() { PartNum = "0SC1", SerialNum = "0035"}}, true },
				
				//? 11. WarehouseCode doesn't exist
				new object[] { "100", "00C1", 1, "", "00-00-00", "BLEM", null, null, false },

				//? 12. BinNum doesn't exist
				new object[] { "100", "00C1", 1, "CHI", "", "BLEM", null, null, false },

				//? 13. ReasonCode doesn't exist
				new object[] { "100", "00C1", 1, "CHI", "00-00-00", "", null, null, false },

				//? 14. LotNum doesn't exist - Epicor allows this
				//new object[] { "100", "00C1", 1, "CHI", "00-00-00", "BLEM", "qwertyuiop", null, false },
			
				//? 16. Serials don't exist
				new object[] { "100", "0SC1", 1, "CHI", "00-00-00", "BLEM", null, new List<PartSerial>() { new PartSerial() { PartNum = "0SC1", SerialNum = "" }}, false },
				// 17. Serials count is not equal to the required qty ???
				new object[] { "100", "0SC1", 2, "CHI", "00-00-00", "BLEM", null, new List<PartSerial>() { new PartSerial() { PartNum = "0SC1", SerialNum = "" }}, false },
				// 18. Serials lot num doesn't match LotNum
				new object[] { "100", "307-2-4-3-6-8-UF-LVD", 1, "CHI", "00-00-00", "BLEM", "1", new List<PartSerial>() { new PartSerial() { PartNum = "307-2-4-3-6-8-UF-LVD", SerialNum = "04282022AAAA00017"}}, true },
			};

		#endregion

		//[Theory]
		[MemberData(nameof(TestData))]
		public void Create_Inventory_NCR(string empID, string partNum, double quantity, string warehouseCode, string binNum, string reasonCode, string lotNum, List<PartSerial> serials, bool expected)
		{
			var ncrEpicor = new NCREpicor(GetEpicorConnection(EpicorEnvironment.env102600));

			var response = ncrEpicor.CreateNCR(empID, partNum, quantity, warehouseCode, binNum, reasonCode, lotNum, serials);

			Assert.Equal(expected, response.Success);

			DeleteNCR(response.ReturnObj);
		}

		private void DeleteNCR(int tranId)
		{
			var ncrEpicor = new NCREpicor(GetEpicorConnection(EpicorEnvironment.env102600));

			var response = ncrEpicor.DeleteNCR(tranId);
		}
	}
}