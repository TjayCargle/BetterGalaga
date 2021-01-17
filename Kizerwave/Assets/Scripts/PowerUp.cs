using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpType;
    public bool isPaused = false;
    public float p_lifespan = 10.0f;
    public float xDirection = 0;

    private void Awake()
    {
        xDirection = Random.Range(-5, 5);
    }
    void OnTriggerEnter(Collider theObject)
    {
        if (theObject.GetComponent<PlayerScript>())
        {
            Pickup(theObject);
        }
    }

    void Pickup(Collider theplayer)
    {

        PlayerScript player = theplayer.GetComponent<PlayerScript>();

        player.PICKUP = player.WEAPON;
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

    private void Update()
    {
        if (isPaused == false)
        {
            if (p_lifespan > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(xDirection, 1, 0), 10 * Time.deltaTime);
                transform.localEulerAngles = transform.localEulerAngles + new Vector3(1, 0, 1);
                p_lifespan -= 1.5f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);

            }
        }
    }
}
