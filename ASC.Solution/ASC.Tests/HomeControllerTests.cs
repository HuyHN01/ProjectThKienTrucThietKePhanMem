using System;
using ASC.Tests.TestUtilities;
using ASC.Web.Configuration;
using ASC.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using ASC.Utilities;


namespace ASC.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptions<ApplicationSettings>> optionsMock;
        private readonly Mock<HttpContext> mockHttpContext;

        public HomeControllerTests()
        {
            // Create an instance of Mock IOptions
            optionsMock = new Mock<IOptions<ApplicationSettings>>();

            mockHttpContext = new Mock<HttpContext>();
            //Set FakeSession to HttpContext Session
            mockHttpContext.Setup(p => p.Session).Returns(new FakeSession());

            // Set IOptions<> Values property to return ApplicationSettings object
            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSettings
            {
                ApplicationTitle = "ASC"
            });
        }

        [Fact]
        public void HomeController_Index_View_Test() //Kiểm tra xem HomeController có trả về một ViewResult.
        {
            // Home controller instantiated with Mock IOptions<> object
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;  
            // Assert return ViewResult
            Assert.IsType(typeof(ViewResult), controller.Index());
        }

        [Fact]
        public void HomeController_Index_NoModel_Test() //Kiểm tra xem mô hình trả về có phải là null hay không.
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Assert Model for Null
            Assert.Null((controller.Index() as ViewResult).ViewData.Model);
        }

        [Fact]
        public void HomeController_Index_Validation_Test() //Kiểm tra số lượng lỗi trong ModelState của HomeController.
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Assert ModelState Error Count to 0
            Assert.Equal(0, (controller.Index() as ViewResult).ViewData.ModelState.ErrorCount);
        }

        [Fact]
        public void HomeController_Index_Session_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            controller.Index();

            // Session value with key "Test" should not be null.
            Assert.NotNull(controller.HttpContext.Session.GetSession<ApplicationSettings>("Test"));
        }

    }

}

/*
 Chú ý: thuộc tính  Fact được sử dụng để kiểm tra các điều kiện bất biến, và phương thức kiểm tra
 không có đối số. Mặt khác, thuộc tính Theory cho phép chuyển các dữ liệu khác nhau đến phương thức 
 kiểm tra dưới dạng đối số.
 */