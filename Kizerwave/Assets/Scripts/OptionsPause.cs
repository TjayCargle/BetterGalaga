using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsPause : MonoBehaviour
{
    public AudioSource music;
    public Slider musicSlider;
    public float lastVolume = 0.35f;

    public AudioSource sfx;
    public Slider sfxSlider;
    public float lastSFXVolume = 0.35f;
    private static string volumePref = "LAST_VOLUME";
    private static string sfxPref = "LAST_SFX";
    private void Start()
    {
        if (musicSlider != null)
        {
            if (PlayerPrefs.HasKey(volumePref))
            {
                lastVolume = PlayerPrefs.GetFloat(volumePref);
            }
            music.volume = lastVolume;
            musicSlider.value = music.volume;
        }

        if (sfxSlider != null)
        {
            if (PlayerPrefs.HasKey(sfxPref))
            {
                lastSFXVolume = PlayerPrefs.GetFloat(sfxPref);
            }
            sfx.volume = lastSFXVolume;
            sfxSlider.value = sfx.volume;
        }
        SyncOptions();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(volumePref, lastVolume);
        PlayerPrefs.SetFloat(sfxPref, lastSFXVolume);
        PlayerPrefs.Save();
    }

    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch(currentScene)
        {
            case "Level 1":
                {
                    SceneManager.LoadScene("TJayTestScene");
                }
                break;

            case "Level 2":
                {
                    SceneManager.LoadScene("Level 3");
                }
                break;

            case "Level 3":
                {
                     SceneManager.LoadScene("MainMenu");
                }
                break;

            case "TJayTestScene":
                {
                    SceneManager.LoadScene("Level 2");
                }
                break;
            default:
                {
                    SceneManager.LoadScene("MainMenu");

                }
                break;
        }
    }

    public void OpenPanel(GameObject somePanel)
    {
        if (somePanel != null)
        {
            somePanel.gameObject.SetActive(true);
        }
    }

    public void ClosePanel(GameObject somePanel)
    {
        if (somePanel != null)
        {
            somePanel.gameObject.SetActive(false);
        }
    }

    public void SyncOptions()
    {
        if (music != null)
        {
            if (musicSlider != null)
            {
                if (music.volume != musicSlider.value)
                {
                    music.volume = musicSlider.value;
                    lastVolume = music.volume;
                }
            }
        }

        if (sfx != null)
        {
            if (sfxSlider != null)
            {
                if (sfx.volume != sfxSlider.value)
                {
                    sfx.volume = sfxSlider.value;
                    lastSFXVolume = sfx.volume;
                    if (sfxSlider.gameObject.activeInHierarchy == true)
                        SFXLibrary.PlayDefaultMissile(true);
                }
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ZeroScore()
    {
        ScoreScript.playerScore = 0;
    }
}
