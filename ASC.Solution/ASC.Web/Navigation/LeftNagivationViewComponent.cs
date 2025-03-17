using Microsoft.AspNetCore.Mvc;
using ASC.Web.Models;

namespace ASC.Web.Navigation
{
    [ViewComponent(Name = "ASC.Web.Navigation.LeftNavigation")]
    public class LeftNagivationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(NavigationMenu menu)
        {
            menu.MenuItems = menu.MenuItems.OrderBy(p => p.Sequence).ToList();
            return View(menu);
        }
    }
}
