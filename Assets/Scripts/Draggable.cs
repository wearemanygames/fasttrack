using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
    public Vector3 relativeOriginPosition = Vector3.zero;
    private Transform relativeOriginalParent = null;

    public int Value {
        get { return int.Parse(GetComponent<Image>().sprite.name); }
    }

    public bool isOnBoard = false;

    private Vector3 originalPosition = Vector3.zero;
    private Transform originalParent = null;

    private void Start() {
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        relativeOriginalParent = transform.parent;
        relativeOriginPosition = transform.position;

        transform.localScale = new Vector3(1.2f, 1.2f, 1f);

        //Best way i found to simulate a sorting order. ATENTION: May break matrix positions
        transform.parent.SetAsLastSibling();

        //transform.SetParent(transform.parent.pa);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Board").GetComponent<BoardManager>().highLightValidOptions();
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");

        transform.localScale = new Vector3(1f, 1f, 1f);

        //OnEndDrag runs after OnDrop, só, if there isn't a constraint, it will always run a rollback 
        if (!isOnBoard) {
            transform.SetParent(relativeOriginalParent);
        }

        transform.position = relativeOriginPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("Board").GetComponent<BoardManager>().lightOffValidOptions();
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("OnPointerClick");
        
        //This clearly doesn't belong to this class, please, refactor!
        if (isOnBoard) {
            transform.position = originalPosition;
            transform.parent = originalParent;
            GameObject.Find("GameManager").GetComponent<GameManager>().subtractFromCurrentSum(Value);
            isOnBoard = false;
        }
    }
}