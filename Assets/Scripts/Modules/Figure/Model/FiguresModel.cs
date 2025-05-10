using System.Collections.Generic;

namespace Modules.Figure.Model
{
    public class FiguresModel
    {
        private List<FigureData> _figureDatas = new List<FigureData>();

        public FigureData GetDataByView(FigureView figureView)
        {
            return _figureDatas.Find((item) => item.FigureView == figureView);
        }

        public void Add(FigureData figureData)
        {
            _figureDatas.Add(figureData);
        }

        public void Remove(FigureData figureData)
        {
            _figureDatas.Remove(figureData);
        }

        public bool Contains(FigureData figureData)
        {
            return _figureDatas.Contains(figureData);
        }

        public void Clear()
        {
            _figureDatas.Clear();
        }

        public IEnumerable<FigureData> GetEnumerable()
        {
            return _figureDatas;
        }
    }
}