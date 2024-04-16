using Logic.Inventory;
using UnityEngine;

namespace UI.Inventory
{
    public abstract class BaseItemView : MonoBehaviour
    {
        public ItemData ItemData { get; protected set; }
        
        public virtual void SetData(ItemData itemData)
        {
            ItemData = itemData;
            Redraw();
        }

        protected virtual void Redraw()
        {
            
        }
    }
}