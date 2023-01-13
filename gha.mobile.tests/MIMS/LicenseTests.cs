using gha.mobile.mims.repositories;
using Xunit;

namespace gha.mobile.tests.MIMS
{
    public class LicenseTests : BaseTest
    {
        //[Fact]
        public void EPICOR_ISAMMLicensed_CallWorks()
        {
            // Arrange
            var licenseEpicor = new LicensingEpicor(GetEpicorConnection(EpicorEnvironment.env102600));

            // Act
            var response = licenseEpicor.IsAMMLicensed();

            // Assert
            Assert.True(response.Success);
        }

        //[Theory]
        [InlineData(EpicorEnvironment.env102600, true)]
        [InlineData(EpicorEnvironment.env28957test, false)] //! Could change
        public void EPICOR_ISAMMLicensed_CallReturnsCorrectValue(EpicorEnvironment environment, bool expectedResult)
        {
            // Arrange
            var licenseEpicor = new LicensingEpicor(GetEpicorConnection(environment));

            // Act
            var response = licenseEpicor.IsAMMLicensed();

            // Assert
            Assert.Equal(expectedResult, response.ReturnObj);
        }
    }
}