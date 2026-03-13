using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject selectedUnit;
    public int selectedCost;

    [Header("Lane centers")]
    public float topLaneY = 3.06f;
    public float bottomLaneY = -0.69f;

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

        int lane;
        Vector3 spawnPos = position;

        if (position.y > 1.0f) // верхня дорога
        {
            lane = 0;
            spawnPos.y = 2.0f;
        }
        else // нижня дорога
        {
            lane = 1;
            spawnPos.y = -2.0f;
        }

        if (!CoinManager.instance.SpendCoins(selectedCost))
            return;

        GameObject unit = Instantiate(selectedUnit, spawnPos, Quaternion.identity);

        AlienBladeController blade = unit.GetComponent<AlienBladeController>();
        if (blade != null)
            blade.lane = lane;

        AlienBlasterController blaster = unit.GetComponent<AlienBlasterController>();
        if (blaster != null)
            blaster.lane = lane;

        selectedUnit = null;
    }

    int GetNearestLane(float yPos)
    {
        float distToTop = Mathf.Abs(yPos - topLaneY);
        float distToBottom = Mathf.Abs(yPos - bottomLaneY);

        if (distToTop < distToBottom)
            return 0;

        return 1;
    }
}