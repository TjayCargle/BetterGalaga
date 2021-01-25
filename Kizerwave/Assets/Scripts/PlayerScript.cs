using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PlayerBase
{
    public CharacterController characterController;
    public GameObject player;
    public int playerScore;
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
        characterController.detectCollisions = false;
        playerIsAlive = true;
        playerIsPlayable = true;
        playerScore = 0;
        SPECIAL = 0;
    }

    private void Awake()
    {

        if (playerHUD == null)
        {
            playerHUD = GameObject.FindObjectOfType<HUDUpdate>();
        }
        StatManager stats = GameObject.FindObjectOfType<StatManager>();

        if(stats != null)
        {
            if(stats.selectedMesh != null)
            {
            MeshFilter myMesh = gameObject.GetComponentInChildren<MeshFilter>();
                myMesh.mesh = stats.selectedMesh;
                myMesh.transform.localScale = new Vector3(stats.meshScale, stats.meshScale, stats.meshScale);
            }

            if(stats.selectedMaterial != null)
            {
                MeshRenderer myMaterial = gameObject.GetComponentInChildren<MeshRenderer>();
                myMaterial.material = stats.selectedMaterial;
            }

            fireDelay = 0.6f - ((float)stats.fireRateStat / 10.0f);
            SPEED = 15 + (5 * stats.speedStat);
            BoxCollider myCollider = GetComponentInChildren<BoxCollider>();
           if(myCollider != null)
            {
                myCollider.size = new Vector3((1.0f - (0.15f * (float)stats.speedStat)), 1.0f - (0.15f * (float)stats.speedStat), 1.0f - (0.15f * (float)stats.speedStat)); 
            }

            MaxHealth = 5 * stats.healthStat;
            MaxShield = 3 * stats.healthStat;

            s_health = MaxHealth;
            s_shield = MaxShield;
           if (playerHUD != null)
            {
                playerHUD.playerHealthBar.maxValue = MaxHealth;
                playerHUD.playerHealthBar.value = MaxHealth;
                playerHUD.playerShieldBar.maxValue = MaxShield;
                playerHUD.playerShieldBar.value = MaxShield;
            }
        
        }
        else
        {
            SPEED = 20;

        }

    }


    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            characterController.detectCollisions = false;

            //float yStore = moveDirection.y;
            moveDirection.x = (Input.GetAxis("Horizontal"));
            moveDirection.y = (Input.GetAxis("Vertical"));
            moveDirection.z = 0;
            moveDirection = moveDirection * SPEED;
            transform.localEulerAngles = new Vector3(-90, moveDirection.x * -1, transform.localEulerAngles.z);
            if (moveDirection.x > 0 && transform.position.x + (moveDirection.x * Time.deltaTime) > 35)
                moveDirection.x = 0;

            if (moveDirection.x < 0 && transform.position.x + (moveDirection.x * Time.deltaTime) < -35)
                moveDirection.x = 0;

            if (moveDirection.y > 0 && transform.position.y + (moveDirection.y * Time.deltaTime) > 20)
                moveDirection.y = 0;

            if (moveDirection.y < 0 && transform.position.y + (moveDirection.y * Time.deltaTime) < -20)
                moveDirection.y = 0;

            if (transform.position.z != 40)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 40);
            }

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
                if (characterController.enabled)
                    characterController.Move(moveDirection * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection * Time.deltaTime, SPEED * Time.fixedDeltaTime);
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

            if (Input.GetKeyDown(KeyCode.Q))
            {
                normalWeapon = WEAPON;
                WEAPON = PICKUP;
                PICKUP = normalWeapon;

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Bomb();

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (SPECIAL >= 100)
                {

                    LaserSpecial();
                    SPECIAL = 0;
                    playerHUD.ValidateChanges();

                }

            }
        }
    }
}
