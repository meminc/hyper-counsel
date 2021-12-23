using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float animSpeed = .05f;
    
    public void WriteText(String str)
    {
        _text.text = "";
        StartCoroutine(TextAnim(str, animSpeed));
    }

    IEnumerator TextAnim(String str, float animSpeed)
    {
        foreach (var ch in str.ToCharArray())
        {
            _text.text += ch;
            yield return new WaitForSeconds(animSpeed);
            
        }
    }
    
}
