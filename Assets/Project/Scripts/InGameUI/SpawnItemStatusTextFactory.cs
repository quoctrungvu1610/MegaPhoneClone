using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemStatusTextFactory : TextFactory
{
    [SerializeField] private SpawnItemStatusText spawnItemStatusTextPrefab;
    [SerializeField] private Transform spawnItemStatusTextHoldingObject;

    public static SpawnItemStatusTextFactory Instance;

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
        GameObject instance = Instantiate(spawnItemStatusTextPrefab.gameObject, spawnPosition, Quaternion.identity, spawnItemStatusTextHoldingObject);
        
        SpawnItemStatusText spawnItemStatusText = instance.GetComponent<SpawnItemStatusText>();

        spawnItemStatusText.InitializeItem(positionToJump, textToTransfer);

        return spawnItemStatusText;
    }
}
