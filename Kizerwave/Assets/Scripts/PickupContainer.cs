using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupContainer : MonoBehaviour
{
    public List<PowerUp> powerUp = new List<PowerUp>();

    private static List<PowerUp> realPowerUps = new List<PowerUp>();

    private void Awake()
    {
        realPowerUps.AddRange(powerUp);

    }

    public static PowerUp GetPowerUp(int type, Vector3 pos)
    {


        PowerUp returnedPowerup = null;
        if (type < realPowerUps.Count)
        {
            returnedPowerup = Instantiate(realPowerUps[type], pos, Quaternion.identity);
            returnedPowerup.transform.localEulerAngles = new Vector3(0, 90, 0);
        }



        return returnedPowerup;
    }
}
