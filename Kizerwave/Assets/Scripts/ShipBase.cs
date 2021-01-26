using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJayEnums;
namespace TJayEnums
{
    public enum MissileType
    {
        normal,
        Spread,
        Cluster,
        Protective,
        Homing,
        Bomb,
        Laser,
        Random

    }
}
public class ShipBase : MonoBehaviour
{
    [SerializeField]
    protected bool isPaused = false;
    public enum ShipType
    {
        player,
        enemy
    }

    [SerializeField]
    protected int s_health = 5;
    [SerializeField]
    protected float s_speed = 1;
    [SerializeField]
    protected int s_dmg = 1;
    [SerializeField]
    protected int s_level = 1;



    [SerializeField]
    protected Vector3 firingDirection = Vector3.down;
    [SerializeField]
    protected ShipType type = ShipType.enemy;
    protected float firetime;
    public float fireDelay = 5.0f;

    public int protectiveCount = 0;
    public int maxProtectiveCount = 10;
    [SerializeField]
    protected int timesCalled = 0;

    public MissileType powerupDrop = MissileType.Random;
    public float dropChance = 25.0f;

    public virtual int HEALTH
    {
        get { return s_health; }
        set { s_health = value; }
    }
    public bool ISPAUSED
    {
        get { return isPaused; }
    }


    public float SPEED
    {
        get { return s_speed; }
        set { s_speed = value; }
    }

    public int DMG
    {
        get { return s_dmg; }
        set { s_dmg = value; }
    }

    public int LEVEL
    {
        get { return s_level; }
        set { s_level = value; }
    }

    public Vector3 FIRE_DIRECTION
    {
        get { return firingDirection; }
        set { firingDirection = value; }
    }

    public ShipType SHIPTYPE
    {
        get { return type; }
        set { type = value; }
    }

    public virtual void TakeDamage(int dmgVal)
    {
        HEALTH -= dmgVal;
    }

    public virtual void Fire()
    {

    }

    public virtual void Despawn()
    {

    }

    public virtual void Pause()
    {
        isPaused = true;
    }

    public virtual void Resume()
    {
        isPaused = false;
    }
}
