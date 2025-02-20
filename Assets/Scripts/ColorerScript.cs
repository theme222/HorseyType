// I think you like composition a biiiiiiiit too much my dude
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorerScript : MonoBehaviour
{
    public ColorType imageColor = ColorType.none;
    public ColorType hoverColor = ColorType.none;
    public ColorType textColor = ColorType.none;
    public ColorType cameraBackgroundColor = ColorType.none;
    Image img;
    Button btn;
    TextMeshProUGUI text;
    Camera cam;

    ColorManagerScript GameColor;

    void Awake()
    {
        GameObject[] controllers;
        controllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject obj in controllers)
        {
            if (obj.name == "Color Manager") GameColor = obj.GetComponent<ColorManagerScript>();
        }

        img = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
        cam = GetComponent<Camera>();
        btn = GetComponent<Button>();

        GameColor.OnThemeChange.AddListener(ChangeColor);
    }

    void ChangeColor()
    {
        if (imageColor != ColorType.none && img) img.color = GameColor.GetColor(imageColor.ToString());
        if (textColor != ColorType.none && text) text.color = GameColor.GetColor(textColor.ToString());
        if (cameraBackgroundColor != ColorType.none && cam) cam.backgroundColor = GameColor.GetColor(cameraBackgroundColor.ToString());
        if (hoverColor != ColorType.none && btn)
        {
            ColorBlock block = btn.colors;
            block.highlightedColor = GameColor.GetColor(hoverColor.ToString());
            btn.colors = block;
        } 
    }


    public enum ColorType
    {
        none,
        primary,
        secondary,
        accent,
        background,
        shadow,
        error,
        
    }



}


