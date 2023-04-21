using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public int zonkerValue;

    private int score;

    // Use this for initialization
    void Start()
    {
        score = 0;

    }

    void OnTriggerEnter2D()
    {
        score += zonkerValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = string.Format("Zonkers:\n{0}", score);
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }
}
