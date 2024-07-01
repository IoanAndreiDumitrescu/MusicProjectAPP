using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void Test_ErrorViewModel_Creation()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel
            {
                RequestId = "1234"
            };

            // Act
            var requestId = errorViewModel.RequestId;
            var showRequestId = errorViewModel.ShowRequestId;

            // Assert
            Assert.AreEqual("1234", requestId);
            Assert.IsTrue(showRequestId);
        }

        [TestMethod]
        public void Test_ErrorViewModel_ShowRequestId_When_RequestId_Is_Null()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel
            {
                RequestId = null
            };

            // Act
            var showRequestId = errorViewModel.ShowRequestId;

            // Assert
            Assert.IsFalse(showRequestId);
        }
    }
}