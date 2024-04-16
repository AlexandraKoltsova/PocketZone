using UnityEngine;

namespace UI.Inventory
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DroppedItemView : BaseItemView
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private CircleCollider2D _collider;

        protected override void Redraw()
        {
            base.Redraw();

            if (ItemData == null)
            {
                _collider.enabled = false;
                return;
            }

            _sprite.sprite = ItemData.Sprite;
            _collider.enabled = true;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }

        private void OnValidate()
        {
            if (_sprite == null)
            {
                _sprite = GetComponentInChildren<SpriteRenderer>(true);
            }
        }
    }
}