using UnityEngine;
using DG.Tweening;
using System.Collections;
public class PlayerStackMechanic : MonoBehaviour
{
    [SerializeField] GameObject bulletSpawner;
    [SerializeField] private GameObject[] bones;
    [SerializeField] private Transform itemDropPosition;
    private bool isLoadingAnimation = false;
    public int numberOfItemHolding = 1;
    private int checkKey;
    public bool isCollidedWithPickupItem = false;
    private Transform objectHolding;
    private Transform playerStackMechanicTransform;
    private SpawnItemFactory spawnItemFactory;
    public static PlayerStackMechanic Instance;
    public int NumberOfItemHolding => numberOfItemHolding;
    public bool IsLoadingAnimation => isLoadingAnimation;
    
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
        spawnItemFactory = SpawnItemFactory.Instance;
        playerStackMechanicTransform = this.transform;
        
    }

    private void Update()
    {
        Debug.Log("Check key: "+ checkKey + " " + "Number of itemHolding: " + numberOfItemHolding);
        objectHolding = bones[numberOfItemHolding].transform;
        bulletSpawner.transform.position = objectHolding.transform.position;
    }

    public IEnumerator ReScaleBehavior()
    {
        Vector3 scaleBigSize = new Vector3(25f, 25f, 25f);
        Vector3 scaleSmallSize = new Vector3(20f, 20f, 20f);

        for(int i = 0; i <= numberOfItemHolding; i++)
        {
            bones[i].transform.GetChild(0).transform.DOScale(scaleBigSize,0.1f).OnComplete(()=>{
                bones[i].transform.GetChild(0).transform.DOScale(scaleSmallSize,0.1f);
            });
            yield return new WaitForSeconds(0.1f);
        }
        objectHolding.GetChild(0).gameObject.SetActive(true); 
        numberOfItemHolding += 1;
    }

    public IEnumerator AddMoreItemsToBones()
    {
        for(int i = 1; i < numberOfItemHolding; i++ )
        {
            if(bones[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                yield return null;
            }
            else
            {
                bones[i].transform.GetChild(0).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public IEnumerator SubtractItemsFromBones()
    {
        float zOffset = 1;

        for(int i = checkKey - 1; i >= numberOfItemHolding; i-- )
        {
            float randomXPos = Random.Range(-2f, 2f);
            float randomZPos = Random.Range(15f, 30f);
            
            bones[i].transform.GetChild(0).gameObject.SetActive(false);
            spawnItemFactory.GetSpawnItem(itemDropPosition.position ,playerStackMechanicTransform.position + new Vector3(randomXPos, 1.2f , 0)); 
            yield return new WaitForSeconds(0.05f);   
            zOffset += 5;
        }
    }

    public IEnumerator CalculateNumberOfItemHolding(CalculationTypes calculationTypes, int calculationNumber)
    {
        checkKey = numberOfItemHolding;

        switch(calculationTypes)
        {
            case CalculationTypes.Add:
                numberOfItemHolding += calculationNumber;
                break;
            case CalculationTypes.Divide:
                numberOfItemHolding /= calculationNumber;
                if(numberOfItemHolding <= 1) numberOfItemHolding = 1;
                break;
            case CalculationTypes.Minus:
                numberOfItemHolding -= calculationNumber;
                if(numberOfItemHolding <= 1) numberOfItemHolding = 1;
                break;
            case CalculationTypes.Multiply:
                numberOfItemHolding *= calculationNumber;
                break;
        }
        yield return new WaitForSeconds(0.1f);
        AdjustBoneItems(checkKey);
    }

    private void AdjustBoneItems(int checkKey)
    {
        if(checkKey < numberOfItemHolding)
        {
            StartCoroutine(AddMoreItemsToBones());
        }

        else if(checkKey > numberOfItemHolding)
        {
            StartCoroutine(SubtractItemsFromBones());
        }     
    }

    public void RemoveItemFromBones(int numberToRemove)
    {
        checkKey = numberOfItemHolding;

        if(numberOfItemHolding <= numberToRemove)
        {
            numberOfItemHolding = 1;
            StartCoroutine(SubtractItemsFromBones());
        }
        else
        {
            numberOfItemHolding -= numberToRemove;
            StartCoroutine(SubtractItemsFromBones());
        } 
    }

    public void FindBrokenSpeaker()
    {
        for(int i = 1; i < numberOfItemHolding; i++)
        {
            if(bones[i].transform.GetChild(0).gameObject.activeSelf == false)
            {
                int boneToSubstract = numberOfItemHolding - i;
                RemoveItemFromBones(boneToSubstract);
            }
        }
    }
}
