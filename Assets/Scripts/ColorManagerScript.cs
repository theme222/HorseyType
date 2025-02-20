using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorManagerScript : MonoBehaviour
{
    public UnityEvent OnThemeChange;
    public Color background;
    public Color accent;
    public Color primary;
    public Color secondary;
    public static Color HexToColor(string hex)  // Untested ChatGPT Code Part 2 : Electric Boogaloo
    {
        // Remove the "#" if present
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        // Parse the hex string
        if (hex.Length == 6) // RGB format
        {
            if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            {
                return color;
            }
        }
        else if (hex.Length == 8) // RGBA format
        {
            if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            {
                return color;
            }
        }

        Debug.LogWarning("Invalid hex color format: " + hex);
        return Color.white; // Default fallback
    }

    void Awake()
    {
        background = HexToColor("#4a5b6e");
        accent = HexToColor("#f8cdc6");
        primary = HexToColor("#f5efee");
        secondary = HexToColor("#9ec1cc");
    }

    void Start()
    {
        OnThemeChange.Invoke();
    }

    public Color GetColor(string colorType)
    {
        switch (colorType)
        {
            case "background":
                return background;
            case "accent":
                return accent;
            case "primary":
                return primary;
            case "secondary":
                return secondary;
            case "shadow":
                return background - new Color(0.1f,0.1f,0.1f,0);
        }
        return new Color(0,0,0);
    }

}
