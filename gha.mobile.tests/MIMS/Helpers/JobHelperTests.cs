using gha.mobile.mims.entities;
using gha.mobile.mims.helpers;
using gha.mobile.mims.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace gha.mobile.tests.MIMS.Helpers
{
    public class JobHelperTests : BaseTest
    {
        //[Theory]
        [InlineData(EpicorEnvironment.env102600, "2446", new int[] { 10, 20 })]
        public void EPICOR_ValidOperationsReturnsCorrectResult(EpicorEnvironment environment, string jobNum, int[] expectedOperationSeqs)
        {
            // Arrange
            var response = new JobValidationEpicor(GetEpicorConnection(environment)).GetEntireJob(jobNum);

            Job job = response.ReturnObj;
            Assembly assembly = JobHelper.GetMainAssembly(job);

            // Act
            var validOperations = JobHelper.GetValidAssemblyOperations(job, assembly);

            // Assert
            Assert.Equal(expectedOperationSeqs, validOperations.Select(r => r.OperationSeq).ToArray());
        }
    }
}
