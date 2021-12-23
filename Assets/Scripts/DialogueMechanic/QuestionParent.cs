using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionParent : MonoBehaviour
{
    [SerializeField] private Animator bubbleAnimator;
    
    public void OpenBubble()
    {
        bubbleAnimator.SetBool("Open", true);
        bubbleAnimator.SetBool("Close", false);
    }

    public void CloseBubble()
    {
        bubbleAnimator.SetBool("Open", false);
        bubbleAnimator.SetBool("Close", true);
    }
}
