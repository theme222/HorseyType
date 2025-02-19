using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorManagerScript : MonoBehaviour
{
    public UnityEvent OnThemeChange;
    public Color backgroundColor;
    public Color accentColor;
    public Color primaryColor;
    public Color secondaryColor;
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
        backgroundColor = HexToColor("#4a5b6e");
        accentColor = HexToColor("#f8cdc6");
        primaryColor = HexToColor("#9ec1cc");
        secondaryColor = HexToColor("#f5efee");
    }

}
