using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 1f;
    public float gameSpeedIncrease = 0.1f;
    public bool gameStarted;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI scoreText;

    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject ui;

    private float score;
    public int currentHp;

    public float cd = 30f;

    private void Start()
    {
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
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }    

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        scoreText.text = Mathf.FloorToInt(score).ToString("D6");
        cd -= 1f * Time.deltaTime;
        
        Debug.Log(Mathf.CeilToInt(cd));
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameStarted = true;
        tutorial.SetActive(false);
        ui.SetActive(true);
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
        Debug.Log("Game over");
        Time.timeScale = 0f;
    }
}
