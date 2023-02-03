using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnItem : MonoBehaviour, ISpawnItem
{
    public void InitializeItem(Vector3 positionToJump)
    {
        Vector3 itemPickupScale = new Vector3(13,13,13);

        transform.localScale = new Vector3(0,0,0);
        transform.DOJump(positionToJump,Random.Range(2,7),1,Random.Range(0.4f,0.6f));
        transform.DOScale(itemPickupScale,0.2f);  
    }
}
