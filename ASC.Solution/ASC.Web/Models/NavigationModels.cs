namespace ASC.Web.Models
{
    // Lớp lưu trữ các mục menu
    public class NavigationMenu
    {
        // Danh sách các mục menu
        public List<NavigationMenuItem> MenuItems { get; set; }
    }

    // Lớp lưu trữ thông tin của từng mục menu
    public class NavigationMenuItem
    {
        // Tên hiển thị của mục menu
        public string DisplayName { get; set; }

        // Biểu tượng Material Icon
        public string MaterialIcon { get; set; }

        // Đường dẫn tới trang của mục menu
        public string Link { get; set; }

        // Kiểm tra xem menu có phải là menu con hay không
        public bool IsNested { get; set; }

        // Thứ tự hiển thị của mục menu
        public int Sequence { get; set; }

        // Các vai trò người dùng có quyền truy cập vào mục menu này
        public List<string> UserRoles { get; set; }

        // Danh sách các mục con nếu menu này có menu con
        public List<NavigationMenuItem> NestedItems { get; set; }
    }
}
