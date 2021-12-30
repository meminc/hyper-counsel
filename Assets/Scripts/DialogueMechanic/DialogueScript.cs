using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueScript : MonoBehaviour
{
    private string[] lines;
    
    

    public void ReadLinesFromTxt(Dialogue _dialogue)
    {
        lines = _dialogue.dialogueScript.Split('\n');
    }

    public String ReturnQuestionStr(int index)
    {

        foreach (var line in lines)
        {
            string[] data = line.Split(';');
            if (data[0] == "Question" + index)
            {
                return data[1];
            }
        }

        return "";
    }

    public List<String> ReturnAnswers(int index)
    {
        List<String> answers = new List<string>();
        foreach (var line in lines)
        {
            string[] data = line.Split(';');
            if (data[0] == "Question" + index && data[1].Contains("Answer"))
            {
                answers.Add(data[2]);
            }
        }

        return answers;
    }

    public String ReturnAnswerResult(int index, int id)
    {
        foreach (var line in lines)
        {
            string[] data = line.Split(';');
            if (data[0] == "Question" + index && data[1].Contains("Answer" + id))
            {
                return data[3];
            }
        }

        return "";
    }
    
    public int ReturnNextQuesitonIndex(int index, int id)
    {
        int newIndex = 0;
        
 
        foreach (var line in lines)
        {
            string[] data = line.Split(';');
            if (data[0] == "Question" + index && data[1].Contains("Answer" + id))
            {
                String b = "";
                for (int i=0; i< data[4].Length; i++)
                {
                    if (Char.IsDigit(data[4][i]))
                        b += data[4][i];
                }

                if (b.Length>0)
                    newIndex = int.Parse(b);
            }
        }

        return newIndex;

    }
}
