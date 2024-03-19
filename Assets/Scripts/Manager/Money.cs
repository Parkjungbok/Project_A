using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int MoneyAmount;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        MoneyAmount = 0;
    }

    private void Update()
    {
        if ( text != null )
        {
            text.text = MoneyAmount.ToString();
        }
    }
}

