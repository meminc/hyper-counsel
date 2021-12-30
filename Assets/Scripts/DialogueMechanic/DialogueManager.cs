using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region INSTANCE
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
    }
    #endregion
    
    
    [SerializeField] private DialogueScript _dialogueScript;

    [SerializeField] private QuestionMethods _questionMethods;

    private String LEVEL_TEXT_FILES_PATH = "Assets\\Dialogues\\Dialogues_txt";
    
    private List<String> LEVEL_TXT_FILES = new List<string>();

    [SerializeField] private int NUMBER_OF_LEVEL = 3;
    
    private int CURRENT_LEVEL_INDEX = 1;

    [SerializeField] private Dialogue[] dialgues;
    
    private LevelEndCanvas _endCanvas;

    private MoneyAnimScript _moneyAnim;
    
    private void Start()
    {
        CURRENT_LEVEL_INDEX = PlayerPrefs.GetInt("CURRENT_LEVEL_INDEX", 1);
        
        print(CURRENT_LEVEL_INDEX);
        
        GetTextFilesPath();
        
        _endCanvas = FindObjectOfType<LevelEndCanvas>();
        _moneyAnim = FindObjectOfType<MoneyAnimScript>();
    }

    void GetTextFilesPath()
    {
        _dialogueScript.ReadLinesFromTxt(dialgues[CURRENT_LEVEL_INDEX - 1]);
    }

    public void NextLevel(bool _success)
    {
        if (_success)
        {
            if (CURRENT_LEVEL_INDEX == NUMBER_OF_LEVEL)
            {
                CURRENT_LEVEL_INDEX = 1;
            }
            else
            {
                CURRENT_LEVEL_INDEX += 1;
            }
        }
        
        PlayerPrefs.SetInt("CURRENT_LEVEL_INDEX", CURRENT_LEVEL_INDEX);

        StartCoroutine(WaitForLevelChange());
    }

    public void IncreaseMoney()
    {
        _moneyAnim.IncreaseMoney(100);
    }

    IEnumerator WaitForLevelChange()
    {
        _endCanvas.Change();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    //Answers altındaki butonlara atandi
    public void Button(int _id)
    {
        String answerResult = _dialogueScript.ReturnAnswerResult(_questionMethods.getIndex(), _id);
        
        _questionMethods.CheckAnswer(answerResult, _id);
    }


    public void StartConversationWhenCharacterSit()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(1);
        _questionMethods.BubbleAnim(true);
        yield return new WaitForSeconds(.6f);
        _questionMethods.StartConversation();
        yield return new WaitForSeconds(1.2f);
        _questionMethods.ButtonAnim(true);
        
    }
    
}
