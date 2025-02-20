// Handles the lighting of the Center icon and the sounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MorseQueuerScript : MonoBehaviour
{
    UnityEvent OnToggleChange = new UnityEvent();
    SettingsManagerScript Settings;
    ColorManagerScript GameColor;
    InputDetectorScript Detector;

    public AudioClip morseSound;
    AudioSource source;

    Queue<char> morseQueue = new Queue<char>();  // Used for semi and auto | Manual uses the update loop

    // Controled by settings manager //
    float dashThreshold;
    string mode;
    // Controled by settings manager //

    long startTime;
    Image img;
    bool toggle = false;
    bool ready = true;


    void Awake()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GameController"))
        {
            if (obj.name == "Settings Screen") Settings = obj.GetComponent<SettingsManagerScript>();
            if (obj.name == "Color Manager") GameColor = obj.GetComponent<ColorManagerScript>();
        }

        Detector = GetComponent<InputDetectorScript>();
        source = GetComponent<AudioSource>();
        img = GetComponent<Image>();
        OnToggleChange.AddListener(ChangeStatusSemiAndAuto);  // This is for semi and auto modes
        Detector.OnKeyChange.AddListener(ChangeStatusManual);  // This is for manual mode
        if (!Settings) print("bruhh");
        Settings.OnSettingsChange.AddListener(SettingsListener);
    }

    void Start()
    {
        source.clip = morseSound;
    }

    void Update()
    {
        char result;
        if (!ready) return;
        if (!morseQueue.TryDequeue(out result)) return;
        ready = false;
        print("startingcrouroiutasdfkllkasdfkjlasdf");
        StartCoroutine(DoTimerIGuess(result));

    }

    void ChangeStatusSemiAndAuto()
    {
        if (mode == "manual") return;
        if (toggle)
        {
            img.color = GameColor.accent;
            source.Play();
        }
        else
        {
            img.color = GameColor.primary;
            source.Stop();
        }
    }

    void ChangeStatusManual(bool pressingKey, string _)
    {
        if (mode != "manual") return;
        if (pressingKey)
        {
            img.color = GameColor.accent;
            source.Play();
        }
        else
        {
            img.color = GameColor.primary;
            source.Stop();
        }
    }

    IEnumerator DoTimerIGuess(char length)
    {
        ready = false;
        toggle = true;
        OnToggleChange.Invoke();
        if (length == '.') yield return new WaitForSeconds((float)(dashThreshold / 3000f));
        else if (length == '-') yield return new WaitForSeconds((float)(dashThreshold / 1000f));
        else yield break;
        toggle = false;
        OnToggleChange.Invoke();
        yield return new WaitForSeconds((float)(dashThreshold / 3000f));
        ready = true;
    }

    public void QueueIt(string a) // InputDetectorScript calls this for semi and auto modes.
    {
        morseQueue.Enqueue(a[0]);
    }


    long GetCurrentTimeMilli()
    {
        return (long)Mathf.Round(Time.time * 1000);
    }

    void SettingsListener()
    {
        mode = Settings.currentMode.ToString();
        dashThreshold = Settings.currentDashThreshold;
    }

}
