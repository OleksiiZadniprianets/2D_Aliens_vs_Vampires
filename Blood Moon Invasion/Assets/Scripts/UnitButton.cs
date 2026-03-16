using UnityEngine;

public class UnitButton : MonoBehaviour
{
    public GameObject unitPrefab;
    public int cost = 20;

    public void Select()
    {
        BuildManager.instance.SelectUnit(unitPrefab, cost);
    }
}