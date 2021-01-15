using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PlayerBase
{
    public CharacterController characterController;
    public GameObject player;



    //Player Movement Verb
    //all inherited
    //[SerializeField] private float moveSpeed;
    //[SerializeField] private float health;
    //[SerializeField] private float damage;
    //[SerializeField] private float gravityScale;

    public bool playerIsAlive;
    public bool playerIsPlayable;
    private Vector3 moveDirection = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerIsAlive = true;
        playerIsPlayable = true;
        SPEED = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {

            //float yStore = moveDirection.y;
            moveDirection.x = (Input.GetAxis("Horizontal"));
            moveDirection.y = (Input.GetAxis("Vertical"));
            moveDirection.z = 0;
            moveDirection = moveDirection * SPEED;


            transform.localEulerAngles = new Vector3(-90, moveDirection.x * -1, transform.localEulerAngles.z);

            //if (characterController.isGrounded)
            //{
            //    moveDirection.y = Physics.gravity.y * Time.deltaTime;

            //}
            //else if (!characterController.isGrounded)
            //{
            //    moveDirection.z = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            //}

            if (playerIsPlayable)
            {
                characterController.Move(moveDirection * Time.deltaTime);
            }

            if (canFire == false)
            {

                if (firetime > 0)
                {
                    firetime -= 1 * Time.deltaTime;
                }
                if (firetime <= 0)
                {
                    canFire = true;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (canFire)
                {
                    firetime = fireDelay;
                    Fire();
                    canFire = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                if((int)secondaryWeapon < 5)
                {
                    secondaryWeapon++;
                }
                else
                {
                    secondaryWeapon = TJayEnums.MissileType.normal;
                }
            }

        }
    }
}
