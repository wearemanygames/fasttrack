using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class InputManager : MonoBehaviour {
    struct Pair {
        public int row;
        public int cell;

        public Pair(int row, int cell) {
            this.row = row;
            this.cell = cell;
        }
    }

    private bool _draggingItem;
    private GameObject _draggedObject;
    private Vector2 _touchOffset;
    private int _goalSum;

    public int _currentSum;
    private Transform _grid;
    private Tile _currentTile;

    private Vector2 CurrentTouchPosition {
        get {
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    void Start() {
        _grid = GameObject.Find("Grid").transform;

        //Invertendo a ordem das linhas para a lógica de validação funcionar -> Sim, é gambiarra :D
        for (int i = 1; i < _grid.childCount; i++) {
            _grid.GetChild(0).SetSiblingIndex(_grid.childCount - i);
        }

        _goalSum = new Random().Next(13, 50);
        GameObject.Find("GoalSum").GetComponent<Text>().text = _goalSum.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (HasInput) {
            DragOrPickup();
        } else if (_draggingItem)
        {
            DropItem();
        }
        showCurrentSum();
    }

    private void DragOrPickup() {
        var inputPosition = CurrentTouchPosition;
        
        if (_draggingItem) {
            _draggedObject.transform.position = inputPosition + _touchOffset;
        } else {
            var layerMask = 1 << 0;
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f, layerMask);
            if (touches.Length > 0) {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.CompareTag("Tile")) {
                    _draggingItem = true;
                    _draggedObject = hit.transform.gameObject;
                    _touchOffset = (Vector2) hit.transform.position - inputPosition;
                    hit.transform.GetComponent<Tile>().PickUp();
                }
            }
        }

        if (dragged) {
            //atualiza sum
            var pieceValue =
                int.Parse(_draggedObject.GetComponent<Tile>().GetComponent<SpriteRenderer>().sprite.name.Split('_')[1]);
            _currentSum -= pieceValue;
        }
    }

    private bool HasInput {
        get { return Input.GetMouseButton(0); }
    }

    void DropItem() {
        _draggingItem = false;
        _draggedObject.transform.localScale = new Vector3(1f, 1f, 1f);
        _draggedObject.GetComponent<Tile>().Drop();
        
        //atualiza sum
        var pieceValue =
            int.Parse(_draggedObject.GetComponent<Tile>().GetComponent<SpriteRenderer>().sprite.name.Split('_')[1]);
        _currentSum += pieceValue;
    }

    private void showCurrentSum() {
        //Esse metodo precisa ser executado só quando a peça estiver substituindo um tile. senão da bosta quando o cara
        // clica rapidinho e ela faz drag e drop de volta pro lugar original dela
        GameObject.Find("CurrentSum").GetComponent<Text>().text = _currentSum.ToString();
    }

    public void OnClicked(Button button) {
        _currentSum = 0;
        if (EachRowHasAtLeastOnePiece()) {
            if (TrailPiecesAreConnected()) {
                if (SumIsValid()) {
                    button.GetComponent<Image>().color = Color.green;
                    GameObject.Find("Erro").GetComponent<Text>().enabled = false;
                    var errorComponent = GameObject.Find("Acerto").GetComponent<Text>();
                    errorComponent.text = "Parabéns!";
                    errorComponent.enabled = true;
                    GreenMarkPieces();
                } else {
                    button.GetComponent<Image>().color = Color.red;
                    GameObject.Find("Acerto").GetComponent<Text>().enabled = false;
                    var successComponent = GameObject.Find("Erro").GetComponent<Text>();
                    successComponent.text = "ERROU: " + _currentSum;
                    successComponent.enabled = true;
                }
            }
        }
    }

    private void GreenMarkPieces() {
        foreach (Transform currentRow in _grid) {
            foreach (Transform currentCell in currentRow) {
                if (currentCell.childCount > 0) {
                    currentCell.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
    }

    private bool SumIsValid() {
        return _goalSum == _currentSum;
    }

    private bool EachRowHasAtLeastOnePiece() {
        int rowsCount = _grid.childCount;
        int rowsPassed = 0;
        foreach (Transform row in _grid) {
            foreach (Transform cell in row) {
                if (cell.childCount > 0) {
                    rowsPassed++;
                    break;
                }
            }
        }

        return rowsCount == rowsPassed;
    }

    private bool TrailPiecesAreConnected() {
        var lastPieceFound = new Pair(0, 0);

        foreach (Transform currentRow in _grid) {
            foreach (Transform currentCell in currentRow) {
                if (currentCell.childCount > 0) {
                    var currentPieceFound = new Pair(int.Parse(currentRow.name), int.Parse(currentCell.name));

                    if (lastPieceFound.cell != 0) {
                        if (!IsCurrentConnectedToLast(currentPieceFound, lastPieceFound)) {
                            return false;
                        }
                    }

                    lastPieceFound = currentPieceFound;
                }
            }
        }
        return true;
    }

    private bool IsCurrentConnectedToLast(Pair currentPieceFound, Pair lastPieceFound) {
        if (currentPieceFound.row != lastPieceFound.row) {
            return lastPieceFound.cell == currentPieceFound.cell;
        }

        int left = lastPieceFound.cell - 1;
        int right = lastPieceFound.cell + 1;

        return currentPieceFound.cell >= left && currentPieceFound.cell <= right;
    }
}