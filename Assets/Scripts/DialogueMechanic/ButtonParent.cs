using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParent : MonoBehaviour
{
    public Button[] buttons;

    public Animator[] buttonAnimators;
    

    public void ActivateButtons()
    {
        foreach (var btn in buttons)
        {
            btn.interactable = true;
        }
    }

    public void DeactivateButtons()
    {
        foreach (var btn in buttons)
        {
            btn.interactable = false;
        }
    }

    public void ShowSelection(int _id)
    {
        var colors = buttons[_id - 1].colors;
        colors.normalColor = Color.green;
        colors.selectedColor = Color.green;
        colors.disabledColor = Color.green;
        buttons[_id - 1].colors = colors;
    }

    public void RemoveSelection()
    {
        foreach (var btn in buttons)
        {
            var colors =btn.colors;
            colors.normalColor = Color.white;
            colors.selectedColor = Color.white;
            colors.disabledColor = Color.white;
            btn.colors = colors;
        }
    }
    
    public void DisappearButton()
    {
        StartCoroutine(Dissappear());
    }

    public void AppearButton()
    {
        StartCoroutine(AppearButtons());
    }

    IEnumerator AppearButtons()
    {
        foreach (var btnAnm in buttonAnimators)
        {
            btnAnm.SetBool("Open", true);
            btnAnm.SetBool("Close", false);
            yield return new WaitForSeconds(.4f);
        }
    }
    
    IEnumerator Dissappear()
    {
        foreach (var btnAnm in buttonAnimators)
        {
            btnAnm.SetBool("Open", false);
            btnAnm.SetBool("Close", true);
            yield return new WaitForSeconds(.4f);
        }
    }
}
