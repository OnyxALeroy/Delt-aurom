using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private Image[] buttonImages = new Image[5];
    [SerializeField] private Sprite[] unselectedButtonSprites = new Sprite[5];
    [SerializeField] private Sprite[] selectedButtonSprites = new Sprite[5];
    [SerializeField] private Sprite mainCharacterActUnselectedButtonSprite;
    [SerializeField] private Sprite mainCharacterActSelectedButtonSprite;

    private bool isMainCharacter = false;
    public bool IsMainCharacter => isMainCharacter;

    void Awake()
    {
        UpdateButtons(0);
    }

    void Update() { }

    // --------------------------------------------------------------------------------------------
    
    public void SetMainCharacter(bool value)
    {
        isMainCharacter = value;
        UpdateButtons(0);
    }

    public void UpdateButtons(int selectedIndex)
    {
        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (i == selectedIndex)
            {
                if (i == 1 && isMainCharacter)
                {
                    buttonImages[i].sprite = mainCharacterActSelectedButtonSprite;
                } else
                {
                    buttonImages[i].sprite = selectedButtonSprites[i];
                }
            }
            else
            {
                if (i == 1 && isMainCharacter)
                {
                    buttonImages[i].sprite = mainCharacterActUnselectedButtonSprite;
                }
                else
                {
                    buttonImages[i].sprite = unselectedButtonSprites[i];
                }
            }
        }
    }

    // --------------------------------------------------------------------------------------------
}
