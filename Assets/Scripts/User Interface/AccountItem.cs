using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountItem : MonoBehaviour {

    public string Name = "Random Guy";
    public string Balance = "123.45$";
    public Color BalanceColour = Color.green;

    public int ID;

    public Text NameText, BalanceText;
    public bool ConstantUpdate = true;

    public void Start()
    {
        RefreshText();
    }

    public void Update()
    {
        if(ConstantUpdate)
            RefreshText();
    }

    public void ButtonPressed()
    {
        AccountDetailsView.Instance.OpenAccount(ID);
    }

    public void RefreshText()
    {
        NameText.text = Name.Trim();
        BalanceText.text = "<color=#" + ColorUtility.ToHtmlStringRGBA(BalanceColour) + ">" + Balance.Trim() + "</color>";
    }

}
