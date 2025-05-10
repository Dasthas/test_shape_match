using Modules.Figure.Model;
using UnityEngine;

namespace Modules.Figure
{
    public class FigureView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _iconRenderer;
        [SerializeField] private SpriteRenderer _frameSpriteRenderer;
        [SerializeField] private SpriteRenderer _figureSpriteRenderer;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;

        private void OnDisable()
        {
            Reset();
        }

        public void InitializeView(FigureSettingData figureSettingData)
        {
            _iconRenderer.sprite = figureSettingData.IconSprite;

            _frameSpriteRenderer.sprite = figureSettingData.FigureSprite;

            _figureSpriteRenderer.sprite = figureSettingData.FigureSprite;
            _figureSpriteRenderer.color = figureSettingData.FigureColor;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(float angle)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void EnablePhysics(bool enable)
        {
            _collider2D.enabled = enable;
            _rigidbody2D.isKinematic = !enable;
            if (!enable)
            {
                Reset();
            }
        }

        private void Reset()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }
}