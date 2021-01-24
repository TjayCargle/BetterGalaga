using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelComplete : StatDisplay
{
    // 0 = Health, 1 = Fire Rate, 2 = speed, -1 = nothing selected;
    public int selectedUpgrade = -1;
    private int tempHealth = 0;
    private int tempFire = 0;
    private int tempSpeed = 0;
    private void Awake()
    {
        manager = StatManager.Instance;
        UpdateBars();
    }


    public void ZeroOut()
    {
        if (nameText != null)
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

        if (startButton != null)
        {
            startButton.SetActive(false);
        }
        selectedUpgrade = -1;
    }
    public void SetUpgrade(int newVal)
    {
        if (selectedUpgrade == newVal)
        {
            selectedUpgrade = -1;
        }
        else
        {
            selectedUpgrade = newVal;
            tempHealth = manager.healthStat;
            tempFire = manager.fireRateStat;
            tempSpeed = manager.speedStat;

            switch (selectedUpgrade)
            {
                case 0:
                    tempHealth++;
                    if (tempHealth > 5)
                        selectedUpgrade = -1;

                    break;

                case 1:
                    tempFire++;
                    if (tempFire > 5)
                        selectedUpgrade = -1;

                    break;

                case 2:
                    tempSpeed++;
                    if (tempSpeed > 5)
                        selectedUpgrade = -1;

                    break;
            }
        }

        UpdateBars();

    }
    public void UpdateBars()
    {
        // if (selectedUpgrade == -1)
        {

            tempHealth = manager.healthStat;
            tempFire = manager.fireRateStat;
            tempSpeed = manager.speedStat;

            switch (selectedUpgrade)
            {
                case 0:
                    tempHealth++;
                    break;

                case 1:
                    tempFire++;
                    break;

                case 2:
                    tempSpeed++;
                    break;
            }

            if (nameText != null)
            {
                nameText.text = manager.playerName;
            }
            if (healthShowcase != null)
            {
                healthImages = healthShowcase.GetComponentsInChildren<Image>();
                for (int i = 1; i < healthImages.Length; i++)
                {
                    if (i - 1 < tempHealth)
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
                    if (i - 1 < tempFire)
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
                    if (i - 1 < tempSpeed)
                    {
                        speedImages[i].color = Color.yellow;

                    }
                    else
                    {
                        speedImages[i].color = Color.white;

                    }
                }
            }
            if (selectedUpgrade != -1)
            {
                if (startButton != null)
                {
                    startButton.SetActive(true);
                }
                else
                {
                    startButton.SetActive(false);
                }
            }
        }
        // else
        {
            //  ZeroOut();
        }
    }




}
