using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
    public GameObject boardTilePrefab;

    public int boardSize = 10;
    public GameObject[,] tableMatrix;
    private float step = 40f;
    
    public GameObject currentHead;

    // Start is called before the first frame update
    void Start() {
        tableMatrix = new GameObject[boardSize, boardSize];

        float yPosition = 454;
        for (int row = 0; row < boardSize; row++) {
            float xPosition = 200;
            for (int column = 0; column < boardSize; column++) {
                var cell = Instantiate(boardTilePrefab, new Vector2(xPosition, yPosition), Quaternion.identity);
                cell.transform.SetParent(transform);
                var dropZone = cell.GetComponent<DropZone>();
                dropZone.matrixPosition = new DropZone.Pair(row, column);
                tableMatrix[row, column] = cell;
                xPosition += step;
            }

            yPosition -= step;
        }

        //transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }
    
    public void highLightValidOptions() {
        var dropZone = currentHead.GetComponent<DropZone>();
        
        //Fix: Calculate adjacent points OR always check if the gameObject has childCount > 0, if it does, do nothing
        tableMatrix[dropZone.matrixPosition.row + 1, dropZone.matrixPosition.column].GetComponent<Image>().color = Color.green;
        tableMatrix[dropZone.matrixPosition.row - 1, dropZone.matrixPosition.column].GetComponent<Image>().color = Color.green;
        tableMatrix[dropZone.matrixPosition.row, dropZone.matrixPosition.column + 1].GetComponent<Image>().color = Color.green;
        tableMatrix[dropZone.matrixPosition.row, dropZone.matrixPosition.column - 1].GetComponent<Image>().color = Color.green;
    }

    public void overrideCurrentHead(GameObject head) {
        currentHead = head;
    }
    
    // Update is called once per frame
    void Update() {
    }
}