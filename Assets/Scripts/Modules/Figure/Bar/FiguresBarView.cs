using System.Collections.Generic;
using UnityEngine;

namespace Modules.Figure.Bar
{
    public class FiguresBarView: MonoBehaviour
    {
        [SerializeField] List<Transform> _points = new List<Transform>();

        public int PointsCount => _points.Count;
        
        public Vector3 GetPoint(int index)
        {
            return _points[index].position;
        }
    }
}