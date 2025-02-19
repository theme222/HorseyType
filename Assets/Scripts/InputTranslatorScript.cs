using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 245 239 238
public class InputTranslatorScript : MonoBehaviour
{
    public InputDetectorScript InputDetector;
    public PassageHandlerScript PassageHandler;
    public int longPressInterval; // Milliseconds
    public TextMeshProUGUI currentInputText;
    public string currentInput;
    int currentStartTime;
    
    Dictionary<string, char> morseDictionary = new Dictionary<string, char>
        {
            { ".-", 'A' },
            { "-...", 'B' },
            { "-.-.", 'C' },
            { "-..", 'D' },
            { ".", 'E' },
            { "..-.", 'F' },
            { "--.", 'G' },
            { "....", 'H' },
            { "..", 'I' },
            { ".---", 'J' },
            { "-.-", 'K' },
            { ".-..", 'L' },
            { "--", 'M' },
            { "-.", 'N' },
            { "---", 'O' },
            { ".--.", 'P' },
            { "--.-", 'Q' },
            { ".-.", 'R' },
            { "...", 'S' },
            { "-", 'T' },
            { "..-", 'U' },
            { "...-", 'V' },
            { ".--", 'W' },
            { "-..-", 'X' },
            { "-.--", 'Y' },
            { "--..", 'Z' },
            { "-----", '0' },
            { ".----", '1' },
            { "..---", '2' },
            { "...--", '3' },
            { "....-", '4' },
            { ".....", '5' },
            { "-....", '6' },
            { "--...", '7' },
            { "---..", '8' },
            { "----.", '9' }
        }; // I don't wanna hear it

    int GetCurrentTimeMilli() 
    {
        return (int)Mathf.Round(Time.time * 1000);
    }

    void Awake()
    {
        InputDetector.OnKeyChange.AddListener(Transcript);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && currentInput != "")
        {
            Translate();
            currentInput = "";
            currentInputText.text = currentInput;

        }
    }

    void Transcript(bool pressingKey)
    {
        if (pressingKey)
        {
            currentStartTime = GetCurrentTimeMilli();
            return;
        } 
        
        int timeDiff = GetCurrentTimeMilli() - currentStartTime;
        if (timeDiff < longPressInterval) currentInput += ".";
        else currentInput += "-";
        currentInputText.text = currentInput;

    }

    void Translate()
    {
        if (morseDictionary.ContainsKey(currentInput)) TypeCheck(morseDictionary[currentInput]);
        else TypeCheck('!');
    }

    void TypeCheck(char morseInput)
    {
        print(morseInput);
        if (char.ToUpper(PassageHandler.currentCharacter) == char.ToUpper(morseInput)) PassageHandler.MoveText();
    }


}
