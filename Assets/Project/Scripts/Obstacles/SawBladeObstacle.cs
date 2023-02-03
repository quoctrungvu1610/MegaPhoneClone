using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeObstacle : MonoBehaviour, IRotateable, IDamageable
{
    [SerializeField] private float rotateSpeed = 5;
    [SerializeField] private int damage = 2;
    private GameObject sawBladeObstacle;

    private void Start()
    {
        sawBladeObstacle = this.gameObject;
    }
    public void RotateObstacleObject()
    {
        sawBladeObstacle.transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }

    private void Update()
    {
        RotateObstacleObject();
    }

    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        client.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ICanTakeDamage client;
            client = other.gameObject.GetComponent<ICanTakeDamage>();

            HandleDamage(damage,client);
        }    
    }
}
