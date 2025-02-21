using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 245 239 238
public class InputTranslatorScript : MonoBehaviour
{
    public InputDetectorScript InputDetector;
    public ColorManagerScript GameColor;
    public PassageHandlerScript PassageHandler;
    public SettingsManagerScript Settings;
    public TextMeshProUGUI currentInputText;
    int longPressInterval; // Milliseconds
    public string currentInput;
    string mode;

    Dictionary<char, string> alphabetToMorse = new Dictionary<char, string>
        {
            { 'A', ".-" },
            { 'B', "-..." },
            { 'C', "-.-." },
            { 'D', "-.." },
            { 'E', "." },
            { 'F', "..-." },
            { 'G', "--." },
            { 'H', "...." },
            { 'I', ".." },
            { 'J', ".---" },
            { 'K', "-.-" },
            { 'L', ".-.." },
            { 'M', "--" },
            { 'N', "-." },
            { 'O', "---" },
            { 'P', ".--." },
            { 'Q', "--.-" },
            { 'R', ".-." },
            { 'S', "..." },
            { 'T', "-" },
            { 'U', "..-" },
            { 'V', "...-" },
            { 'W', ".--" },
            { 'X', "-..-" },
            { 'Y', "-.--" },
            { 'Z', "--.." },
            { '0', "-----" },
            { '1', ".----" },
            { '2', "..---" },
            { '3', "...--" },
            { '4', "....-" },
            { '5', "....." },
            { '6', "-...." },
            { '7', "--..." },
            { '8', "---.." },
            { '9', "----." }
        }; // I don't wanna hear it


    void Awake()
    {
        Settings.OnSettingsChange.AddListener(SettingsListener);
        InputDetector.OnMorseAdded.AddListener(MistakeCheck);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentInputText.color = GameColor.accent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MistakeCheck()
    {
        string correctMorse = alphabetToMorse[Char.ToUpper(PassageHandler.currentCharacter)];
        if (currentInputText.text.Length > correctMorse.Length) PunishMistake();
        if (currentInputText.text != correctMorse[..currentInputText.text.Length]) PunishMistake();
        if (currentInputText.text == correctMorse) DoSuccess();
    }

    void PunishMistake()
    {
        currentInputText.text = "";
        TextMeshProUGUI centerText = PassageHandler.currentCharacterObjects[PassageHandler.characterCenterIndex].GetComponent<TextMeshProUGUI>();
        centerText.color = GameColor.GetColor("error");
    }

    void DoSuccess()
    {
        currentInputText.text = "";
        PassageHandler.MoveText();
        TextMeshProUGUI centerText = PassageHandler.currentCharacterObjects[PassageHandler.characterCenterIndex].GetComponent<TextMeshProUGUI>();
        centerText.color = GameColor.GetColor("secondary"); // Just incase the script change the color because of a mistake
    }

    void SettingsListener()
    {
        longPressInterval = Settings.currentDashThreshold;
        mode = Settings.currentMode.ToString();
    }

}
