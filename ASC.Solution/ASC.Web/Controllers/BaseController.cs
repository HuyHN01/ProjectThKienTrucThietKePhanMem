using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASC.Web.Controllers
{
    [Authorize]  // Đảm bảo rằng tất cả các controller kế thừa từ BaseController yêu cầu người dùng phải đăng nhập
    public class BaseController : Controller
    {
        // Các hành động chung có thể được thêm vào đây
    }
}
