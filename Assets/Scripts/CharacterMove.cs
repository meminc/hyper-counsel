using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateSpeed = 5f;

    private Animator _animatorController;
    
    private bool isMoving = false;

    private bool isRotating = false;

    private void Start()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == "Target")
            {
                target = transform.parent.GetChild(i);
                isMoving = true;
                break;
            }
        }

        _animatorController = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            Vector3 targetDirection = target.position - transform.position;
            
            float singleStep = rotateSpeed * Time.deltaTime;
            
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            
            transform.rotation = Quaternion.LookRotation(newDirection);
            
            if (transform.position == target.position)
            {
                isMoving = false;

                isRotating = true;
                
                _animatorController.SetTrigger("Sit");
                
            }
        }

        if (isRotating)
        {
            float singleStep = rotateSpeed * Time.deltaTime;
            
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(0,0,1), singleStep, 0.0f);
                
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (Mathf.Abs(transform.eulerAngles.y) < 1f || Mathf.Abs(transform.eulerAngles.y) > 358)
            {
                isRotating = false;

                DialogueManager.instance.StartConversationWhenCharacterSit();
            }
        }
    }
}
