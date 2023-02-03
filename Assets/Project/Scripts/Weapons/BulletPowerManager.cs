using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerManager : MonoBehaviour
{
    [SerializeField] private Transform bulletsHoldingObject;
    public static BulletPowerManager Instance;
    public GameObject checkKeyTarget = null;
    public int increaseBulletPowerCheckPoint = 5;
    public int increaseBulletPowerPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void IncreaseBulletPower()
    {
        increaseBulletPowerCheckPoint += 20;
        for(int i = 0; i <= 49; i++)
        {
            Bullet bullet;
            bullet = bulletsHoldingObject.GetChild(i).transform.GetComponent<Bullet>();
            bullet.DoubleBulletPower();
        }
    }

    public void ResetBulletPower()
    {
        increaseBulletPowerCheckPoint = 5;
        increaseBulletPowerPoint = 0;
        for(int i = 0; i <= 49; i++)
        {
            Bullet bullet;
            bullet = bulletsHoldingObject.GetChild(i).transform.GetComponent<Bullet>();
            bullet.SetBulletPower(1);
        }
    }

    public void Check()
    {
        increaseBulletPowerPoint += 1;

        if(increaseBulletPowerPoint == increaseBulletPowerCheckPoint)
        {
            IncreaseBulletPower();
        }
    }

}
