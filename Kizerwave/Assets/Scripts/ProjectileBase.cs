using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJayEnums;
public class ProjectileBase : MonoBehaviour
{
    public PoolManager myPool = null;
    public GameObject Explosion;
    public GameObject Explosion2;

    [SerializeField]
    protected bool isPaused = false;
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
    public MissileType missleType;

    private ShipBase neareastEnemy = null;
    private ColorChanger colorChanger = null;
    private void Start()
    {
        transform.localEulerAngles = p_initialRotation;

    }

    private void Awake()
    {
        transform.localEulerAngles = p_initialRotation;
    }

    public void UpdateColor()
    {
        if (colorChanger == null)
        {
            colorChanger = GetComponentInChildren<ColorChanger>();
        }
        if (colorChanger != null)
        {
            if (p_owner != null)
            {
                if (p_owner is PlayerScript)
                {
                    colorChanger.SetColor(new Color(0, 1, 1, 0.75f));
                }
                else
                {
                    colorChanger.SetColor(new Color(1, 0, 0, 0.75f));
                }
            }
        }
    }

    public void Despawn()
    {
        if (missleType == MissileType.Cluster)
        {
            SpawnCluster();
        }
        if (missleType == MissileType.Bomb)
        {
            EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();

            for (int i = enemies.Length - 1; i >= 0; i--)
            {
                EnemyBase someEnemy = enemies[i];
                float sqrDist = (transform.position - someEnemy.transform.position).magnitude;


                if (sqrDist < 25)
                {
                    someEnemy.TakeDamage(3);

                    if (someEnemy.HEALTH <= 0)
                    {
                        SquadManager sqm = someEnemy.squadManager;
                        if (sqm != null)
                        {
                            sqm.aliveEnemies.Remove(someEnemy);
                            ScoreScript.playerScore += someEnemy.score;

                            PlayerBase.SPECIAL += someEnemy.specialIncreaseVal;
                            if (p_owner.SHIPTYPE == ShipBase.ShipType.player)
                            {
                                (p_owner as PlayerScript).playerHUD.ValidateChanges();
                            }
                        }

                        float rand = Random.Range(0.0f, 100.0f);
                        if (rand <= someEnemy.dropChance)
                        {
                            if (someEnemy.powerupDrop == MissileType.Random || someEnemy.powerupDrop > MissileType.Bomb)
                            {
                                PickupContainer.GetPowerUp(Random.Range(0, 7), someEnemy.transform.position);
                            }
                            else
                            {

                                PickupContainer.GetPowerUp((int)someEnemy.powerupDrop, someEnemy.transform.position);

                            }
                        }

                        if (Explosion2)
                        {
                            Instantiate(Explosion2, someEnemy.transform.position, someEnemy.transform.rotation);
                        }

                        Destroy(someEnemy.gameObject);
                    }

                }
            }

            BombExplosion();
        }
        if (Explosion)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }
        if (myPool != null)
        {
            if (p_owner != null)
            {
                if (missleType == MissileType.Protective)
                {
                    p_owner.protectiveCount--;
                }
            }
            neareastEnemy = null;
            myPool.ReturnProjectile(this);
            this.gameObject.SetActive(false);
            p_active = false;
        }

    }

    public float rotRadius = 10.0f;
    float rotSpeed = 3.5f;
    float posX, posY, angle = 0.0f;

    private void Update()
    {
        if (isPaused == false)
        {

            if (p_active == true)
            {
                if (p_lifespan > 0)
                {
                    switch (missleType)
                    {


                        case MissileType.Protective:
                            {
                                if (p_owner)
                                {
                                    posX = p_owner.transform.position.x + Mathf.Cos(angle) * rotRadius / 2.0f;
                                    posY = p_owner.transform.position.y + Mathf.Sin(angle) * rotRadius;

                                    //Debug.Log("x= " + );
                                    transform.position = new Vector3(posX, posY, transform.position.z);
                                    angle = angle + Time.deltaTime * rotSpeed;
                                    if (angle >= 360.0f)
                                    {
                                        angle = 0.0f;
                                    }

                                    transform.LookAt(p_owner.transform);
                                    transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, -transform.localEulerAngles.y, transform.localEulerAngles.z);
                                }
                                else
                                {
                                    Despawn();
                                }

                                p_lifespan -= p_decrease * Time.deltaTime;
                            }
                            break;
                        case MissileType.Homing:
                            {
                                if (p_lifespan > 2.0f)
                                {
                                    if (neareastEnemy == null)
                                    {
                                        neareastEnemy = GetNearestEnemy();
                                    }
                                    if (neareastEnemy != null)
                                    {
                                        transform.position = Vector3.MoveTowards(transform.position, neareastEnemy.transform.position, p_defaultSpeed * Time.deltaTime);
                                        moveDirection = neareastEnemy.transform.position - transform.position;
                                        p_lifespan -= p_decrease * Time.deltaTime;
                                        transform.LookAt(neareastEnemy.transform);


                                    }
                                    else
                                    {
                                        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, p_defaultSpeed * Time.deltaTime);
                                        p_lifespan -= p_decrease * Time.deltaTime;
                                        transform.localEulerAngles = transform.localEulerAngles + p_updateRotation;
                                    }
                                }
                                else
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, p_defaultSpeed * Time.deltaTime);
                                    p_lifespan -= p_decrease * Time.deltaTime;
                                    transform.localEulerAngles = transform.localEulerAngles + p_updateRotation;

                                }
                            }
                            break;

                        case MissileType.Laser:
                            {

                                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, p_defaultSpeed * Time.deltaTime);
                                if (PlayerBase.usingSpecial == true)
                                    transform.position = new Vector3(p_owner.transform.position.x, transform.position.y, transform.position.z);
                                p_lifespan -= p_decrease * Time.deltaTime;
                                transform.localEulerAngles = transform.localEulerAngles + p_updateRotation;
                            }
                            break;
                        default:
                            {
                                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, p_defaultSpeed * Time.deltaTime);
                                p_lifespan -= p_decrease * Time.deltaTime;
                                transform.localEulerAngles = transform.localEulerAngles + p_updateRotation;
                            }
                            break;
                    }



                }
                else
                {

                    Despawn();
                }
            }
        }
    }



    public void SpawnCluster()
    {
        angle = 360.0f;
        if (p_owner != null)
        {

            for (int i = 0; i < 10; i++)
            {
                ProjectileBase defaultProjectile = myPool.GetProjectile(p_owner, MissileType.normal);

                posX = transform.position.x + Mathf.Cos(angle) * 2.0f;
                posY = transform.position.y + Mathf.Sin(angle) * 1.0f;
                defaultProjectile.transform.position = new Vector3(posX, posY, transform.position.z);
                angle -= 36.0f; ;
                defaultProjectile.transform.LookAt(transform.position);
                defaultProjectile.transform.localEulerAngles = new Vector3(-defaultProjectile.transform.localEulerAngles.x, -transform.localEulerAngles.y, transform.localEulerAngles.z);
                defaultProjectile.moveDirection = defaultProjectile.transform.position - transform.position;

            }
            SFXLibrary.PlaySmallExplosion();
        }
    }

    public void BombExplosion()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        SFXLibrary.PlaySmallExplosion();

    }

    private ShipBase GetNearestEnemy()
    {
        ShipBase neareast = null;
        ShipBase[] ships = GameObject.FindObjectsOfType<ShipBase>();

        for (int i = 0; i < ships.Length; i++)
        {
            ShipBase aShip = ships[i];
            if (aShip != p_owner && p_owner != null)
            {
                if (aShip.SHIPTYPE != p_owner.SHIPTYPE)
                {
                    if (neareast == null)
                    {
                        neareast = aShip;
                        continue;
                    }
                    else
                    {
                        if (Vector3.Distance(aShip.transform.position, transform.position) < Vector3.Distance(neareast.transform.position, transform.position))
                        {
                            neareast = aShip;
                        }
                    }

                }
            }
        }

        return neareast;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<ShipBase>())
        {

            ShipBase someShip = other.gameObject.GetComponent<ShipBase>();


            if (someShip.SHIPTYPE != p_owner.SHIPTYPE)
            {

                if (someShip.SHIPTYPE == ShipBase.ShipType.enemy)
                {
                    SFXLibrary.PlayEnemyHit();
                }
                else
                {
                    SFXLibrary.PlayPlayerHit();

                }
                someShip.TakeDamage(1);
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
                            ScoreScript.playerScore += anEnemy.score;
                            if (missleType != MissileType.Laser)
                            {
                                PlayerBase.SPECIAL += anEnemy.specialIncreaseVal;
                                if (p_owner.SHIPTYPE == ShipBase.ShipType.player)
                                {
                                    (p_owner as PlayerScript).playerHUD.ValidateChanges();
                                }
                            }
                            float rand = Random.Range(0.0f, 100.0f);
                            if (rand <= someShip.dropChance)
                            {
                                if (someShip.powerupDrop == MissileType.Random || someShip.powerupDrop > MissileType.Bomb)
                                {
                                    PickupContainer.GetPowerUp(Random.Range(0, 6), anEnemy.transform.position);
                                }
                                else
                                {

                                    PickupContainer.GetPowerUp((int)someShip.powerupDrop, anEnemy.transform.position);

                                }
                            }
                            SFXLibrary.PlayMediumExplosion();
                            if (Explosion2)
                            {
                                Instantiate(Explosion2, anEnemy.transform.position, anEnemy.transform.rotation);
                            }
                            Destroy(anEnemy.gameObject);
                        }
                    }
                    else if (someShip.SHIPTYPE == ShipBase.ShipType.player)
                    {

                        PlayerBase thePlayer = someShip.GetComponent<PlayerBase>();
                        if (thePlayer != null)
                        {
                            if (thePlayer.LIVES > 1)
                            {
                                thePlayer.LIVES--;

                            }
                            else
                            {
                                thePlayer.LIVES--;
                                GameManager gm = GameObject.FindObjectOfType<GameManager>();
                                if (gm != null)
                                {
                                    gm.GameOver();
                                }
                            }

                        }
                    }
                    else
                    {
                        OptionsPause.LoadMainMenu();
                    }
                }
                else
                {
                    someShip.transform.position += (someShip.transform.position - transform.position) * 50 * Time.fixedDeltaTime;
                }
                if (missleType != MissileType.Laser)
                    Despawn();
            }
        }
        else if (other.gameObject.GetComponent<ProjectileBase>())
        {

            ProjectileBase someProjectile = other.gameObject.GetComponent<ProjectileBase>();
            if (someProjectile.p_owner.SHIPTYPE != p_owner.SHIPTYPE && missleType != MissileType.Laser)
            {

                Despawn();
            }
        }

    }


    public virtual void Pause()
    {
        isPaused = true;
    }

    public virtual void Resume()
    {
        isPaused = false;
    }
}
