using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform rightHandTransform;
    private Vector3 offset;

    private Transform weaponTransform;
    
    private void Start()
    {
        weaponTransform = this.transform;
        offset = new Vector3(0, 0, -0.3f);
    }

    private void Update()
    {
        weaponTransform.position = rightHandTransform.position + offset;
    }
}
