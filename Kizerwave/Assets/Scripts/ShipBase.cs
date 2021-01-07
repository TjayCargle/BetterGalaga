using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    [SerializeField]
    protected int s_health = 3;
    [SerializeField]
    protected float s_speed = 1;
    [SerializeField]
    protected int s_dmg = 1;
    [SerializeField]
    protected int s_level = 1;

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

    public virtual void TakeDamage(int dmgVal)
    {
        HEALTH -= dmgVal;
    }

    public virtual void Fire()
    {

    }
}
