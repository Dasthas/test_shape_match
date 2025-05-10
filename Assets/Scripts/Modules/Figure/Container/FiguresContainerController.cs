using System.Collections.Generic;

namespace Modules.Figure.Container
{
    public class FiguresContainerController : IFiguresContainerController
    {
        public List<FigureView> ActiveFigures { get; } = new List<FigureView>();
    }
}