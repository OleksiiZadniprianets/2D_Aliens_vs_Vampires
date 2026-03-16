using UnityEngine;
using UnityEngine.EventSystems;

public class MapClickManager : MonoBehaviour
{
    void Update()
    {
        if (UFOAbilityManager.instance != null && UFOAbilityManager.instance.IsSelectingTarget())
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (UFOAbilityManager.instance != null)
        {
            if (UFOAbilityManager.instance.IsSelectingTarget())
                return;
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        Debug.Log(pos);
        BuildManager.instance.PlaceUnit(pos);
    }
}