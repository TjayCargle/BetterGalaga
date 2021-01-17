using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJayEnums;
public class PoolManager : MonoBehaviour
{
    public ProjectileBase spawnedProjectile = null;
    public ProjectileBase spawnedCluster = null;
    public ProjectileBase spawnedProtective = null;
    public ProjectileBase spawnedHoming = null;
    public ProjectileBase spawnedSpread = null;
    private List<ProjectileBase> inactiveProjectiles = new List<ProjectileBase>();

    private ProjectileBase CreateProjectile(ShipBase fireingShip, MissileType missileType)
    {
        ProjectileBase returnedProjectile = null;
        switch (missileType)
        {
            case MissileType.normal:
                {
                    returnedProjectile = Instantiate(spawnedProjectile, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.normal;
                }
                break;
            case MissileType.Spread:
                {
                    returnedProjectile = Instantiate(spawnedSpread, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.Spread;
                }
                break;
            case MissileType.Cluster:
                {
                    returnedProjectile = Instantiate(spawnedCluster, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.Cluster;
                }
                break;
            case MissileType.Protective:
                {
                    returnedProjectile = Instantiate(spawnedProtective, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.Protective;
                }
                break;
            case MissileType.Homing:
                {
                    returnedProjectile = Instantiate(spawnedHoming, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.Homing;
                }
                break;
            default:
                {
                    returnedProjectile = Instantiate(spawnedProjectile, fireingShip.transform.position, Quaternion.identity);
                    returnedProjectile.missleType = MissileType.normal;
                }
                break;
        }

        return returnedProjectile;
    }

    public ProjectileBase GetProjectile(ShipBase fireingShip, MissileType missileType)
    {


        ProjectileBase returnedProjectile = null;
        if (inactiveProjectiles.Count > 0)
        {
            bool found = false;
            for (int i = 0; i < inactiveProjectiles.Count; i++)
            {
                if(inactiveProjectiles[i].missleType == missileType)
                {
                    found = true;
                    returnedProjectile = inactiveProjectiles[i];
                    inactiveProjectiles.Remove(returnedProjectile);
                    break;
                }
            }
            if(found == false)
            {
                returnedProjectile = CreateProjectile(fireingShip, missileType);

            }
          
        }
        else
        {

            returnedProjectile = CreateProjectile(fireingShip, missileType);
        }

        if (returnedProjectile != null)
        {
            returnedProjectile.gameObject.SetActive(true);
            returnedProjectile.transform.position = fireingShip.transform.position;
            returnedProjectile.myPool = this;
            returnedProjectile.p_owner = fireingShip;
           // returnedProjectile.missleType = TJayEnums.MissileType.normal;
            returnedProjectile.p_currentSpeed = returnedProjectile.p_defaultSpeed + fireingShip.SPEED;
            returnedProjectile.moveDirection = fireingShip.FIRE_DIRECTION;
            returnedProjectile.p_lifespan = returnedProjectile.p_maxLifespan;
            returnedProjectile.p_active = true;

        }
        else
        {
            Debug.Log("Could not find projectile");
        }
        return returnedProjectile;
    }

    public void ReturnProjectile(ProjectileBase someProjectile)
    {
        if (someProjectile != null)
        {
            if (!inactiveProjectiles.Contains(someProjectile))
            {
                inactiveProjectiles.Add(someProjectile);

            }
        }
    }

}
