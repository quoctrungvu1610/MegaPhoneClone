using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [Header ("UI references :")]
   [SerializeField] private Image uiFillImage ;
   [SerializeField] private TextMeshProUGUI uiStartText ;
   [SerializeField] private TextMeshProUGUI uiEndText ;

   [Header ("Player & Endline references :")]
   [SerializeField] private Transform playerTransform ;
   [SerializeField] private Transform endLineTransform ;

   private Vector3 endLinePosition ;
   private float fullDistance ;

   private void Start () {
      endLinePosition = endLineTransform.position ;
      fullDistance = GetDistance () ;
   }

   private float GetDistance ()
   {
      return (endLinePosition - playerTransform.position).sqrMagnitude ;
   }


   private void UpdateProgressFill (float value)
   {
      uiFillImage.fillAmount = value ;
   }


   private void Update ()
   {
      if (playerTransform.position.z <= endLinePosition.z)
      {
         float newDistance = GetDistance () ;
         float progressValue = Mathf.InverseLerp (fullDistance, 0f, newDistance) ;

         UpdateProgressFill (progressValue) ;
      }
   }
}
