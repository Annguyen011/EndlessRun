using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    Home,
    Gameplay,
    Gameover,
    Pause
}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject spawn;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtHighScore;
    [SerializeField] private float score;
    [SerializeField] private float highScore;
    private GameState state;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        home.SetActive(false);
        pause.SetActive(false);
        gamePlay.SetActive(false);
        gameover.SetActive(false);
        SetState(GameState.Home);
    }
    public void SetState(GameState state)
    {
        this.state = state;
        home.SetActive(state == GameState.Home);
        pause.SetActive(state == GameState.Pause);
        gamePlay.SetActive(state == GameState.Gameplay);
        gameover.SetActive(state == GameState.Gameover);
        if (state == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else if (state == GameState.Home || state == GameState.Gameover)
        {
            spawn.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            spawn.SetActive(true);
        }

    }
    private void FixedUpdate()
    {
        if (state == GameState.Gameplay)
        {
            score += Time.fixedDeltaTime * 0.5f;
            txtScore.SetText("Score: " + Mathf.Round(score));
        }


        highScore = PlayerPrefs.GetFloat("HighScore");
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        txtHighScore.SetText(highScore.ToString());
    }
    public void Btn_Play()
    {
        AudioManager.Instance.OnPressBtn();
        SetState(GameState.Gameplay);
    }
    public void Btn_Home()
    {
        AudioManager.Instance.OnPressBtn();
        SetState(GameState.Home);
        score = 0;
    }
    public void Btn_Pause()
    {
        AudioManager.Instance.OnPressBtn();
        SetState(GameState.Pause);
    }
}
