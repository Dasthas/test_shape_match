using UnityEngine;

namespace Modules.Figure.Model
{
    public readonly struct FigureSettingData
    {
        public readonly Sprite FigureSprite;
        public readonly Sprite IconSprite;
        public readonly Color FigureColor;

        public FigureSettingData(Sprite figureSprite, Sprite iconSprite, Color figureColor)
        {
            FigureSprite = figureSprite;
            IconSprite = iconSprite;
            FigureColor = figureColor;
        }
    }
}