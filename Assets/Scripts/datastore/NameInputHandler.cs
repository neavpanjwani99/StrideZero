using UnityEngine;
using TMPro;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    public void SaveName()
    {
        string name = inputField.text;

        Debug.Log("FORCED SAVE: " + name);

        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("EMPTY NAME - NOT SAVED");
            return;
        }

        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();

        Debug.Log("NAME SAVED SUCCESSFULLY");
    }
}