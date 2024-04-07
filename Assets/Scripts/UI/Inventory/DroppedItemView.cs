using UnityEngine;

namespace UI.Inventory
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DroppedItemView : BaseItemView
    {
        [SerializeField] private SpriteRenderer _sprite;
        
        protected override void Redraw()
        {
            base.Redraw();

            if (ItemData == null) return;

            _sprite.sprite = ItemData.Sprite;
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