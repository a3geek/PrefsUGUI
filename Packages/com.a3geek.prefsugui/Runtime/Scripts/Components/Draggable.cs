using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Components
{
    [DisallowMultipleComponent]
    public sealed class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        private Vector2 position = Vector2.zero;


        void IBeginDragHandler.OnBeginDrag(PointerEventData pointerEventData)
        {
            this.position = pointerEventData.position;
        }

        void IDragHandler.OnDrag(PointerEventData pointerEventData)
        {
            var d = pointerEventData.position - this.position;
            var pos = this.transform.position;

            var v = d + new Vector2(pos.x, pos.y);
            this.transform.position = new Vector3(v.x, v.y, 0f);
            this.position = pointerEventData.position;
        }
    }
}
