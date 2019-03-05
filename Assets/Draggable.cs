using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Vector3 originPosition = Vector3.zero;
    private Transform originalParent = null;
    public bool isOnBoard = false;
    
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        originalParent = transform.parent;
        originPosition = transform.position;
        transform.parent.SetAsLastSibling();
        //transform.SetParent(transform.parent.pa);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");

        if (!isOnBoard) {
            transform.SetParent(originalParent);
        }

        transform.position = originPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}