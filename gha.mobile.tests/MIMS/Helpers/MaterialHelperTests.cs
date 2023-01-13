using gha.mobile.mims.entities;
using gha.mobile.mims.helpers;
using gha.mobile.mims.repositories;
using System.Linq;
using Xunit;

namespace gha.mobile.tests.MIMS.Helpers
{
    public class MaterialHelperTests : BaseTest
    {
        //[Theory]
        [InlineData(EpicorEnvironment.env102600clean, "GHA-000152", 10)]
        public void EPICOR_GetMaterialsToIssue_ReturnsCorrectResult(EpicorEnvironment environment, string jobNum, int expectedMaterialCount)
        {
            // Arrange
            var response = new JobValidationEpicor(GetEpicorConnection(environment)).GetEntireJob(jobNum);
            Job job = response.ReturnObj;

            // Act
            var materialsToIssue = MaterialHelper.GetMaterialsToIssue(job.Materials);

            // Assert
            Assert.Equal(expectedMaterialCount, materialsToIssue.Count);
        }

        //[Theory]
        [InlineData(EpicorEnvironment.env102600clean, "GHA-000152", 10)] //! Run in clean102600
        public void EPICOR_SetFirstOperationMaterials_ReturnsCorrectResult(EpicorEnvironment environment, string jobNum, int expectedMaterialCount)
        {
            // Arrange
            var response = new JobValidationEpicor(GetEpicorConnection(environment)).GetEntireJob(jobNum);
            Job job = response.ReturnObj;

            // Act
            var materials = job.Materials;
            var afterMaterials = MaterialHelper.SetFirstOperationMaterials(job);

            // Assert
            Assert.Equal(expectedMaterialCount, afterMaterials.Count);
            Assert.Equal(0, afterMaterials.Count(r => r.RelatedOperation == 0));
        }
    }
}