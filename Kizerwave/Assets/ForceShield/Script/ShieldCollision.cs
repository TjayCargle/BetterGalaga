using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public PlayerScript myPlayer = null;
    [SerializeField] string[] _collisionTag;
    float hitTime;
    Material mat;

    void Start()
    {
        if (GetComponent<Renderer>())
        {
            mat = GetComponent<Renderer>().sharedMaterial;
        }

        if (myPlayer == null)
        {
            myPlayer = GameObject.FindObjectOfType<PlayerScript>();
        }

    }

    void Update()
    {

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
            mat.SetFloat("_HitTime", hitTime);
        }

    }



    void OnCollisionEnter(Collision collision)
    {
        // for (int i = 0; i < _collisionTag.Length; i++)
        {


            //  if (_collisionTag.Length > 0 || collision.transform.CompareTag(_collisionTag[i]))
            if (collision.collider.gameObject.GetComponent<ProjectileBase>())
            {
                if (collision.collider.gameObject.GetComponent<ProjectileBase>().p_owner.SHIPTYPE != ShipBase.ShipType.player)
                {

                    ContactPoint[] _contacts = collision.contacts;
                    for (int i2 = 0; i2 < _contacts.Length; i2++)
                    {
                        mat.SetVector("_HitPosition", transform.InverseTransformPoint(_contacts[i2].point));
                        hitTime = 500;
                        mat.SetFloat("_HitTime", hitTime);
                    }
                    collision.collider.gameObject.GetComponent<ProjectileBase>().Despawn();
                    if (myPlayer != null)
                    {
                        if (myPlayer.SHIELD > 0)
                            myPlayer.SHIELD--;
                    }
                }
            }
        }
    }



}

