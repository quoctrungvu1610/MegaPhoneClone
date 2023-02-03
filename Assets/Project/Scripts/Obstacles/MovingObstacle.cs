using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObstacle : MonoBehaviour, IMoveable
{
    private float endPositionValue;
    private GameObject movingObstacle;
    private GameObject obstacle;
    private GameObject rail;
    private float firstPostionValue;
    private float endValueOffset = 3.5f;
    
    private void Start()
    {
        movingObstacle = this.gameObject;

        obstacle = movingObstacle.transform.GetChild(0).gameObject;
        rail = movingObstacle.transform.GetChild(1).gameObject;
        
        firstPostionValue = obstacle.transform.position.x;
        endPositionValue = firstPostionValue - rail.transform.localScale.x + endValueOffset;
        
        HandleMovement();
    }

    public void HandleMovement()
    {
        obstacle.transform.DOMoveX(endPositionValue, 2f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
}
