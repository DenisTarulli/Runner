using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;

    private float transitionDuration = 1f;

    private void Awake()
    {
        levelLoader.SetActive(true);
    }
    public void PlayGame()
    {
        StartCoroutine(nameof(LoadLevel));

        ClickSound();
    }
    
    public void ClickSound()
    {
        AudioManager.instance.Play("ClickUI");
    }

    public void BackSound()
    {
        AudioManager.instance.Play("BackUI");
    }
    
    public void QuitGame()
    {
        BackSound();
        Debug.Log("Quitting...");
        Application.Quit();
    }

    private IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionDuration);

        SceneManager.LoadScene("GameScene");
    }
}
