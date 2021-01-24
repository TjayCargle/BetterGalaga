using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDUpdate : MonoBehaviour
{
    public Slider playerHealthBar = null;
    public Slider playerShieldBar = null;
    public Text playerLives = null;
    public Text playerBombs = null;
    public Text playerCurrentWeapon = null;
    public Text playerPickup = null;
    public Text playerScore = null;
    public List<Sprite> shotTypes = new List<Sprite>();

    public Image currentWeaponImg = null;
    public Image playerPickupImg = null;
    public Image healthSliderFill = null;
    public PlayerScript thePlayer = null;
    public Slider specialSlider = null;

    private void Awake()
    {
        StatManager stats = GameObject.FindObjectOfType<StatManager>();

        if (stats != null)
        {
            if(playerHealthBar != null)
            {
                RectTransform rect = playerHealthBar.GetComponent<RectTransform>();
                Vector2 updateRect = rect.sizeDelta;
                updateRect.x = 272 + (100 * stats.healthStat);
                rect.sizeDelta = updateRect;
                rect.position = new Vector3((rect.position.x + (100 * (float)stats.healthStat * 0.5f)), rect.position.y, rect.position.z);
            }

            if (playerShieldBar != null)
            {
                RectTransform rect = playerShieldBar.GetComponent<RectTransform>();
                Vector2 updateRect = rect.sizeDelta;
                updateRect.x = 222 + (50 * stats.healthStat);
                rect.sizeDelta = updateRect;
                rect.position = new Vector3((rect.position.x + (50 * (float)stats.healthStat * 0.5f)), rect.position.y, rect.position.z);
            }
        }
    }

    public void ValidateChanges()
    {
        if (thePlayer != null)
        {
            if (playerHealthBar != null)
            {
                playerHealthBar.value = thePlayer.HEALTH;

                if (healthSliderFill != null)
                    healthSliderFill.color = Color.Lerp(Color.red, Color.green, playerHealthBar.value / playerHealthBar.maxValue);

            }

            if (playerShieldBar != null)
            {
                playerShieldBar.value = thePlayer.SHIELD;
            }

            if (playerLives != null)
            {
                playerLives.text = "x" + thePlayer.LIVES;
            }

            if (playerBombs != null)
            {
                playerBombs.text = "x" + thePlayer.BOMBS;


            }

            if (playerCurrentWeapon != null)
            {
                switch (thePlayer.WEAPON)
                {
                    case TJayEnums.MissileType.normal:
                        playerCurrentWeapon.text = "Missile";
                        break;
                    case TJayEnums.MissileType.Spread:
                        playerCurrentWeapon.text = "Spread";
                        break;
                    case TJayEnums.MissileType.Cluster:
                        playerCurrentWeapon.text = "Cluster";
                        break;
                    case TJayEnums.MissileType.Protective:
                        playerCurrentWeapon.text = "Protect";
                        break;
                    case TJayEnums.MissileType.Homing:
                        playerCurrentWeapon.text = "Homing";

                        break;
                }

                if (currentWeaponImg != null)
                {

                    if ((int)thePlayer.WEAPON < shotTypes.Count)
                    {
                        currentWeaponImg.sprite = shotTypes[(int)thePlayer.WEAPON];
                    }
                }


            }

            if (playerPickup != null)
            {
                switch (thePlayer.PICKUP)
                {
                    case TJayEnums.MissileType.normal:
                        {
                            if (thePlayer.PICKUP == TJayEnums.MissileType.normal && thePlayer.WEAPON == TJayEnums.MissileType.normal)
                                playerPickup.text = "No Pickup";
                            else
                                playerPickup.text = "Missile";
                        }
                        break;
                    case TJayEnums.MissileType.Spread:
                        playerPickup.text = "Spread";
                        break;
                    case TJayEnums.MissileType.Cluster:
                        playerPickup.text = "Cluster";
                        break;
                    case TJayEnums.MissileType.Protective:
                        playerPickup.text = "Protect";
                        break;
                    case TJayEnums.MissileType.Homing:
                        playerPickup.text = "Homing";

                        break;
                }

                if (playerPickupImg != null)
                {

                    if ((int)thePlayer.PICKUP < shotTypes.Count)
                    {
                        if (thePlayer.PICKUP == TJayEnums.MissileType.normal && thePlayer.WEAPON == TJayEnums.MissileType.normal)
                        {
                            playerPickupImg.sprite = shotTypes[5];
                        }
                        else
                        {
                            playerPickupImg.sprite = shotTypes[(int)thePlayer.PICKUP];
                        }
                    }
                }

                if (specialSlider != null)
                {

                    StopCoroutine(updateBars());
                    StartCoroutine(updateBars());
                }

            }

        }
    }

    IEnumerator updateBars()
    {
        if (specialSlider != null)
        {
            while (Mathf.Abs(PlayerBase.specialValue - specialSlider.value ) > 1)
            {
                float newVal = Mathf.Lerp(specialSlider.value, PlayerBase.specialValue, 1 * Time.deltaTime);

                specialSlider.value = newVal;



                yield return null;
            }
        }

    }
}
