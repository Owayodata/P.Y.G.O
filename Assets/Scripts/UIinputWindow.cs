using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIinputWindow : MonoBehaviour
{
    private static UIinputWindow instance;
    private Button okBtn;
    private Button cancelBtn;
    private TextMeshProUGUI titleText;
    private TMP_InputField inputField;

    private void Awake()
    {
        instance = this;
        okBtn = transform.Find("okBtn").GetComponent<Button>();
        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        titleText = transform.Find("BackgroundSubmitScore/Title").GetComponent<TextMeshProUGUI>();
        inputField = transform.Find("BackgroundSubmitScore/inputField").GetComponent<TMP_InputField>();
        Hide();
    }

    private void Show(string titleString, string inputString, string validCharacters, Action onCancel, Action<string> onOkay)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        titleText.text = titleString;
        inputField.text = inputString;

        inputField.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            return ValidateChar(validCharacters, addedChar);
        };

        okBtn.onClick.AddListener(() =>
        {
            Hide();
            onOkay(inputField.text);
        });

        cancelBtn.onClick.AddListener(() =>
        {
            Hide();
            onCancel();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        okBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
    }

    private char ValidateChar(string validCharacters, char addedChar)
    {
        if (validCharacters.IndexOf(addedChar) != -1)
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    public static void Show_Static(string titleString, string inputString, string validCharacters, Action onCancel, Action<string> onOkay)
    {
        instance.Show(titleString, inputString, validCharacters, onCancel, onOkay);
    }

    public static void Show_Static(string titleString, int defaultInt, Action onCancel, Action<int> onOkay)
    {
        instance.Show(titleString, defaultInt.ToString(), "0123456789", onCancel, (string inputText) =>
        {
            if (int.TryParse(inputText, out int _i))
            {
                onOkay(_i);
            }
            else
            {
                onOkay(defaultInt);
            }
        });
    }
}
