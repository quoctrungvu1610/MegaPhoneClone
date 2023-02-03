using UnityEngine;

public interface ISpawnItem
{
    public void InitializeItem(Vector3 positionToJump);  
}

public abstract class Factory : MonoBehaviour
{
    public abstract ISpawnItem GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJump);
}

public interface ISpawnText
{
    public void InitializeItem(Vector3 positionToJump, string textToTransfer);
}

public abstract class TextFactory : MonoBehaviour
{
    public abstract ISpawnText GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJump, string textToTransfer);
}