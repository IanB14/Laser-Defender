using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public int score = 0;
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        Debug.Log("Enemy down - points awarded.");
        score += points;
        myText.text = score.ToString();
    }

    private void Reset()
    {
        score = 0;
        myText.text = score.ToString();
    }
}
