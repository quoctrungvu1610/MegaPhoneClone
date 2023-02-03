using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SpawnDamageText : MonoBehaviour, ISpawnText
{
    private TextMeshPro damageText;
    private int damage = 0;
    Vector3 spawnTextScale = new Vector3(1.5f,1.5f,1.5f);
    
    public void InitializeItem(Vector3 positionToJump, string textToTransfer)
    {
        // if(int.Parse(textToTransfer) != damage)
        // {
        //     if(int.Parse(textToTransfer) > damage)
        //     {
        //         spawnTextScale += new Vector3(1,1,1);
        //         damage = int.Parse(textToTransfer);
        //     }
            
        // }

        damageText = transform.GetComponent<TextMeshPro>();

        damageText.text = "-" + textToTransfer;

        transform.localScale = new Vector3(0,0,0);
        
        transform.DOMove(positionToJump, 0.5f);
        transform.DOScale(spawnTextScale,0.2f).OnComplete(()=>{
            Destroy(transform.gameObject,0.1f);
        });  
    }
}
