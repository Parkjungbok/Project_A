using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int money = 0;
    public Text moneyText; 

    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney( int amount )
    {
        money += amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        if ( moneyText != null )
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }
}
