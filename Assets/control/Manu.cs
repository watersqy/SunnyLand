using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Manu : MonoBehaviour
{
    public GameObject Pause;
    public AudioMixer Audiomixer;
    public AudioMixer music;

   public void Palygame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void Ui()
    {
        GameObject.Find("Canvas/mainManu/ui").SetActive(true);
    }

    public void pauesmanue()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Return()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        SceneManager.LoadScene("manu");
        Time.timeScale = 1f;
    }

    public void Setvolume1(float volume)
    {
        Audiomixer.SetFloat("Mainvolume",volume);
    }

    public void Setvolume2(float volume)
    {
        music.SetFloat("music", volume);
    }
}
