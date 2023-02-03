using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] Transform bulletsRoot;
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake() 
    {
        SharedInstance = this;
    }

    private void Start() 
    {
        AddBulletToPool();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }   
        return null; 
    }

    public void AddBulletToPool()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < amountToPool; i++)
        {
            Bullet bullet;
            tmp = Instantiate(objectToPool,bulletsRoot);
            bullet = tmp.gameObject.GetComponent<Bullet>();
            bullet.SetBulletPower(1);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}
