using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class StringIntPair
{
    public string key;
    public Sprite value;
}

public class TextDisplayer : MonoBehaviour
{
    [Header("Letter Sprites")]
    [SerializeField] private List<StringIntPair> letterSprites;

    [Header("Image References")]
    [SerializeField] private Image[] lettersImages;

    private Dictionary<string, Sprite> lettersDict = new Dictionary<string, Sprite>();

    private void Awake() {
        // Convert list to dictionary at runtime
        lettersDict.Clear();
        if (letterSprites != null) {
            foreach (var pair in letterSprites) { lettersDict[pair.key] = pair.value; }
        }
    }

    public void SetValue(string value) {
        foreach (Image image in lettersImages) { image.gameObject.SetActive(true); }

        int counter = 0;
        foreach (char letter in value) {
            if (counter >= lettersImages.Length) { break; }

            string letterAsString = letter.ToString();
            if (lettersDict.ContainsKey(letterAsString)) {
                lettersImages[counter].sprite = lettersDict[letterAsString];
                counter++;
            }
        }

        while (counter < lettersImages.Length) {
            lettersImages[counter].gameObject.SetActive(false);
            counter++;
        }
    }
}
