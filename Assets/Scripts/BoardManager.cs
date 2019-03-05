using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameObject boardTilePrefab;

    public int boardSize = 10;
    public GameObject[,] tableMatrix;
    private float step = 40f;

    // Start is called before the first frame update
    void Start() {
        tableMatrix = new GameObject[boardSize, boardSize];

        float yPosition = 381;
        for (int row = 0; row < boardSize; row++) {
            float xPosition = 89.5f;
            for (int column = 0; column < boardSize; column++) {
                var cell = Instantiate(boardTilePrefab, new Vector2(xPosition, yPosition), Quaternion.identity);
                cell.transform.SetParent(transform);
                tableMatrix[row, column] = cell;
                xPosition += step;
            }

            yPosition -= step;
        }

        //transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    // Update is called once per frame
    void Update() {
    }
}