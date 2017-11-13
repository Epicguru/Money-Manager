using UnityEngine;
using UnityEngine.UI;

public class ManagerAccountItem : MonoBehaviour {

    public Text Name;

    public void SetText(string text)
    {
        Name.text = text;
    }

    public string GetText()
    {
        return Name.text;
    }
}