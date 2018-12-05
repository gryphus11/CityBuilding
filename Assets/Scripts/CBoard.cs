using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoard : MonoBehaviour
{
    private CBuilding[,] _buildings = new CBuilding[100, 100];

    public void AddBuilding(CBuilding building, Vector3 position)
    {
        CBuilding buildingToAdd = Instantiate(building, position, Quaternion.identity);
        _buildings[(int)position.x, (int)position.z] = buildingToAdd;
    }

    public void RemoveBuilding(Vector3 position)
    {
        Destroy(_buildings[(int)position.x, (int)position.z].gameObject);
        _buildings[(int)position.x, (int)position.z] = null;
    }

    public CBuilding CheckForBuildingAtPosition(Vector3 position)
    {
        return _buildings[(int)position.x, (int)position.z];
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), 0.5f, Mathf.Round(position.z));
    }
}
