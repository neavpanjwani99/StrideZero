using TMPro;
using UnityEngine;

public class NameInputLoader : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        inputField.text = PlayerPrefs.GetString("PlayerName", "");
    }
}