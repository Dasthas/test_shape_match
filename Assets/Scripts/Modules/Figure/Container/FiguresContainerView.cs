using UnityEngine;

namespace Modules.Figure.Container
{
    public class FiguresContainerView : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointTransform;
        [SerializeField] private Transform _figuresBarPoint;
        [SerializeField] private Transform _content;

        public Vector3 SpawnPoint => _spawnPointTransform.position;

        public Transform Content => _content;

        public Transform FiguresBarPoint => _figuresBarPoint;
    }
}