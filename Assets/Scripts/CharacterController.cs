using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private GameObject[] charactersGo;

    [SerializeField] private GameObject bubble;

    private GameObject _activeCharacter;

    private Animator _animatorController;

    private Animator bubbleAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ActivateCharacter(PlayerPrefs.GetInt("CURRENT_LEVEL_INDEX", 1) - 1);

        bubbleAnimator = bubble.GetComponent<Animator>();
    }
    

    public void ActivateCharacter(int id)
    {
        _activeCharacter = charactersGo[id];
        
        _activeCharacter.SetActive(true);

        _animatorController = _activeCharacter.GetComponentInChildren<Animator>();

        //_activeCharacter.transform.position = _movePoints[0].position;
    }

    public void Speak()
    {
        _animatorController.SetTrigger("Speak");
    }

    public void Listen()
    {
        _animatorController.SetTrigger("Listen");
    }

    public void Angry()
    {
        _animatorController.SetTrigger("Angry");
    }

    public void Happy()
    {
        _animatorController.SetTrigger("Happy");
    }

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
