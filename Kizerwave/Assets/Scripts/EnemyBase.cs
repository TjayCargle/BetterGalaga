using System.Collections;
using System.Collections.Generic;
using TJayEnums;
using UnityEngine;

public class EnemyBase : ShipBase
{

    [SerializeField]
    protected List<Vector3> s_waypoints = new List<Vector3>();
    [SerializeField]
    protected int currentWaypoint = -1;
    [SerializeField]
    public bool inFormation = false;
    public Vector3 StartingPosition;
    public Vector3 StartingRotation;
    public PoolManager bulletPool = null;
    public int score;

    public bool startInFormation = false;
    public bool endInFormation = true;
    public bool fireAtWaypoints = false;
    public bool fireAtIntervals = true;
    public float fireIntervals = 5.0f;
    public SquadManager squadManager;
    public Vector3 formationLocation = Vector3.zero;
    public MissileType shotType = MissileType.normal;
    [SerializeField]
    protected EnemyHealthBar myHealthBar = null;

    public override int HEALTH
    {
        get { return s_health; }
        set { s_health = value;
            if(myHealthBar != null)
            {
                myHealthBar.gameObject.SetActive(true);
                myHealthBar.UpdateHealthBar();
            }
        }
    }
    public List<Vector3> WAYPOINTS
    {
        get { return s_waypoints; }
    }

    private void Start()
    {
        if (bulletPool == null)
        {
            bulletPool = GameObject.FindObjectOfType<PoolManager>();
        }

        if (fireAtIntervals == true)
        {
            StartCoroutine(DelayedFire());
        }

        
    }

   

    IEnumerator DelayedFire()
    {
        float timer = fireIntervals;
        while (true)
        {
            if (isPaused == false)
            {

                if (fireAtIntervals == false)
                {
                    break;
                }

                if (timer > 0)
                {
                    timer -= 1 * Time.deltaTime;
                    yield return null;
                }
                else
                {
                    Fire();
                    timer = fireIntervals;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    public override void Fire()
    {
        if (bulletPool != null)
        {
            // ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, TJayEnums.MissileType.normal);
            // defaultProjectile.p_initialRotation = new Vector3(90, 0, 0);
            // defaultProjectile.transform.localEulerAngles = new Vector3(90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            switch (shotType)
            {
 
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
         
                default:
                    NormalShot();
                    break;
            }
            SFXLibrary.PlayEnemyFire();
        }
    }

    public void NormalShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.normal);
            defaultProjectile.p_initialRotation = new Vector3(90, 0, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            SFXLibrary.PlayDefaultMissile();
        }
    }

    public void ClusterShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Cluster);
            //defaultProjectile.missleType = TJayEnums.MissileType.Cluster;
            firetime = fireDelay * 4.5f;
            defaultProjectile.p_lifespan = defaultProjectile.p_lifespan * 0.5f;
            defaultProjectile.transform.localEulerAngles = new Vector3(90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            SFXLibrary.PlayClusterMissile();
        }
    }

    public void SpreadShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            defaultProjectile.p_initialRotation = new Vector3(90, 0, 0);
            defaultProjectile.moveDirection = new Vector3(0, -1, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase secondProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            secondProjectile.p_initialRotation = new Vector3(45, 45, 0);
            secondProjectile.moveDirection = new Vector3(1, -1, 0);
            secondProjectile.transform.localEulerAngles = new Vector3(45, 45, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase thirdProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            thirdProjectile.p_initialRotation = new Vector3(45, -45, 0);
            thirdProjectile.moveDirection = new Vector3(-1, -1, 0);
            thirdProjectile.transform.localEulerAngles = new Vector3(45, -45, defaultProjectile.transform.localEulerAngles.z);
            SFXLibrary.PlaySpreadMissile();
        }
    }

    public void HomingShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Homing);
            defaultProjectile.missleType = TJayEnums.MissileType.Homing;
            SFXLibrary.PlayHomingMissile();
        }
    }

    public void ProtectiveShot()
    {
        timesCalled++;
        if (timesCalled == 3 && protectiveCount < maxProtectiveCount)
        {
            if (bulletPool != null)
            {
                ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Protective);
                defaultProjectile.missleType = TJayEnums.MissileType.Protective;
                defaultProjectile.p_lifespan = defaultProjectile.p_maxLifespan * 3;
                protectiveCount++;
                timesCalled = 0;
                SFXLibrary.PlayProtectMissile();
            }
        }
        else
        {
            NormalShot();

        }
    }

    public override void Despawn()
    {
        base.Despawn();
    }
}
