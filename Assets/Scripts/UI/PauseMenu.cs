using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseMenuButtons;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject numberThree;
    [SerializeField] private GameObject numberTwo;
    [SerializeField] private GameObject numberOne;

    // Private references
    private GameObject levelAnim;
    private AudioSource music;

    [HideInInspector] public bool gameIsPaused = false;
    [HideInInspector] public bool inOptions = false;
    [HideInInspector] public bool onCountdown = false;

    private void Start()
    {
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        music.volume = 0.02f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onCountdown && GameManager.Instance.gameStarted)
        {
            if (gameIsPaused && !inOptions)
            {
                BackSound();
                Resume();
            }
            else if (!gameIsPaused && !inOptions)
                Pause();
            else if (gameIsPaused && inOptions)
            {
                BackSound();
                pauseMenuButtons.SetActive(true);
                optionsMenu.SetActive(false);
                Options();
            }
        }  
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        StartCoroutine(nameof(ResumeDelay));        
        gameIsPaused = false;
        music.volume = 0.045f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        AudioManager.instance.Play("Pause");
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("ClickUI");
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        if (!inOptions)
        {
            inOptions = true;
        }
        else
        {
            inOptions = false;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        AudioManager.instance.Play("BackUI");
        Application.Quit();
    }

    public IEnumerator ResumeDelay()
    {
        onCountdown = true;
        numberThree.SetActive(true);
        AudioManager.instance.Play("Tick");

        yield return new WaitForSecondsRealtime(0.5f);

        numberThree.SetActive(false);
        numberTwo.SetActive(true);
        AudioManager.instance.Play("Tick");

        yield return new WaitForSecondsRealtime(0.5f);

        numberTwo.SetActive(false);
        numberOne.SetActive(true);
        AudioManager.instance.Play("Tick");

        yield return new WaitForSecondsRealtime(0.5f);

        numberOne.SetActive(false);

        onCountdown = false;
        Time.timeScale = 1f;
    }

    public void ConfirmSound()
    {
        AudioManager.instance.Play("ClickUI");
    }

    public void BackSound()
    {
        AudioManager.instance.Play("BackUI");
    }
}
