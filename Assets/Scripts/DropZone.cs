using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        var draggedItem = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggedItem != null && transform.childCount == 0) {
            draggedItem.transform.SetParent(transform);
            draggedItem.originPosition = transform.position;
            draggedItem.isOnBoard = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("OnPointerExit");
    }
}
