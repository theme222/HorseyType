using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SettingsManagerScript : MonoBehaviour
{
    public UnityEvent OnSettingsChange;
    public ColorManagerScript GameColor;

    public Image manualButton;
    public Image semiButton;
    public Image autoButton;
    public TMP_InputField dashThresholdTextInput;
    public TextMeshProUGUI primaryKeyTextInput;
    public TextMeshProUGUI secondaryKeyTextInput;
    public TextMeshProUGUI modeDescriptionText;

    public int currentDashThreshold;
    public KeyCode currentPrimaryKey;
    public KeyCode currentSecondaryKey;
    public Mode currentMode; // manual semi auto

    KeyCode theImpossibleKey = KeyCode.Joystick8Button19; // Try pressing this one bitches

    Dictionary<Mode, string> modeDescription = new Dictionary<Mode, string>
    {
        {Mode.manual, "Player controls timing for dots (.), dashes (-), and timing between inputs for characters"},
        {Mode.semi, "Player presses primary key to input a dot (.) and secondary key to input a dash (-) once and doesn't repeat"},
        {Mode.auto, "Player presses primary key for dots (.) and secondary key for dashes (-) and repeats while holding"},
    };

    void Update()
    {
        if (currentPrimaryKey != theImpossibleKey && currentSecondaryKey != theImpossibleKey && !Input.anyKeyDown) return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(key)) continue;
        
            if (currentPrimaryKey == theImpossibleKey) 
            {
                currentPrimaryKey = key;
                primaryKeyTextInput.text = key.ToString();
                InvokeSettingsChangeEvent();
                return;
            }
            else if (currentSecondaryKey == theImpossibleKey) 
            {
                currentSecondaryKey = key;
                secondaryKeyTextInput.text = key.ToString();
                InvokeSettingsChangeEvent();
                return;
            }
            
        }

    }

    public void InvokeSettingsChangeEvent()
    {
        currentDashThreshold = int.Parse(dashThresholdTextInput.text);
        OnSettingsChange.Invoke();
    }

    public void ChangeMode(Mode mode)
    {
        currentMode = mode;
        modeDescriptionText.text = modeDescription[mode];
        Color selectedColor = GameColor.primary;
        Color unselectedColor = GameColor.secondary;
        switch(mode)
        {
            case Mode.manual:
                manualButton.color = selectedColor;
                semiButton.color = unselectedColor;
                autoButton.color = unselectedColor;
                break;
            case Mode.semi:
                semiButton.color = selectedColor;
                manualButton.color = unselectedColor;
                autoButton.color = unselectedColor;
                break;
            case Mode.auto:
                autoButton.color = selectedColor;
                semiButton.color = unselectedColor;
                manualButton.color = unselectedColor;
                break;
        }
        InvokeSettingsChangeEvent();
    }

    public void ChangeManualMode()
    {
        ChangeMode(Mode.manual);
    }

    public void ChangeSemiMode()
    {
        ChangeMode(Mode.semi);
    }

    public void ChangeAutoMode()
    {
        ChangeMode(Mode.auto);
    }

    public void GetNextKeyDownPrimary()
    {
        currentPrimaryKey = theImpossibleKey;
        primaryKeyTextInput.text = "Press any key";
    }

    public void GetNextKeyDownSecondary()
    {
        currentSecondaryKey = theImpossibleKey; 
        secondaryKeyTextInput.text = "Press any key";
    }

    void ColorListener()
    {
        ChangeMode(currentMode); // Reset button colors
    }

    void Awake()
    {
        currentPrimaryKey = KeyCode.Period;
        currentSecondaryKey = KeyCode.Slash;
        GameColor.OnThemeChange.AddListener(ColorListener);
    }

    void Start()
    {
        ChangeMode(Mode.manual);
    }

    public enum Mode 
    {
        manual,
        semi,
        auto
    }

}
