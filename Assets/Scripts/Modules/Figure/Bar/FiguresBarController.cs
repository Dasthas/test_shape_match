using System.Collections.Generic;
using System.Threading;
using Core.Pool;
using Core.Settings;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Modules.Figure.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Figure.Bar
{
    public partial class FiguresBarController : IFiguresBarController, IInitializable
    {
        private const float ANIMATION_DURATION = 0.7f;

        [Inject] private FiguresBarView _view;
        [Inject] private PoolSimple<FigureView> _figuresPool;
        [Inject] private GameplaySettings _settings;
        [Inject] private FiguresModel _figuresModel;

        private List<BarItemData> _barItems = new List<BarItemData>(7);
        private Dictionary<int, List<BarItemData>> _matches = new Dictionary<int, List<BarItemData>>(7);

        private Stack<int> _freePoints = new Stack<int>(7);
        private List<int> _matchesToRemove = new List<int>(7);


        public void Initialize()
        {
            ResetStack();
        }

        public bool IsFull()
        {
            return _barItems.Count >= _settings.MinFiguresToLose;
        }

        public async UniTask MoveFigureToBar(FigureData figureData, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var freeIndex = _freePoints.Pop();
            var barData = new BarItemData(freeIndex, figureData);
            _barItems.Add(barData);

            var transform = figureData.FigureView.transform;

            if (_matches.TryGetValue(figureData.MatchIndex, out var matches))
            {
                matches.Add(barData);
                matches.Sort(new BarItemComparer());
            }
            else
            {
                _matches[figureData.MatchIndex] = new List<BarItemData>(7)
                {
                    barData
                };
            }

            Sequence sequence = DOTween.Sequence(figureData.FigureView);
            await sequence
                .Join(transform.DOMove(_view.GetPoint(freeIndex), ANIMATION_DURATION))
                .Join(transform.DORotate(Vector3.zero, ANIMATION_DURATION))
                .SetEase(Ease.OutBounce)
                .AsyncWaitForCompletion()
                .AsUniTask()
                .AttachExternalCancellation(token);
        }

        public void ClearMatches()
        {
            foreach (var (matchIndex, matchList) in _matches)
            {
                if (matchList.Count < _settings.MinFiguresToMatch)
                {
                    continue;
                }

                _matchesToRemove.Add(matchIndex);
                // delete matches
                foreach (var barItem in matchList)
                {
                    barItem.FigureData.FigureView.gameObject.SetActive(false);
                    var freeIndex = barItem.BarIndex;
                    _freePoints.Push(freeIndex);
                    _barItems.Remove(barItem);
                    _figuresModel.Remove(barItem.FigureData);
                }
            }

            foreach (var matchIndex in _matchesToRemove)
            {
                _matches.Remove(matchIndex);
            }

            _matchesToRemove.Clear();
        }

        public void ClearAll()
        {
            _barItems.Clear();
            _matches.Clear();
            ResetStack();
        }

        private void ResetStack()
        {
            _freePoints.Clear();
            for (int i = _view.PointsCount - 1; i >= 0; i--)
            {
                _freePoints.Push(i);
            }
        }
    }
}