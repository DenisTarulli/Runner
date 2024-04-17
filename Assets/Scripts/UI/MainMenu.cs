using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;

    private float transitionDuration = 1f;

    public void PlayGame()
    {
        StartCoroutine(nameof(LoadLevel));

        //ClickSound();
    }
    /*
    public void ClickSound()
    {
        AudioManager.instance.Play("ClickUI");
    }

    public void BackSound()
    {
        AudioManager.instance.Play("BackUI");
    }
    */
    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    private IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene("GameScene");
    }
}
