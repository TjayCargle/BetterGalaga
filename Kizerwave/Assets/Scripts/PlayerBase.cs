using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJayEnums;
public class PlayerBase : ShipBase
{

    public bool canFire = true;
    public PoolManager bulletPool = null;
    protected MissileType secondaryWeapon = MissileType.normal;


    // Start is called before the first frame update
    void Start()
    {

        
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Fire()
    {
        if (bulletPool != null)
        {
            switch (secondaryWeapon)
            {
                case MissileType.normal:
                    NormalShot();
                    break;
                case MissileType.Spread:
                    SpreadShot();
                    break;
                case MissileType.Cluster:
            ClusterShot();
                    break;
                case MissileType.Protective:
             ProtectiveShot();
                    break;
                case MissileType.Homing:
             HomingShot();
                    break;
             
            }
        }
    }


    public void NormalShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
            defaultProjectile.p_initialRotation = new Vector3(-90, 0, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(-90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);
        }
    }

    public void ClusterShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
            defaultProjectile.missleType = TJayEnums.MissileType.Cluster;
            firetime = fireDelay * 4.5f ;
            defaultProjectile.p_lifespan = defaultProjectile.p_lifespan * 0.5f;

        }
    }

    public void SpreadShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
            defaultProjectile.p_initialRotation = new Vector3(-90, 0, 0);
            defaultProjectile.moveDirection = new Vector3(0, 1, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(-90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase secondProjectile = bulletPool.GetProjectile(this);
            secondProjectile.p_initialRotation = new Vector3(-45, 45, 0);
            secondProjectile.moveDirection = new Vector3(1, 1, 0);
            secondProjectile.transform.localEulerAngles = new Vector3(-45, 45, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase thirdProjectile = bulletPool.GetProjectile(this);
            thirdProjectile.p_initialRotation = new Vector3(-45, -45, 0);
            thirdProjectile.moveDirection = new Vector3(-1, 1, 0);
            thirdProjectile.transform.localEulerAngles = new Vector3(-45, -45, defaultProjectile.transform.localEulerAngles.z);

        }
    }

    public void HomingShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
            defaultProjectile.missleType = TJayEnums.MissileType.Homing;
        }
    }

    public void ProtectiveShot()
    {
        timesCalled++;
        if(timesCalled == 3 && protectiveCount < maxProtectiveCount)
        {
            if (bulletPool != null)
            {
                ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
                defaultProjectile.missleType = TJayEnums.MissileType.Protective;
                defaultProjectile.p_lifespan = defaultProjectile.p_maxLifespan * 3;
                protectiveCount++;
                timesCalled = 0;
            }
        }
        else
        {
            NormalShot();
        }
    }
}
