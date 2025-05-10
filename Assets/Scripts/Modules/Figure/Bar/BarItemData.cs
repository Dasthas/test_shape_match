using System;
using Modules.Figure.Model;

namespace Modules.Figure.Bar
{
    public readonly struct BarItemData:IEquatable<BarItemData>
    {
        public readonly FigureData FigureData;
        public readonly int BarIndex;

        public BarItemData(int barIndex, FigureData figureData)
        {
            BarIndex = barIndex;
            FigureData = figureData;
        }

        public bool Equals(BarItemData other)
        {
            return BarIndex == other.BarIndex;
        }

        public override int GetHashCode()
        {
            return BarIndex;
        }
    }
}