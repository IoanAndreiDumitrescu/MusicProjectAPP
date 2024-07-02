using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void Test_ErrorViewModel_Creation()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = "1234"
            };

            var requestId = errorViewModel.RequestId;
            var showRequestId = errorViewModel.ShowRequestId;

            Assert.AreEqual("1234", requestId);
            Assert.IsTrue(showRequestId);
        }

        [TestMethod]
        public void Test_ErrorViewModel_ShowRequestId_When_RequestId_Is_Null()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = null
            };

            var showRequestId = errorViewModel.ShowRequestId;

            Assert.IsFalse(showRequestId);
        }
    }
}