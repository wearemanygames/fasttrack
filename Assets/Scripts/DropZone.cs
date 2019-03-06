using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    
    private Draggable draggedItem;
    
    public Pair matrixPosition;
    
    [System.Serializable]
    public struct Pair {
        public int row;
        public int column;

        public Pair(int row, int column) {
            this.row = row;
            this.column = column;
        }
    }
    
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        
        draggedItem = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggedItem != null && transform.childCount == 0) {
            draggedItem.transform.SetParent(transform);
            draggedItem.relativeOriginPosition = transform.position;
            if (!draggedItem.isOnBoard) {
                GameObject.Find("GameManager").GetComponent<GameManager>().addToCurrentSum(draggedItem.Value);
            }
            draggedItem.isOnBoard = true;
            GameObject.Find("Board").GetComponent<BoardManager>().overrideCurrentHead(gameObject);
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
