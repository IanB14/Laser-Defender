using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
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
        win(score);
    }

    public static void Reset()
    {
        score = 0;
    }

    public void win(int points)
    {
        int winPoints = 1000;

        if(points >= winPoints)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }
}
