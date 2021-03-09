using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Components
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        protected Vector2 position = Vector2.zero;


        public virtual void OnBeginDrag(PointerEventData pointerEventData)
        {
            this.position = pointerEventData.position;
        }

        public virtual void OnDrag(PointerEventData pointerEventData)
        {
            var d = pointerEventData.position - this.position;
            var pos = this.transform.position;

            this.transform.position = d + new Vector2(pos.x, pos.y);
            this.position = pointerEventData.position;
        }
    }
}
