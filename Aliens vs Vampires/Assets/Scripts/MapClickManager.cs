using UnityEngine;
using UnityEngine.EventSystems;

public class MapClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            BuildManager.instance.PlaceUnit(pos);
        }
    }
}