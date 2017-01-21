using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Controller
{
    public class EventCatcher  : MonoBehaviour, IPointerClickHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        ISelectHandler
    {
        public void OnPointerEnter(PointerEventData evd)
        {
            Debug.Log("OnPointerEnter");
        }
        public void OnPointerExit(PointerEventData evd)
        {
            Debug.Log("OnPointerExit");
        }
        public void OnPointerClick(PointerEventData evd)
        {
            Debug.Log("OnPointerClick");
        }
        public void OnPointerDown(PointerEventData evd)
        {
            Debug.Log("OnPointerDown");
        }
        public void OnPointerUp(PointerEventData evd)
        {
            Debug.Log("OnPointerUp");
        }
        public void OnSelect(BaseEventData evd)
        {
            Debug.Log("OnSelect");
        }
    }
}