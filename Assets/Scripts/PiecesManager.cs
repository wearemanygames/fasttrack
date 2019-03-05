using System;
using UnityEngine;
using UnityEngine.UI;

public class PiecesManager : MonoBehaviour {
    public Image gamePiecePrefab;

    //public Image gamePiecePrefab;
    public String piecesType = "whiteDice";
    public int rowsCount = 6;

    private int handSize = 6;
    private Image[] palleteList;
    private float step = 40f;
    float xOffset = 100f;


    // Start is called before the first frame update
    void Start() {
        palleteList = new Image[handSize];
        var sprites = Resources.LoadAll<Sprite>(string.Format("{0}", piecesType));

        float yPosition = 103f;
        float xPosition = 120f;
        for (int row = 0; row < rowsCount; row++) {
            for (int column = 0; column < handSize; column++) {
                var cell = Instantiate(gamePiecePrefab, new Vector2(xPosition, yPosition), Quaternion.identity);
                cell.transform.SetParent(transform);
                cell.sprite = sprites[column];
                //palleteList[column] = cell;
                xPosition += step;
            }

            if (row > 0) {
                if (row % 2 != 0) {
                    xPosition = 120f;
                    yPosition -= step;
                }
            }
        }

        //transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    // Update is called once per frame
    void Update() {
    }
}