using UnityEngine;

namespace UI.Inventory
{
    public class ItemPlayerView : BaseItemView
    {
        [SerializeField] private SpriteRenderer _sprite;
        
        protected override void Redraw()
        {
            base.Redraw();

            _sprite.sprite = ItemData?.Sprite;
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