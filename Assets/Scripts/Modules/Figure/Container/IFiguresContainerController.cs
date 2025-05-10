using System.Collections.Generic;

namespace Modules.Figure.Container
{
    public interface IFiguresContainerController
    {
        List<FigureView> ActiveFigures { get; }
    }
}