using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Utilities
{
    /// <summary>
    /// uGUIをカーソルでドラッグ出来るようにする
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        /// <summary>カーソルの座標</summary>
        private Vector2 position = Vector2.zero;


        /// <summary>
        /// ドラッグ開始
        /// </summary>
        /// <param name="pointerEventData">ドラッグ情報</param>
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            this.position = pointerEventData.position;
        }

        /// <summary>
        /// ドラッグ中
        /// </summary>
        /// <param name="pointerEventData">ドラッグ情報</param>
        public void OnDrag(PointerEventData pointerEventData)
        {
            var d = pointerEventData.position - this.position;
            var pos = transform.position;

            transform.position = d + new Vector2(pos.x, pos.y);
            this.position = pointerEventData.position;
        }
    }
}
