using System.Collections.Generic;

namespace Modules.Figure.Bar
{
    public partial class FiguresBarController
    {
        private struct BarItemComparer : IComparer<BarItemData>
        {
            public int Compare(BarItemData x, BarItemData y)
            {
                return x.BarIndex > y.BarIndex ? -1 : 1;
            }
        }
    }
}