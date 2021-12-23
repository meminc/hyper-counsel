using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    [SerializeField] private GameObject _policeGo;
    public void ActivatePolice()
    {
        _policeGo.SetActive(true);
    }
}
