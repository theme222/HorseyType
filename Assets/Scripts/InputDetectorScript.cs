using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputDetectorScript : MonoBehaviour
{
    public UnityEvent<bool> OnKeyChange; // Passes isPressingKey through the event (The new action)

    public AudioClip morseSound;
    public Material undetectedMat;
    public Material detectedMat;
    public bool isPressingKey = false;
    public string morseKey;
    
    Renderer rend;
    AudioSource source;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
        OnKeyChange.AddListener(ChangeMaterialColor);
        OnKeyChange.AddListener(PlaySound);
    }

    // Start is called before the first frame update
    void Start()
    {
        source.clip = morseSound;
    }

    // Update is called once per frame
    void Update()
    {
        bool currentKeyPress = Input.GetKey(morseKey);
        if (isPressingKey != currentKeyPress) OnKeyChange.Invoke(currentKeyPress); 
        isPressingKey = currentKeyPress;
    }

    void ChangeMaterialColor(bool pressingKey)
    {
        if (pressingKey) rend.material = detectedMat;
        else rend.material = undetectedMat;
    }

    void PlaySound(bool pressingKey)
    {
        if (pressingKey) source.Play();
        else source.Stop();
    }
    
}
 