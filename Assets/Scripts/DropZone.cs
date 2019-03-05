using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    
    private Draggable draggedItem;
    
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        
        draggedItem = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggedItem != null && transform.childCount == 0) {
            draggedItem.transform.SetParent(transform);
            draggedItem.relativeOriginPosition = transform.position;
            if (!draggedItem.isOnBoard) {
                GameObject.Find("MyManager").GetComponent<MyManager>().addToCurrentSum(draggedItem.Value);
            }
            draggedItem.isOnBoard = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("OnPointerEnter");
        gameObject.GetComponent<Image>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("OnPointerExit");
        gameObject.GetComponent<Image>().color = Color.white;
    }
}
