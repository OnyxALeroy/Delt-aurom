using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HPNumberSprites : MonoBehaviour
{
    [Header("Digit Sprites (0-9)")]
    [SerializeField] private Sprite[] digitSprites = new Sprite[10];

    [Header("Image References")]
    [SerializeField] private Image[] currentDigits;
    [SerializeField] private Image[] maxDigits;

    public void SetHP(int current, int max)
    {
        SetNumber(currentDigits, current);
        SetNumber(maxDigits, max);
    }

    private void SetNumber(Image[] slots, int number)
    {
        number = Mathf.Clamp(number, 0, 999);
        string text = number.ToString();
        int lastIndex = slots.Length - 1;
        int slotIndex = lastIndex;
        for (int charIndex = text.Length - 1; charIndex >= 0; charIndex--)
        {
            int digit = text[charIndex] - '0';
            slots[slotIndex].sprite = digitSprites[digit];
            slots[slotIndex].enabled = true;
            slotIndex--;
        }
    }
}
