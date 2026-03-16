using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins = 100;
    void Start()
    {
        BrainUI.instance.UpdateBrains(coins);
    }
    void Awake()
    {
        instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        BrainUI.instance.UpdateBrains(coins);

        Debug.Log("Coins: " + coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;

            BrainUI.instance.UpdateBrains(coins);

            Debug.Log("Coins: " + coins);
            return true;
        }

        Debug.Log("Not enough coins!");
        return false;
    }
}