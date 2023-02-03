using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SpawnItemStatusText : MonoBehaviour, ISpawnText
{
    private TextMeshPro itemStatusText;

    public void InitializeItem(Vector3 positionToJump, string textToTransfer)
    {
        itemStatusText = transform.GetComponent<TextMeshPro>();

        Vector3 spawnTextScale = new Vector3(0.3f,0.3f,0.3f);

        itemStatusText.text = textToTransfer;

        transform.localScale = spawnTextScale;
        
        transform.DOMove(positionToJump, 0.5f).OnComplete(()=>{
            Destroy(transform.gameObject);
        });  
    }
}
