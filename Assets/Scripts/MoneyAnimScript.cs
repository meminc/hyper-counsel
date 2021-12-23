using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAnimScript : MonoBehaviour
{
    public Text moneyText;

    private int CURRENT_MONEY;
    
    private void Start()
    {
        CURRENT_MONEY = PlayerPrefs.GetInt("CURRENT_MONEY", 0);

        moneyText.text = "" + CURRENT_MONEY;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseMoney(100);
        }
        
    }

    public void IncreaseMoney(int incAmount)
    {
        CURRENT_MONEY = CURRENT_MONEY + incAmount;

        StartCoroutine(MoneyAnim(CURRENT_MONEY, incAmount));
        
        PlayerPrefs.SetInt("CURRENT_MONEY", CURRENT_MONEY);
    }

    IEnumerator MoneyAnim(int targetMoney, int incAmount)
    {
        float animIncRate = (float)incAmount * Time.deltaTime;

        float _current_money = CURRENT_MONEY - incAmount;
        while (true)
        {
            _current_money += (animIncRate * .5f);
            if (_current_money < targetMoney)
            {
                moneyText.text = "" + Mathf.RoundToInt(_current_money);
            }
            else
            {
                moneyText.text = "" + targetMoney;
                break;
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
}
