using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJayEnums;
public class PlayerBase : ShipBase
{
    public HUDUpdate playerHUD = null;
    public bool canFire = true;
    public PoolManager bulletPool = null;
    [SerializeField]
    protected MissileType secondaryWeapon = MissileType.normal;
    protected MissileType normalWeapon = MissileType.normal;
    [SerializeField]
    protected MissileType currentWeapon = MissileType.normal;
    protected int bombCount = 3;
    [SerializeField]
    protected int s_shield = 5;

    [SerializeField]
    protected int s_lives = 3;

    public GameObject shieldObject = null;
    public static bool usingSpecial = false;
    private static int specialValue = 0;

    public static int SPECIAL
    {
        get { return specialValue; }
        set { specialValue = value;
            if (specialValue > 100)
                specialValue = 100;
            
        }

    }
    protected int MaxHealth = 5;
    protected int MaxShield = 5;

    public override int HEALTH
    {
        get { return s_health; }
        set
        {
            if (SHIELD > 0)
            {
                SHIELD--;
            }
            else
            {
                s_health = value;
                if (s_health > MaxHealth)
                    s_health = MaxHealth;
            }
            UpdateHUD();
        }
    }

    public int BOMBS
    {
        get { return bombCount; }
        set { bombCount = value; UpdateHUD(); }
    }
    public MissileType WEAPON
    {
        get { return currentWeapon; }
        set { currentWeapon = value; UpdateHUD(); }
    }

    public MissileType PICKUP
    {
        get { return secondaryWeapon; }
        set { secondaryWeapon = value; UpdateHUD(); }
    }

    public int SHIELD
    {
        get { return s_shield; }
        set
        {
            s_shield = value; UpdateHUD();
            if (shieldObject != null)
            {
                if (SHIELD <= 0)
                {
                    shieldObject.SetActive(false);
                }
                else
                {
                    if(shieldObject.activeInHierarchy == false)
                    {
                        shieldObject.SetActive(true);
                        if (s_shield > MaxShield)
                            s_shield = MaxShield;
                    }
                }
            }
        }
    }

    public int LIVES
    {
        get { return s_lives; }
        set { if (value < s_lives) { s_health = MaxHealth; }  s_lives = value; UpdateHUD(); }
    }
 
    private void UpdateHUD()
    {
        if (playerHUD != null)
        {
            playerHUD.ValidateChanges();
        }
    }

    public override void Fire()
    {
        if (bulletPool != null)
        {
            switch (currentWeapon)
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
                case MissileType.Bomb:
                    Bomb();
                    break;

            }
        }
    }

    IEnumerator SpeacialShot(float delay, int length)
    {
        float timer = delay;
        int amount = 0;
        while (true)
        {
            if (isPaused == false)
            {

                usingSpecial = true;
                if (timer > 0)
                {
                    timer -= 1 * Time.deltaTime;
                    yield return null;
                }
                else
                {

                    timer = delay;
                 if(amount < length)
                    {
                        LaserSpecial(Vector3.up * 8);
                        amount++;
                        SFXLibrary.PlayFireLaser();
                    }
                 if(amount >= length)
                    {
                        usingSpecial = false;
                        break;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public void LaserSpecial()
    {
        StartCoroutine(SpeacialShot(0.25f, 16));
    }

    public void LaserSpecial(Vector3 offsetPos)
    {
        ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Laser);
        defaultProjectile.p_initialRotation = new Vector3(0, 0, 0);
        defaultProjectile.transform.position += offsetPos;
        defaultProjectile.transform.localEulerAngles = new Vector3(0, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

    }

    public void NormalShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.normal);
            defaultProjectile.p_initialRotation = new Vector3(-90, 0, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(-90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

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

            SFXLibrary.PlayClusterMissile();
        }
    }

    public void SpreadShot()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            defaultProjectile.p_initialRotation = new Vector3(-90, 0, 0);
            defaultProjectile.moveDirection = new Vector3(0, 1, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(-90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase secondProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            secondProjectile.p_initialRotation = new Vector3(-45, 45, 0);
            secondProjectile.moveDirection = new Vector3(1, 1, 0);
            secondProjectile.transform.localEulerAngles = new Vector3(-45, 115, defaultProjectile.transform.localEulerAngles.z);

            ProjectileBase thirdProjectile = bulletPool.GetProjectile(this, MissileType.Spread);
            thirdProjectile.p_initialRotation = new Vector3(-45, -45, 0);
            thirdProjectile.moveDirection = new Vector3(-1, 1, 0);
            thirdProjectile.transform.localEulerAngles = new Vector3(-45, -115, defaultProjectile.transform.localEulerAngles.z);
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
        if (timesCalled >= 3 && protectiveCount < maxProtectiveCount)
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

    public void Bomb()
    {
        if (bombCount > 0)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this, MissileType.Bomb);
            defaultProjectile.missleType = TJayEnums.MissileType.Bomb;
            firetime = fireDelay * 4.5f;
            defaultProjectile.p_lifespan = defaultProjectile.p_lifespan * 0.5f;
            BOMBS--;

            
        }
    }
}
