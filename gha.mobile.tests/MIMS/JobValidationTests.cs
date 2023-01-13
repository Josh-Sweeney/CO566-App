using gha.mobile.mims.repositories;
using Xunit;

namespace gha.mobile.tests.MIMS
{
    public class JobValidationTests : BaseTest
    {
        //[Theory]
        [InlineData("2117", 26)]
        [InlineData("2446", 100)]
        public void EPICOR_GetJob_Returns_All_Materials(string jobNum, int expectedMaterialCount)
        {
            // Arrange
            var jobValidation = new JobValidationEpicor(GetEpicorConnection(EpicorEnvironment.env102600));

            // Act
            var response = jobValidation.GetEntireJob(jobNum);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(response.ReturnObj.Materials.Count, expectedMaterialCount);
        }
    }
}