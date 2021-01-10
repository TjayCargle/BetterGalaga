using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PlayerBase
{
    public CharacterController characterController;
    public GameObject player;



    //Player Movement Verb
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float gravityScale;

    public bool playerIsAlive;
    public bool playerIsPlayable;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerIsAlive = true;
        playerIsPlayable = true;
        moveSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (characterController.isGrounded)
        {
            moveDirection.y = Physics.gravity.y * Time.deltaTime;

        }
        else if (!characterController.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }

        if (playerIsPlayable)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Fire();
        }

    }
}
