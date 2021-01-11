using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public enum ShipType
    {
        player,
        enemy
    }

    [SerializeField]
    protected int s_health = 3;
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
    public int HEALTH
    {
        get { return s_health; }
        set { s_health = value; }
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
}
