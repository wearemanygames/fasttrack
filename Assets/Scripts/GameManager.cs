using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //public BoardManager board;
    public Text goalSumText;
    public Text currentSumText;
    public Button finishButton;
    public GameObject board;
    public GameObject hand;

    private int currentSum = 0;
 
    // Start is called before the first frame update
    void Start()
    {        
        goalSumText.text = new System.Random().Next(13, 60).ToString();
    }
    
    public void addToCurrentSum(int value) {
        currentSumText.text = (currentSum += value).ToString();
    }

    public void subtractFromCurrentSum(int value) {
        currentSumText.text = (currentSum -= value).ToString();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
