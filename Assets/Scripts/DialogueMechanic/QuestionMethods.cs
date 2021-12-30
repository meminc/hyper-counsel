using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMethods : MonoBehaviour
{
    [SerializeField] private DialogueScript _dialogueScript;
    [SerializeField] private ButtonParent _buttonParent;
    [SerializeField] private QuestionParent _questionParent;

    [SerializeField] private TextWriter questionTextWriter;
    [SerializeField] private TextWriter[] answerTextWriters;
    [SerializeField] private PoliceController police;
    
    private int currentLevelIndex = 0;

    private CharacterController _characterController;

    private String questionStr;
    private List<String> answers = new List<string>();
    private int index = 1;
    private LevelEndCanvas _endCanvas;

    private void Start()
    {
        _buttonParent.DeactivateButtons();

        _characterController = FindObjectOfType<CharacterController>();

        _endCanvas = FindObjectOfType<LevelEndCanvas>();
    }

    public void StartConversation()
    {
        questionStr = _dialogueScript.ReturnQuestionStr(index);
        answers = _dialogueScript.ReturnAnswers(index);
        StartCoroutine(ConversationUpdate());
    }

    public void BubbleAnim(bool open)
    {
        if (open)
        {
            _questionParent.OpenBubble();
        }
        else
        {
            _questionParent.CloseBubble();
        }
    }
    
    public void ButtonAnim(bool open)
    {
        if (open)
        {
            _buttonParent.AppearButton();
        }
        else
        {
            _buttonParent.DisappearButton();
        }
    }

    public int getIndex()
    {
        return index;
    }

    IEnumerator ConversationUpdate()
    {
        yield return new WaitForSeconds(.5f);
        questionTextWriter.WriteText(questionStr);
        answerTextWriters[0].WriteText("");
        answerTextWriters[1].WriteText("");
        yield return new WaitForSeconds(.5f);
        _buttonParent.RemoveSelection();
        yield return new WaitForSeconds(.5f);
        answerTextWriters[0].WriteText(answers[0]);
        yield return new WaitForSeconds(.5f);
        answerTextWriters[1].WriteText(answers[1]);

        yield return new WaitForSeconds(.75f);    
        _buttonParent.ActivateButtons();
        
        ButtonAnim(true);
    }

    public void CheckAnswer(String _answer, int _id)
    {
        _characterController.Listen();
        
        _buttonParent.DeactivateButtons();

        StartCoroutine(CloseButtons());
        
        _buttonParent.ShowSelection(_id);

        if (_answer.Contains("Succ"))
        {
            LevelSuccess();
            StartCoroutine(MethodsAfterLevelEnd(true));
            return;
        }
        if (_answer.Contains("Fail"))
        {
            LevelFailure();
            StartCoroutine(MethodsAfterLevelEnd(false));
            return;
        }

        if (_answer.Contains("Pol"))
        {
            police.ActivatePolice();
            StartCoroutine(MethodsAfterPoliceCome());
            return;
        }

        index = _dialogueScript.ReturnNextQuesitonIndex(index, _id);
        NextQuestion();
    }

    IEnumerator MethodsAfterPoliceCome()
    {
        BubbleAnim(false);
        _characterController.Angry();
        yield return new WaitForSeconds(4);
        _endCanvas.StartAnim(false);
    }

    IEnumerator MethodsAfterLevelEnd(bool _success)
    {
        yield return new WaitForSeconds(2);
        _endCanvas.StartAnim(_success);
    }

    IEnumerator CloseButtons()
    {
        yield return new WaitForSeconds(.2f);

        ButtonAnim(false);
    }

    void NextQuestion()
    {
        answers.Clear();
        questionStr = _dialogueScript.ReturnQuestionStr(index);
        answers = _dialogueScript.ReturnAnswers(index);
        StartCoroutine(ConversationUpdate());

    }

    void LevelSuccess()
    {
        BubbleAnim(false);
        _characterController.Happy();
        DialogueManager.instance.IncreaseMoney();
    }

    void LevelFailure()
    {
        BubbleAnim(false);
        _characterController.Angry();
    }
}
