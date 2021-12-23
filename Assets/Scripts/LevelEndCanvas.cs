using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LevelEndCanvas : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StartAnim(bool _success)
    {
        if (_success)
        {
            animator.SetTrigger("Success");
        }
        else
        {
            animator.SetTrigger("Failure");
        }
    }

    public void Change()
    {
        animator.SetTrigger("Change");
    }

    public void LevelEndButton(bool _success)
    {
        DialogueManager.instance.NextLevel(_success);
    }
}
