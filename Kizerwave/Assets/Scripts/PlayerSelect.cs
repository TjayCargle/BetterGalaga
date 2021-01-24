using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSelect : StatDisplay
{
 


    private void Awake()
    {
        manager = StatManager.Instance;
    }
    public void ZeroOut()
    {
        if(nameText != null)
        {
            nameText.text = "";
        }
        if (healthShowcase != null)
        {
            Image[] healthImages = healthShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < healthImages.Length; i++)
            {
                healthImages[i].color = Color.white;
            }
        }

        if (fireRateShowcase != null)
        {
            fireRateImages = fireRateShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < fireRateImages.Length; i++)
            {
              fireRateImages[i].color = Color.white;
                
            }
        }


        if (speedShowcase != null)
        {
            speedImages = speedShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < speedImages.Length; i++)
            {
                speedImages[i].color = Color.white;

            }
        }

        if(startButton != null)
        {
            startButton.SetActive(false);
        }
    }

    public void UpdateBars()
    {
        if(nameText != null)
        {
            nameText.text = manager.playerName;
        }
        if (healthShowcase != null)
        {
            healthImages = healthShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < healthImages.Length; i++)
            {
                if(i - 1 < manager.healthStat)
                {
                healthImages[i].color = Color.yellow;

                }
                else
                {
                healthImages[i].color = Color.white;

                }
            }
        }

        if (fireRateShowcase != null)
        {
            fireRateImages = fireRateShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < fireRateImages.Length; i++)
            {
                if (i - 1 < manager.fireRateStat)
                {
                    fireRateImages[i].color = Color.yellow;

                }
                else
                {
                    fireRateImages[i].color = Color.white;

                }
            }
        }


        if (speedShowcase != null)
        {
            speedImages = speedShowcase.GetComponentsInChildren<Image>();
            for (int i = 1; i < speedImages.Length; i++)
            {
                if (i - 1 < manager.speedStat)
                {
                    speedImages[i].color = Color.yellow;

                }
                else
                {
                    speedImages[i].color = Color.white;

                }
            }
        }

        if (startButton != null)
        {
            startButton.SetActive(true);
        }
    }

    public void SelectVolcano()
    {
        manager.SelectVolcano();
    }

    public void SelectBeret()
    {
        manager.SelectBeret();
    }

    public void SelectTundra()
    {
        manager.SelectTundra();
    }

}
