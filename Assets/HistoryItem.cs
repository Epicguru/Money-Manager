using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryItem : MonoBehaviour {

    public Text Balance;
    public Text Date;
    public Text Details;

    public void RefreshText(string balance, Color balanceColour, string date, string details)
    {
        Balance.text = "<color=#" + ColorUtility.ToHtmlStringRGBA(balanceColour) + ">" + balance + "</color>";
        Date.text = date;
        Details.text = details.Trim();
    }
}
