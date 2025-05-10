using System.Threading;
using Cysharp.Threading.Tasks;
using Modules.Figure.Model;

namespace Modules.Figure.Bar
{
    public interface IFiguresBarController
    {
        bool IsFull();
        UniTask MoveFigureToBar(FigureData figureView, CancellationToken token);
        void ClearMatches();
        void ClearAll();
    }
}