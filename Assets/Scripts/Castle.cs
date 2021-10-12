using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

   [SerializeField] private Transform _gate;
   
   public void OpenGate()
   {
      _gate.gameObject.SetActive(false);
   }
}
