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


                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, p_defaultSpeed * Time.deltaTime);
                p_lifespan -= p_decrease * Time.deltaTime;
            }
            else
            {
                Despawn();
            }
        }
    }
}
