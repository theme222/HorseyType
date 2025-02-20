using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class InputDetectorScript : MonoBehaviour
{
    public UnityEvent<bool, string> OnKeyChange; // Passes isPressingKey through the event (The new action) Used for manual and semi transcript
    public UnityEvent OnMorseAdded;
    public ColorManagerScript GameColor;
    public SettingsManagerScript Settings;
    ScreenManagerScript Screen;
    MorseQueuerScript Queuer;

    public AudioClip morseSound;
    public TextMeshProUGUI currentInputText;
    [System.NonSerialized]
    public bool[] isPressingKey = { false, false }; // First is primary second is secondary
    [System.NonSerialized]
    public long[] startPressTime = { 0, 0 };
    [System.NonSerialized]
    public long[] endPressTime = { 0, 0 };

    // Controled by settings manager //
    KeyCode primaryKey; 
    KeyCode secondaryKey;
    int dashThreshold;
    string mode;
    // Controled by settings manager //

    // Used by AutoTranscript // 
    string previousAction;  // This guy gets set by AppendTextToInput which gets called by manual and semi tho
    long previousTime;
    // Used by AutoTranscript // 

    Image img;
    AudioSource source;

    void Awake()
    {
        img = GetComponent<Image>();
        source = GetComponent<AudioSource>();
        //OnKeyChange.AddListener(ChangeMaterialColor);
        //OnKeyChange.AddListener(PlaySound);

        GameObject[] controllers;
        controllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject obj in controllers)
        {
            if (obj.name == "Canvas") Screen = obj.GetComponent<ScreenManagerScript>();
        }
        Queuer = GetComponent<MorseQueuerScript>();

        OnKeyChange.AddListener(ManualTranscript);
        OnKeyChange.AddListener(SemiTranscript);

        Settings.OnSettingsChange.AddListener(SettingsListener);
    }

    // Start is called before the first frame update
    void Start()
    {
        source.clip = morseSound;
    }

    // Update is called once per frame
    void Update()
    {
        // I DO NOT WANNA HEAR IT
        bool isPressingPrimaryKeyAndDifferent = isPressingKey[0] != Input.GetKey(primaryKey) && Input.GetKey(primaryKey);
        bool isntPressingPrimaryKeyAndDifferent = isPressingKey[0] != Input.GetKey(primaryKey) && !Input.GetKey(primaryKey);
        bool isPressingSecondaryKeyAndDifferent = isPressingKey[1] != Input.GetKey(secondaryKey) && Input.GetKey(secondaryKey);
        bool isntPressingSecondaryKeyAndDifferent = isPressingKey[1] != Input.GetKey(secondaryKey) && !Input.GetKey(secondaryKey);

        if (isPressingPrimaryKeyAndDifferent)
        {
            startPressTime[0] = GetCurrentTimeMilli();
            OnKeyChange.Invoke(true, "primary");
        }
        if (isntPressingPrimaryKeyAndDifferent)
        {
            endPressTime[0] = GetCurrentTimeMilli();
            OnKeyChange.Invoke(false, "primary");
        }
        if (isPressingSecondaryKeyAndDifferent)
        {
            startPressTime[1] = GetCurrentTimeMilli();
            OnKeyChange.Invoke(true, "secondary");
        } 
        if (isntPressingSecondaryKeyAndDifferent)
        {
            endPressTime[1] = GetCurrentTimeMilli();
            OnKeyChange.Invoke(false, "secondary");
        } 

        isPressingKey[0] = Input.GetKey(primaryKey);
        isPressingKey[1] = Input.GetKey(secondaryKey);

        AutoTranscript();
    }

    void ManualTranscript(bool pressingKey, string key) // Uses OnKeyChange
    {
        if (Screen.currentScreen != "morse") return;
        if (key == "secondary" || pressingKey || mode != "manual") return;

        long timeDiff = endPressTime[0] - startPressTime[0];
        if (timeDiff < dashThreshold) AppendTextToInput(".");
        else AppendTextToInput("-");
        OnMorseAdded.Invoke();
    }

    void SemiTranscript(bool pressingKey, string key) // Uses OnKeyChange
    {
        if (Screen.currentScreen != "morse") return;
        if (!pressingKey || mode != "semi") return;
        if (key == "primary") AppendTextToInput(".");
        else AppendTextToInput("-");
        OnMorseAdded.Invoke();
    }

    void AutoTranscript() // Is on the update loop
    {
        if (Screen.currentScreen != "morse") return;
        if (mode != "auto") return;
        bool pastCooldown;
        if (previousAction == ".") pastCooldown = GetCurrentTimeMilli() - previousTime > (dashThreshold / 3) * 2;
        else pastCooldown = GetCurrentTimeMilli() - previousTime > (dashThreshold / 3) * 4;
        if (isPressingKey[0] && isPressingKey[1] && pastCooldown)
        {
            if (previousAction == "-")
            {
                AppendTextToInput(".");
            }
            else
            {
                AppendTextToInput("-");
            }
        }
        else if (isPressingKey[1] && pastCooldown)
        {
            AppendTextToInput("-");
        }
        else if (isPressingKey[0] && pastCooldown)
        {
            AppendTextToInput(".");
        }
        else return;
        previousTime = GetCurrentTimeMilli();
        OnMorseAdded.Invoke();
    }

    void AppendTextToInput(string text)
    {
        if (mode != "manual") Queuer.QueueIt(text);
        currentInputText.text += text;
        previousAction = text;
    }

    void SettingsListener()
    {
        primaryKey = Settings.currentPrimaryKey;
        secondaryKey = Settings.currentSecondaryKey;
        mode = Settings.currentMode.ToString();
        dashThreshold = Settings.currentDashThreshold;
    }

    long GetCurrentTimeMilli()
    {
        return (long)Mathf.Round(Time.time * 1000);
    }
}


/*
   void ChangeMaterialColor(bool pressingKey)
   {
       if (pressingKey) img.color = GameColor.accent;
       else img.color = GameColor.primary;
   }

   void PlaySound(bool pressingKey)
   {
       if (mode != Settings.mode.manual) return;
       if (pressingKey) source.Play();
       else source.Stop();
   }
   */