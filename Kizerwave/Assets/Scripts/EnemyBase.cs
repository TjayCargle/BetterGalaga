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
    public bool inFormation = false;
    public Vector3 StartingPosition;
    public Vector3 StartingRotation;
    public PoolManager somePool = null;
    public int score;

    public bool startInFormation = false;
    public bool endInFormation = true;
    public bool fireAtWaypoints = false;
    public bool fireAtIntervals = true;
    public float fireIntervals = 5.0f;
    public SquadManager squadManager;
    public Vector3 formationLocation = Vector3.zero;

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
        if (somePool != null)
        {
            ProjectileBase defaultProjectile = somePool.GetProjectile(this);
            defaultProjectile.p_initialRotation = new Vector3(90, 0, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);

        }
    }

    public override void Despawn()
    {
        base.Despawn();
    }
}
