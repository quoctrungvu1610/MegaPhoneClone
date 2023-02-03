using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpeaker : MonoBehaviour, ICanTakeDamage
{
    private GameObject boneSpeaker;
    private PlayerStackMechanic playerStackMechanic;
    public bool isCollided;
    
    private void Start()
    {
        isCollided = false;
        boneSpeaker= this.gameObject;
        playerStackMechanic = PlayerStackMechanic.Instance;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //playerStackMechanic.RemoveItemFromBones(1);   
        boneSpeaker.SetActive(false);
        playerStackMechanic.FindBrokenSpeaker();
    }
}
