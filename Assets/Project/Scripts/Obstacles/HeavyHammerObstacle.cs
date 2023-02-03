using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyHammerObstacle : MonoBehaviour, IDamageable
{

    [SerializeField] private int damage = 2;
    private GameObject sawBladeObstacle;

    private void Start()
    {
        sawBladeObstacle = this.gameObject;
    }

    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        client.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BoneSpeaker"))
        {
            ICanTakeDamage client;
            client = other.gameObject.GetComponent<ICanTakeDamage>();

            HandleDamage(damage,client);
        }    
    }
}
