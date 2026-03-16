using UnityEngine;
using TMPro;

public class BrainUI : MonoBehaviour
{
    public static BrainUI instance;

    public TMP_Text brainText;

    void Awake()
    {
        instance = this;
    }

    public void UpdateBrains(int amount)
    {
        brainText.text = amount.ToString();
    }
}