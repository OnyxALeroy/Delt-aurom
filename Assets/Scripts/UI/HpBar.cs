using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private HPNumberSprites hpNumberSprites;

    [Header("Animation")]
    [SerializeField] private bool smooth = true;
    [SerializeField] private float smoothSpeed = 5f;

    private int currentHP;
    private int maxHP = 1;

    private float displayedFill = 1f;

    void Update()
    {
        if (smooth)
        {
            float targetFill = (float)currentHP / maxHP;

            if (Mathf.Abs(displayedFill - targetFill) > 0.001f)
            {
                displayedFill = Mathf.Lerp(displayedFill, targetFill, Time.deltaTime * smoothSpeed);
                fillImage.fillAmount = displayedFill;
            }
        }
    }

    public void SetMaxHP(int max)
    {
        maxHP = max;
        UpdateImmediate();
    }

    public void SetCurrentHP(int hp)
    {
        currentHP = hp;
        UpdateImmediate();
    }

    private void UpdateImmediate()
    {
        if (!smooth)
        {
            fillImage.fillAmount = (float)currentHP / maxHP;
        }

        if (hpNumberSprites != null)
        {
            hpNumberSprites.SetHP(currentHP, maxHP);
        }
    }
}
