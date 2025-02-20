using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; // HEHEHEHHEHEHEHEHHE
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PassageHandlerScript : MonoBehaviour
{
    public ColorManagerScript GameColor;
    public GameObject textCharacterPrefab;
    public Passage currentPassage;
    public char currentCharacter = ' ';

    [System.NonSerialized]
    public GameObject[] currentCharacterObjects = new GameObject[characterAmount];
    [System.NonSerialized]
    public int indexInPassage;
    
    string API_LINK = "https://thequoteshub.com/api/";
    
    static int characterAmount = 31;
    static int characterCenterIndex = (int)(characterAmount/2);
    static int characterSize = 70;


    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateCharacterObjects();
        StartCoroutine(GetPassage());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCharacterObjects()
    {
        for (int i = 0; i < characterAmount; i++)
        {
            currentCharacterObjects[i] = GameObject.Instantiate(textCharacterPrefab);
            currentCharacterObjects[i].transform.SetParent(transform);
            currentCharacterObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-characterSize * characterCenterIndex + characterSize * i, 0);
            if (i < characterCenterIndex) currentCharacterObjects[i].GetComponent<TextMeshProUGUI>().color = GameColor.primary;
            else currentCharacterObjects[i].GetComponent<TextMeshProUGUI>().color = GameColor.secondary;
        }
    }

    public void MoveText()
    {
        indexInPassage++;
        if (indexInPassage + characterCenterIndex >= currentPassage.text.Length)
        {
            StartCoroutine(GetPassage());
            return;
        }
        for (int i = 0; i < characterAmount; i++)
        {
            TextMeshProUGUI currentGUI = currentCharacterObjects[i].GetComponent<TextMeshProUGUI>();
            if (0 <= indexInPassage + i && indexInPassage + i < currentPassage.text.Length) currentGUI.text = currentPassage.text[indexInPassage + i].ToString();
            else currentGUI.text = " ";
            if (i == characterCenterIndex) currentCharacter = currentPassage.text[indexInPassage + i];
        }
        if (currentCharacter == ' ') MoveText();
    }

    IEnumerator GetPassage()
    {
        indexInPassage = ((int)(characterAmount / 2) * -1) - 1;
        using (UnityWebRequest request = UnityWebRequest.Get(API_LINK))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                currentPassage = JsonUtility.FromJson<Passage>(json);
                currentPassage.text = Regex.Replace(currentPassage.text, "[^A-Za-z0-9 ]", ""); // Me and teh bois love regex
                print(currentPassage.text);
            }
            else
            {
                Debug.LogError(request.error);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(GetPassage()); // This might cause problems but I can't be bothered to check
            }
        }
        MoveText();
    }

    public class Passage
    {
        public string id;
        public string text;
        public string author;
    }
}
