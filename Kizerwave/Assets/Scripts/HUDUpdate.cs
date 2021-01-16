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

    public PlayerScript thePlayer = null;

    public void ValidateChanges()
    {
        if (thePlayer != null)
        {
            if (playerHealthBar != null)
            {
                playerHealthBar.value = thePlayer.HEALTH;
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
                        playerPickup.text = "No Pickup";
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
                        if(thePlayer.PICKUP == TJayEnums.MissileType.normal)
                        {
                            playerPickupImg.sprite = shotTypes[5];
                        }
                        else
                        {
                            playerPickupImg.sprite = shotTypes[(int)thePlayer.PICKUP];
                        }
                    }
                }


            }

        }
    }
}
