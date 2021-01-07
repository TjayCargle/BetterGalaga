using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public ProjectileBase spawnedProjectile = null;
    private List<ProjectileBase> inactiveProjectiles = new List<ProjectileBase>();

    public ProjectileBase GetProjectile(ShipBase fireingShip)
    {


        ProjectileBase returnedProjectile = null;
        if (inactiveProjectiles.Count > 0)
        {
            returnedProjectile = inactiveProjectiles[0];
            inactiveProjectiles.Remove(returnedProjectile);
        }
        else
        {
            returnedProjectile = Instantiate(spawnedProjectile, fireingShip.transform.position, Quaternion.identity);
        }

        if (returnedProjectile != null)
        {
            returnedProjectile.gameObject.SetActive(true);
            returnedProjectile.transform.position = fireingShip.transform.position;
            returnedProjectile.myPool = this;
            returnedProjectile.p_currentSpeed = returnedProjectile.p_defaultSpeed + fireingShip.SPEED;
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
