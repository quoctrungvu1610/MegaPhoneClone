using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberOfSpeaker;
    [SerializeField] TextMeshProUGUI bonusBulletNumber;
    [SerializeField] GameObject bulletSpawner;
    void Start()
    {
        
    }

    void Update()
    {  
      
            numberOfSpeaker.text = PlayerStackMechanic.Instance.NumberOfItemHolding.ToString();
            bonusBulletNumber.text = BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec.ToString();
        
    }
}
