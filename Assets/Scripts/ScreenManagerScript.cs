using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenManagerScript : MonoBehaviour
{   
    public RectTransform MorseScreen; // 500
    public RectTransform HomeScreen; // 0
    public RectTransform SettingsScreen; // -500
    public int screenSize;
    public float alphaValue; // Fraction to multiply the distance I guess?
    public string currentScreen = "home";

    IEnumerator MoveScreen(RectTransform screen, int yPos)
    {
        Vector2 finalPos = new Vector2(0, yPos);
        for (int i = 0; i < 200; i++)
        {
            screen.anchoredPosition += (finalPos - screen.anchoredPosition) * alphaValue * Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        screen.anchoredPosition = finalPos;
    }

    void Awake()
    {
        MorseScreen.anchoredPosition = new Vector2(0, screenSize);
        HomeScreen.anchoredPosition = new Vector2(0, 0);
        SettingsScreen.anchoredPosition = new Vector2(0, -screenSize);
    }

    public void CallMorseScreen()
    {
        StopAllCoroutines();
        StartCoroutine(MoveScreen(MorseScreen, 0));
        StartCoroutine(MoveScreen(HomeScreen, -screenSize));
        StartCoroutine(MoveScreen(SettingsScreen, -screenSize*2));
        currentScreen = "morse";
    }

    public void CallHomeScreen()
    {
        StopAllCoroutines();
        StartCoroutine(MoveScreen(MorseScreen, screenSize));
        StartCoroutine(MoveScreen(HomeScreen, 0));
        StartCoroutine(MoveScreen(SettingsScreen, -screenSize));
        currentScreen = "home";
    }

    public void CallSettingsScreen()
    {
        StopAllCoroutines();
        StartCoroutine(MoveScreen(MorseScreen, screenSize*2));
        StartCoroutine(MoveScreen(HomeScreen, screenSize));
        StartCoroutine(MoveScreen(SettingsScreen, 0));
        currentScreen = "settings";
    }

    public void QuitOutOfExistence()
    {
        Application.Quit();
    }

    public void SendToGithub()
    {
        Application.OpenURL("https://github.com/theme222/HorseyType");
    }
}
