using UnityEngine;

public class PlayerMoveForward : MonoBehaviour,IMoveable
{
    public float speed;
    private Transform playerRootTransform;
    public static PlayerMoveForward Instance;

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
        
    private void Start() 
    {
        playerRootTransform = this.transform;
    }
    
    public void HandleMovement()
    {
        playerRootTransform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
    }

}
