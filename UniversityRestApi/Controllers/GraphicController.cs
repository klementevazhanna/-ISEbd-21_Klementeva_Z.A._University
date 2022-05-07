using Microsoft.AspNetCore.Mvc;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GraphicController : Controller
    {
        private readonly IGraphicLogic _logic;

        public GraphicController(IGraphicLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public GraphicViewModel[] GetGraphic(int userId)
        {
            return new GraphicViewModel[]
            {
                _logic.GetGraphicByCount(userId),
                _logic.GetGraphicByPrice(userId)
            };
        }
    }
}
