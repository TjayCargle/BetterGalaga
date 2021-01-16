using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpType;

    void OnTriggerEnter(Collider theObject)
    {
        if (theObject.CompareTag("Player"))
        {
            Pickup(theObject);
        }
    }

    void Pickup(Collider theplayer)
    {

        PlayerScript player = theplayer.GetComponent<PlayerScript>();
        if (powerUpType == 1)
        {
            player.WEAPON = TJayEnums.MissileType.Spread;
        }
        else if (powerUpType == 2)
        {
            player.WEAPON = TJayEnums.MissileType.Cluster;

        }
        else if (powerUpType == 3)
        {
            player.WEAPON = TJayEnums.MissileType.Protective;

        }
        else if (powerUpType == 4)
        {
            player.WEAPON = TJayEnums.MissileType.Homing;

        }
        Destroy(gameObject);
    }

}
