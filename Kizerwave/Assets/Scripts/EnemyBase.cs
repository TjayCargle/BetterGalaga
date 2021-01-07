using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{
    [SerializeField]
    protected List<Vector3> s_waypoints = new List<Vector3>();
    [SerializeField]
    protected int currentWaypoint = -1;
    [SerializeField]
    protected bool inFormation = false;
    public Vector3 StartingPosition;
    public Vector3 StartingRotation;
    public PoolManager somePool = null;

    public bool fireAtWaypoints = false;
    public bool fireAtIntervals = true;
    public float fireIntervals = 5.0f;
    public List<Vector3> WAYPOINTS
    {
        get { return s_waypoints; }
    }

    private void Start()
    {
        if (somePool == null)
        {
            somePool = GameObject.FindObjectOfType<PoolManager>();
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
    }
    public override void Fire()
    {
        if (somePool != null)
        {
            ProjectileBase defaultProjectile = somePool.GetProjectile(this);

        }
    }
}
