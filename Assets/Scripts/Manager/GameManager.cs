using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 1f;
    public float gameSpeedIncrease = 0.1f;
    [HideInInspector] public bool gameStarted;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI scoreText;

    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject newRecordText;
    [SerializeField] private GameObject ui;
    [SerializeField] private float maxGameSpeed;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;

    private AudioSource music;
    private float transitionDuration = 1f;
    private float score;
    [HideInInspector] public int currentHp;    

    private void Start()
    {
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        gameStarted = false;
        Time.timeScale = 0f;
        gameSpeed = initialGameSpeed;
        currentHp = 3;
        HpUpdate(currentHp);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);

        levelLoader.SetActive(true);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }    

    private void Update()
    {
        if (gameSpeed < maxGameSpeed)        
            gameSpeed += gameSpeedIncrease * Time.deltaTime;      
        else
            gameSpeed = maxGameSpeed;

        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D6");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameStarted = true;
        tutorial.SetActive(false);
        ui.SetActive(true);
        music.volume = 0.045f;
    }

    public void Restart()
    {
        AudioManager.instance.Play("ClickUI");
        StartCoroutine(nameof(RestartGame));
    }

    public void HpUpdate(int hp)
    {        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }

        if (hp == 0)
            GameOver();
    }

    public void GameOver()
    {
        music.volume = 0.02f;
        ui.SetActive(false);
        gameOverScreen.SetActive(true);
        finalScoreText.text = $"FINAL SCORE: {Mathf.FloorToInt(score).ToString("D6")}";

        UpdateHighScore();
        Time.timeScale = 0f;
    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);

        if (score > highScore)
        {
            newRecordText.SetActive(true);
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        highScoreText.text = $"HIGHEST SCORE: {Mathf.FloorToInt(highScore).ToString("D6")}";
    }

    private IEnumerator RestartGame()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionDuration);
        
        SceneManager.LoadScene("GameScene");
    }
}
