using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<ObjectPooling> poolings;

    //Delegate calling methods subscribed when a point is added
    public delegate void OnAddPoint(int value);
    public event OnAddPoint onAddPoint;

    //Delegate calling methods subscribed when the game time is changed
    public delegate void OnTimeChange(int value);
    public event OnTimeChange onTimeChange;

    //Delegate calling methods subscribed when is a game over
    public delegate void OnGameOver();
    public event OnGameOver onGameOver;

    [SerializeField] private UIScript uiScript;
    [SerializeField] private int totalPoints;
    [SerializeField] private float gameTime;
    [SerializeField] private bool gameOverOnce;

    private PlayerHp player;
    private void Awake()
    {
        instance = this;
        gameTime = GameManager.Instance.InformationSaved.gameTime;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        AddPoolingsOnList();
    }
    private void Start()
    {
        onGameOver += GameOverControl;
        player.onPlayerDeath += GameOverControl;
    }
    private void GameOverControl()
    {
        gameOverOnce = true;
    }

    private void AddPoolingsOnList()
    {
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<ObjectPooling>())
            {
                poolings.Add(child.GetComponent<ObjectPooling>());
            }
        }
    }

    void Update()
    {
        gameTime -= Time.deltaTime;
        onTimeChange?.Invoke((int)gameTime);
        if (!gameOverOnce)
        {
            if (gameTime <= 0)
            {
                onGameOver?.Invoke();
            }
        }
    }

    public void AddPoints(int value)
    {
        totalPoints += value;
        onAddPoint?.Invoke(totalPoints);
    }
}
