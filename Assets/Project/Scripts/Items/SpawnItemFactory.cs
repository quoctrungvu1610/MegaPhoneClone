using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemFactory : Factory
{
    [SerializeField] private SpawnItem spawnItemPrefab;
    [SerializeField] private Transform spawnItemHoldingObject;

    public static SpawnItemFactory Instance;

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

    public override ISpawnItem GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJump)
    {
        GameObject instance = Instantiate(spawnItemPrefab.gameObject, spawnPosition, Quaternion.identity, spawnItemHoldingObject);
        
        SpawnItem spawnItem = instance.GetComponent<SpawnItem>();

        spawnItem.InitializeItem(positionToJump);

        return spawnItem;
    }
}
