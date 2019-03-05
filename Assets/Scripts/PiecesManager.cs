using System;
using UnityEngine;
using UnityEngine.UI;

public class PiecesManager : MonoBehaviour
{
    public Image gamePiecePrefab;
    //public Image gamePiecePrefab;
    public String piecesType = "whiteDice";
    public int piecesPerStack = 5;
    
    public int palleteSize = 6;
    private Image[] palleteList;
    private float step = 50f;
    float xOffset = 100f;
    

    // Start is called before the first frame update
    void Start() {
        palleteList = new Image[palleteSize];
        var sprites = Resources.LoadAll<Sprite>(string.Format("{0}", piecesType));

        for (int row = 0; row <= piecesPerStack; row++) {
            float xPosition = transform.position.x + xOffset;
            for (int column = 0; column < palleteSize; column++) {
                var cell = Instantiate(gamePiecePrefab, new Vector2(xPosition, transform.position.y), Quaternion.identity);
                cell.transform.SetParent(transform);
                cell.sprite = sprites[column]; 
                palleteList[column] = cell;
                xPosition += step;
            }
        }
        transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
