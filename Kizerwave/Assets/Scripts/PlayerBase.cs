using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : ShipBase
{

    public float fireRate = 5.0f;
    public bool canFire = true;
    public PoolManager bulletPool = null;



    // Start is called before the first frame update
    void Start()
    {

        
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DelayedFire()
    {
        float timer = fireRate;
        while (true)
        {
            if (canFire == false)
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
                timer = fireRate;
            }
        }
    }
    public override void Fire()
    {
        if (bulletPool != null)
        {
            ProjectileBase defaultProjectile = bulletPool.GetProjectile(this);
            defaultProjectile.p_initialRotation = new Vector3(-90, 0, 0);
            defaultProjectile.transform.localEulerAngles = new Vector3(-90, defaultProjectile.transform.localEulerAngles.y, defaultProjectile.transform.localEulerAngles.z);
        }
    }
}
