using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    private Transform parentToReturnTo = null;
    
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        parentToReturnTo = transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentToReturnTo);
    }
}
