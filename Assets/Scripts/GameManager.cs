using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public BoardManager board;
    public Text goalSum;
    public Text currentSum;
    public Button finishButton;
 
    // Start is called before the first frame update
    void Start()
    {        
        goalSum.text = new System.Random().Next(13, 60).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
