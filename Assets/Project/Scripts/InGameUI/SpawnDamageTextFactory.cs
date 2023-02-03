using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageTextFactory : TextFactory
{
    [SerializeField] private SpawnDamageText spawnItemPrefab;
    [SerializeField] private Transform spawnDamageTextHoldingObject;

    public static SpawnDamageTextFactory Instance;

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

    public override ISpawnText GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJump, string textToTransfer)
    {
        GameObject instance = Instantiate(spawnItemPrefab.gameObject, spawnPosition, Quaternion.identity, spawnDamageTextHoldingObject);
        
        SpawnDamageText spawnDamageText = instance.GetComponent<SpawnDamageText>();

        spawnDamageText.InitializeItem(positionToJump, textToTransfer);

        return spawnDamageText;
    }
}
