using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IParticleSystems, ICanTakeDamage
{
    [SerializeField] private Transform collideItemParticleSystemSpawnPosition;
    [SerializeField] private ParticleSystem collideItemParticleSystem;
    [SerializeField] private Transform collideGateParticleSystemSpawnPositions;
    [SerializeField] private ParticleSystem collideGateParticleSystems;
    [SerializeField] GameObject weapon;
    //[SerializeField] GameObject bulletSpawnerGameObject;
    private Transform  spawnParticleHoldingObject;
    private PlayerStackMechanic playerStackMechanic;
    private PlayerMoveForward playerMoveForward; 
    private SpawnItemStatusTextFactory spawnItemStatusTextFactory;
    private BulletSpawner bulletSpawner;
    private Transform playerTransform;
    private Vector3 spawnTextPosition;
    private Vector3 spawnTextOffset;
    private PlayerStat playerStat;
    private Animator animator;

    private void Awake() {
        playerStat = PlayerStat.Moving;
    }

    private void Start() 
    {
        playerStackMechanic = PlayerStackMechanic.Instance;  
        playerMoveForward = PlayerMoveForward.Instance;
        bulletSpawner = BulletSpawner.Instance;
        spawnItemStatusTextFactory = SpawnItemStatusTextFactory.Instance;

        playerTransform = this.transform;
        animator = playerTransform.GetComponent<Animator>();

        spawnParticleHoldingObject = GameObject.FindGameObjectWithTag("ParticleHoldingObject").transform;
    }

    private void Update()
    {
        // switch (playerStat)
        // {
        //     case PlayerStat.Idle:
        //         PlayerIdleState();
        //         break;
        //     case PlayerStat.Moving:
        //         PlayerMovingState();
        //         break;
        //     case PlayerStat.Winning:
        //         PlayerWinningState();
        //         break;
        // }
        playerMoveForward.HandleMovement();
    }

    private void PlayerIdleState()
    {
        playerMoveForward.speed = 0;
    
        if(Input.GetMouseButton(0))
        {
            animator.SetBool("IsMoving", true);
            playerStat = PlayerStat.Moving;
            weapon.SetActive(true);
            //bulletSpawnerGameObject.SetActive(true);
        }
    }

    private void PlayerMovingState()
    {
        playerMoveForward.speed = 10;
    }

    private void PlayerWinningState()
    {
        //playerMoveForward.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.CompareTag("EndPoint"))
        // {
        //     playerStat = PlayerStat.Winning;
        // }
        
        if(PlayerStackMechanic.Instance.IsLoadingAnimation == false)
        {
            if (other.CompareTag("Item"))
            {
                Vector3 collideItemParticleSystemScale = new Vector3(2,2,2);

                IPickable iPickable;
                iPickable = other.GetComponent<IPickable>();

                spawnTextPosition = playerTransform.position + new Vector3(0,3,5);
                spawnTextOffset = spawnTextPosition + new Vector3(0,1,0);

                InstantiateParticleSystems(collideItemParticleSystem, collideGateParticleSystemSpawnPositions.position, collideItemParticleSystemScale);
                spawnItemStatusTextFactory.GetSpawnItem(spawnTextPosition, spawnTextOffset, "+1");

                if(iPickable.IsAlreadyCollected == false)
                {
                    playerStackMechanic.isCollidedWithPickupItem = true;
                    iPickable.HandlePickItem();
                    StartCoroutine(playerStackMechanic.ReScaleBehavior());
                }   
            }

            else if(other.CompareTag("CalculationGate"))
            {
                IPlayAnimation client;
                client = other.gameObject.GetComponent<IPlayAnimation>();

                Vector3 collideGateParticalSystemScale = new Vector3(3,3,3);

                CalculationGate calculationGate;
                calculationGate = other.gameObject.GetComponent<CalculationGate>();

                spawnTextPosition = playerTransform.position + new Vector3(0,3,5);
                spawnTextOffset = spawnTextPosition + new Vector3(0,1,0);
                
                InstantiateParticleSystems(collideGateParticleSystems, collideItemParticleSystemSpawnPosition.position, collideGateParticalSystemScale);
                client.PlayAnimation();

                switch(calculationGate.objectToApplyCalculation)
                {
                    case ObjectToApplyCalculation.Speaker:
                        spawnItemStatusTextFactory.GetSpawnItem(spawnTextPosition, spawnTextOffset, " + " + calculationGate.numberToCalculate.ToString() + " Speaker ");
                        StartCoroutine(playerStackMechanic.CalculateNumberOfItemHolding(calculationGate.calculationType, calculationGate.numberToCalculate));
                        break;

                    case ObjectToApplyCalculation.Bullet:
                    spawnItemStatusTextFactory.GetSpawnItem(spawnTextPosition, spawnTextOffset, " + " + calculationGate.numberToCalculate.ToString() + " Bullet ");
                        calculationGate.HandleCalculation();
                        //bulletSpawner.bonusNumberOfBulletSpawnPerSec, calculationGate.numberToCalculate
                        break;
                }
            }
        }     
    }

    public void InstantiateParticleSystems(ParticleSystem particleSystem, Vector3 instantiatePosition, Vector3 instantiateScale)
    {
        ParticleSystem particle =  Instantiate(particleSystem, spawnParticleHoldingObject);

        particle.transform.localPosition = instantiatePosition;
        particleSystem.transform.localScale = instantiateScale;

        Destroy(particle.gameObject,2f);
    }

    public void TakeDamage(int damage)
    {
        playerStackMechanic.RemoveItemFromBones(damage);
    }
}
