using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour, IMoveable, IDamageable, IParticleSystems
{
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private ParticleSystem collideParticleSystem;
    [SerializeField] private ParticleSystem collideParticleSystem2;
    [SerializeField] private int bulletPower;
    [SerializeField] private int bulletDistance = 100;
    private Transform player;
    private Transform bulletTransform;
    private Transform  spawnParticleHoldingObject;
    private SpawnDamageTextFactory spawnDamageTextFactory;
    private BulletPowerManager bulletPowerManager;
    
    private void Start()
    {
        bulletTransform = this.transform;
        spawnDamageTextFactory = SpawnDamageTextFactory.Instance;
        bulletPowerManager = BulletPowerManager.Instance;
        spawnParticleHoldingObject = GameObject.FindGameObjectWithTag("ParticleHoldingObject").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        HandleMovement();
    }

    public void SetBulletPower(int power)
    {
        bulletPower = power;
    }

    public void DoubleBulletPower()
    {
        bulletPower *= 2;
    }

    public void HandleMovement()
    {
        bulletTransform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * (bulletSpeed + PlayerStackMechanic.Instance.NumberOfItemHolding));
        if(Vector3.Distance(player.transform.position, bulletTransform.transform.position) > bulletDistance)
        {
            DeactiveBullet();
        }
    }
    
    private void DeactiveBullet()
    {
        bulletTransform.localScale = new Vector3(0.0f,0.0f,0.0f);
        bulletTransform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("BigWall"))
        {
 
            if(bulletPowerManager.checkKeyTarget == null)
            {
                bulletPowerManager.checkKeyTarget = other.gameObject;
            }
            
            Vector3 bulletCollideParticleSystemScale = new Vector3(2,2,2);
            int randomDamageTextJumpPosition = Random.Range(4,8);

            ICanTakeDamage client;
            client = other.gameObject.GetComponent<ICanTakeDamage>();

            if(other.gameObject != bulletPowerManager.checkKeyTarget)
            {
                bulletPowerManager.ResetBulletPower();
                bulletPowerManager.checkKeyTarget = other.gameObject;
            }
            
            bulletPowerManager.Check();
            //TakeDamage
            HandleDamage(bulletPower,client);

            //Spawn ParticleSystems
            InstantiateParticleSystems(collideParticleSystem, bulletTransform.position, bulletCollideParticleSystemScale);
            InstantiateParticleSystems(collideParticleSystem2, bulletTransform.position, bulletCollideParticleSystemScale);

            //Spawn DamageText
            spawnDamageTextFactory.GetSpawnItem(bulletTransform.position, bulletTransform.position + new Vector3(0,randomDamageTextJumpPosition,0), bulletPower.ToString());

            //Deactive the bullet
            DeactiveBullet();
        } 
        
        if(other.gameObject.CompareTag("DestroyBulletWall"))
        {
            //Deactive the bullet
            DeactiveBullet();
        }
        
    }

    public void HandleDamage(int damage, ICanTakeDamage takeDamegeObject)
    {
        takeDamegeObject.TakeDamage(damage);
    }

    public void InstantiateParticleSystems(ParticleSystem particleSystem, Vector3 instantiatePosition, Vector3 instantiateScale)
    {
        ParticleSystem particle =  Instantiate(particleSystem, spawnParticleHoldingObject);

        particle.transform.localPosition = instantiatePosition;
        particleSystem.transform.localScale = instantiateScale;

        Destroy(particle.gameObject,1f);
    }
}
