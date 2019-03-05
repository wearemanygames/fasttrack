using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        var draggedItem = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggedItem != null) {
            draggedItem.parentToReturnTo = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("OnPointerExit");
    }
}
