using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public PoolManager myPool = null;

    public float p_maxLifespan = 5.0f;
    public float p_lifespan = 5.0f;
    public Vector2 p_direction = Vector2.down;
    public float p_currentSpeed = 5.0f;
    public float p_defaultSpeed = 5.0f;
    public float p_decrease = 1.5f;
    public bool p_active = false;
    public ShipBase p_owner = null;
    public Vector3 p_initialRotation = Vector3.zero;
    public Vector3 p_updateRotation = new Vector3(0, 1.5f, 0);
    public Vector3 moveDirection = Vector3.down;

    private void Start()
    {
        transform.localEulerAngles = p_initialRotation;
    }

    private void Awake()
    {
        transform.localEulerAngles = p_initialRotation;
    }

    public void Despawn()
    {
        if (myPool != null)
        {
            myPool.ReturnProjectile(this);
            this.gameObject.SetActive(false);
            p_active = false;
        }

    }

    private void Update()
    {
        if (p_active == true)
        {
            if (p_lifespan > 0)
            {


                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, p_defaultSpeed * Time.deltaTime);
                p_lifespan -= p_decrease * Time.deltaTime;
                transform.localEulerAngles = transform.localEulerAngles + p_updateRotation;
            }
            else
            {
                Despawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShipBase>())
        {

            ShipBase someShip = other.gameObject.GetComponent<ShipBase>();

            if (someShip.SHIPTYPE != p_owner.SHIPTYPE)
            {
                someShip.HEALTH -= 1;
                if (someShip.HEALTH <= 0)
                {

                    if (someShip.SHIPTYPE == ShipBase.ShipType.enemy)
                    {
                        EnemyBase anEnemy = someShip.GetComponent<EnemyBase>();
                        if (anEnemy != null)
                        {

                            SquadManager sqm = anEnemy.squadManager;

                            if (sqm != null)
                            {

                                sqm.aliveEnemies.Remove(anEnemy);
                            }
                            Destroy(anEnemy.gameObject);
                        }
                    }
                }
                Despawn();
            }
        }
        else if (other.gameObject.GetComponent<ProjectileBase>())
        {

            ProjectileBase someProjectile = other.gameObject.GetComponent<ProjectileBase>();
            if (someProjectile.p_owner != p_owner)
            {
                Despawn();
            }
        }
    }
}
