using Microsoft.AspNetCore.Mvc;
using UniversityContracts.ViewModels;

namespace UniversityClientApp.Controllers
{
    public class GraphicController : Controller
    {
        public IActionResult Index()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            var model = APIClient.GetRequest<GraphicViewModel[]>($"api/graphic/GetGraphic?userId={Program.User.Id}");
            return View(model);
        }
    }
}
