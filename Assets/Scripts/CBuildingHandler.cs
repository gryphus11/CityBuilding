using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuildingHandler : MonoBehaviour
{
    [SerializeField]
    private CCity _city = null;

    [SerializeField]
    private CUIController _uiController = null;

    [SerializeField]
    private CBuilding[] _buildings = null;

    [SerializeField]
    private CBoard _board = null;

    private CBuilding _selectedBuilding = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && _selectedBuilding != null)
        {
            InteractWithBoard(BuildingAction.Build);
        }
        else if (Input.GetMouseButtonDown(0) && _selectedBuilding != null)
        {
            InteractWithBoard(BuildingAction.Build);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(BuildingAction.Remove);
        }
    }

    private void InteractWithBoard(BuildingAction action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            Vector3 gridPosition = _board.CalculateGridPosition(hit.point);

            // UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() => UI 오브젝트를 눌렀는지 판단
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == BuildingAction.Build && _board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (_city.Cash >= _selectedBuilding.cost)
                    {
                        _city.DepositCash(-_selectedBuilding.cost);
                        _uiController.UpdateCityData();
                        _city.buildingCounts[_selectedBuilding.id]++;
                        _board.AddBuilding(_selectedBuilding, gridPosition);
                    }
                }
                else if(action == BuildingAction.Remove && _board.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    _city.DepositCash(_board.CheckForBuildingAtPosition(gridPosition).cost / 2);
                    _uiController.UpdateCityData();
                    _board.RemoveBuilding(gridPosition);
                }
            }
        }
    }

    public void EnableBuilder(int building)
    {
        _selectedBuilding = _buildings[building];
        //Debug.Log("Selected Building : " + _selectedBuilding.buildingName);
    }
}
