using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Class responsible for all the hud aspects of the game
public class UIScript : MonoBehaviour
{
    [Header("Game HUD")]
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text timeText;
    
    [Header("Game Over")]
    [SerializeField] private TMP_Text gameOverPoints;
    [SerializeField] private GameObject gameOver;

    [Header("Audios")]
    [SerializeField] private AudioClip selectButton;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource source;

    private int totalpoints;
    private int points;

    private LoadingScreen loadingScreen;
    private PlayerHp player;
    void Start()
    {
        loadingScreen = GetComponent<LoadingScreen>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        source = Camera.main.GetComponent<AudioSource>();

        AddPoints(0);

        GameController.instance.onAddPoint += AddPoints;
        GameController.instance.onTimeChange += AttTime;
        GameController.instance.onGameOver += GameOver;
        player.onPlayerDeath += GameOver;
    }
    void AddPoints(int value)
    {
        points = totalpoints;
        totalpoints = value;
        StartCoroutine(PointsEnum(totalpoints));
    }
    //Points number animation when enemy is killed
    IEnumerator PointsEnum(int endValue)
    {
        float gamePoints = points;
        pointsText.text = ((int)gamePoints).ToString();

        while (gamePoints < totalpoints)
        {
            gamePoints += (float)endValue / 60;
            pointsText.text = ((int)gamePoints).ToString();
            yield return new WaitForSecondsRealtime(0.01f);
        }
        points = totalpoints;
        pointsText.text = ((int)points).ToString();
    }

    void AttTime(int value)
    {
        timeText.text = value.ToString();
    }
    private void GameOver()
    {
        StartCoroutine(PlayGameOverSound());
        StartCoroutine(GameOverPointsEnum(points));
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
    private IEnumerator PlayGameOverSound()
    {
        yield return new WaitForSecondsRealtime(1);
        source.PlayOneShot(gameOverSound);
    }
    //Points number animation on game over
    private IEnumerator GameOverPointsEnum(int endValue)
    {
        float gamePoints = 0;
        gameOverPoints.text = (int)gamePoints + " Points";
        yield return new WaitForSecondsRealtime(1.5f);

        while (gamePoints < endValue)
        {
            gamePoints += (float)endValue / 60;
            gameOverPoints.text = (int)gamePoints + " Points";
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameOverPoints.text = totalpoints + " Points".ToString();
    }
    
    #region Game Over Buttons
    public void GoToMenu()
    {
        source.PlayOneShot(selectButton);
        Time.timeScale = 1;
        loadingScreen.LoadLevel();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        source.PlayOneShot(selectButton);
        SceneManager.LoadScene(1);
    }
    #endregion
}
