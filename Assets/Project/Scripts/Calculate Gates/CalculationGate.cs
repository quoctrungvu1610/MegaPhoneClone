using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CalculationGate : MonoBehaviour, IPlayAnimation
{
    [SerializeField] public CalculationTypes calculationType;
    [SerializeField] public ObjectToApplyCalculation objectToApplyCalculation;
    [SerializeField] public int numberToCalculate;

    private GameObject collidePart;
    private TextMeshPro calculationText;
    private TextMeshPro topText;
    private GameObject calculationWall;

    private void Awake()
    {

    }

    private void Start()
    {
        calculationWall = this.gameObject;

        topText =  calculationWall.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>();

        calculationText =  calculationWall.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshPro>(); 

        calculationText.text = DisplayGalculationText(calculationType) + numberToCalculate.ToString();
        
        topText.text = objectToApplyCalculation.ToString();

        //play gate animation

        collidePart = calculationWall.transform.GetChild(0).gameObject;
    }

    public void HandleCalculation()
    {
        switch(calculationType)
        {
            case CalculationTypes.Add:
                BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec += numberToCalculate;
                break;
            case CalculationTypes.Divide:
                BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec /= numberToCalculate;
                if(BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec <= 1)
                {
                    BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec = 1;
                } 
                break;
            case CalculationTypes.Minus:
                BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec -= numberToCalculate;
                if(BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec <= 1)
                {
                    BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec = 1;
                } 
                break;
            case CalculationTypes.Multiply:
                BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec *= numberToCalculate;
                break;
        }
    }

    private string DisplayGalculationText(CalculationTypes calculationTypes)
    {
        string calculationText = "";
        switch(calculationType)
        {
            case CalculationTypes.Add:
                calculationText = "+";
                break;
            case CalculationTypes.Divide:
                calculationText = "/";
                break;
            case CalculationTypes.Minus:
                calculationText = "-";
                break;
            case CalculationTypes.Multiply:
                calculationText = "x";
                break;
        }
        return calculationText;
    }

    public void PlayAnimation()
    {
        collidePart.transform.DOMoveY(transform.localPosition.y + 2.5f,0.2f).OnComplete(()=>{
            collidePart.transform.DOMoveY(-3.5f, 0.3f);
        });
    }
}
