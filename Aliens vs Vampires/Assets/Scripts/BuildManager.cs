using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject selectedUnit;
    public int selectedCost;

    void Awake()
    {
        instance = this;
    }

    public void SelectUnit(GameObject unitPrefab, int cost)
    {
        selectedUnit = unitPrefab;
        selectedCost = cost;

        Debug.Log("Unit selected");
    }

    public void PlaceUnit(Vector3 position)
    {
        if (selectedUnit == null)
            return;

        Collider2D hit = Physics2D.OverlapCircle(position, 0.7f);

        if (hit != null && hit.GetComponent<AlienBladeController>() != null)
        {
            Debug.Log("Place occupied");
            return;
        }

        if (!CoinManager.instance.SpendCoins(selectedCost))
            return;

        Instantiate(selectedUnit, position, Quaternion.identity);

        selectedUnit = null;
    }
}