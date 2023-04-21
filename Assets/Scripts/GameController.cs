using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Camera cam;
    public GameObject popcorn;
    public Text timerText;
    public float timeLeft;
    public GameObject splashScreen;
    public GameObject startButton;
    public GameObject gameOver;
    Score score;

    private bool playing;
    private float maxWidth;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("box").GetComponent<Score>();
        splashScreen.SetActive(true);
        startButton.SetActive(true);
        gameOver.SetActive(false);
        
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float popcornWidth = popcorn.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - popcornWidth;
        UpdateText();
        playing = false;
    }

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
                gameOver.SetActive(true);
                splashScreen.SetActive(true);
                startButton.SetActive(true);
            }
            UpdateText();
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        while (timeLeft > 0)
        {
            int popSpawnQuantity = Random.Range(2, 7);
            while (popSpawnQuantity > 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y + Random.Range(1, 5), 0.0f);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(popcorn, spawnPosition, spawnRotation);
                popSpawnQuantity--;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
        }
    }

    void UpdateText()
    {
        timerText.text = string.Format("Time Left:\n{0}", Mathf.RoundToInt(timeLeft));
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        gameOver.SetActive(false);
        score.ResetScore();
        timeLeft = 30;
        StartCoroutine(Spawn());
        playing = true;
    }
}
