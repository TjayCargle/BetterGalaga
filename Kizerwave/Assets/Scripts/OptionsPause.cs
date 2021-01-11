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
    private static string volumePref = "LAST_VOLUME";
    private void Start()
    {
        if (musicSlider != null)
        {
            if(PlayerPrefs.HasKey(volumePref))
            {
                lastVolume = PlayerPrefs.GetFloat(volumePref);
            }
            music.volume = lastVolume;
            musicSlider.value = music.volume;
        }
            SyncOptions();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(volumePref, lastVolume);
        PlayerPrefs.Save();
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("TJayTestScene");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenPanel(GameObject somePanel)
    {
        if(somePanel != null)
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
        if(music != null)
        {
            if(musicSlider != null)
            {
                if(music.volume != musicSlider.value)
                {
                    music.volume = musicSlider.value;
                    lastVolume = music.volume;
                }
            }
        }
    }
}
