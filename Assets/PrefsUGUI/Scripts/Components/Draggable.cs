using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Components
{
    /// <summary>
    /// Allow uGUI to be dragged with the cursor.
    /// </summary>
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        /// <summary>Position of cursor.</summary>
        private Vector2 position = Vector2.zero;


        /// <summary>
        /// Start Move
        /// </summary>
        /// <param name="pointerEventData">Drag information</param>
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            this.position = pointerEventData.position;
        }

        /// <summary>
        /// Dragging
        /// </summary>
        /// <param name="pointerEventData">Drag information</param>
        public void OnDrag(PointerEventData pointerEventData)
        {
            var d = pointerEventData.position - this.position;
            var pos = this.transform.position;

            this.transform.position = d + new Vector2(pos.x, pos.y);
            this.position = pointerEventData.position;
        }
    }
}
