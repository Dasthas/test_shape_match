using System;

namespace Modules.Figure.Model
{
    public readonly struct FigureData: IEquatable<FigureData>
    {
        public readonly FigureView FigureView;
        public readonly int MatchIndex;

        public FigureData(FigureView figureView, int matchIndex)
        {
            FigureView = figureView;
            MatchIndex = matchIndex;
        }

        public bool Equals(FigureData other)
        {
            return Equals(FigureView, other.FigureView) && MatchIndex == other.MatchIndex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FigureView, MatchIndex);
        }
    }
}