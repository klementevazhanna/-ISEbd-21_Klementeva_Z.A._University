using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface IGraphicLogic
    {
        public GraphicViewModel GetGraphicByPrice(int userId);

        public GraphicViewModel GetGraphicByCount(int userId);
    }
}
