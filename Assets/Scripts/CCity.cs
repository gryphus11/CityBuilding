using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCity : MonoBehaviour
{
    /*
        최대작업수 : 공장수 * 10.0f;
        현재작업수 : min(현재 인구, 최대작업수);
        현재식량 += 농장수 * 4.0f;

        최대인구 = 집수 * 5.0f;
        if(현재식량 >= 현재인구 && 현재인구 < 최대인구)
        {
            현재식량 -= 현재인구 * 0.25f;
            현재인구 = min(현재인구 += 현재식량 * 0.25f, 최대인구)
        }

        자금 += 현재작업수 * 2.0f;
    */

    public int Cash { get; set; }
    public int Day { get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public float JobsCurrent { get; set; }
    public float JobsCeiling { get; set; }
    public float Food { get; set; }

    // 0 Road, 1 House, 2 Farm, 3 Factory
    public int[] buildingCounts = new int[4];

    private CUIController _uiController = null;

    // Use this for initialization
    void Start()
    {
        _uiController = GetComponent<CUIController>();
        Cash = 1000;
        Food = 0.0f;
        JobsCeiling = 0.0f;
        buildingCounts[0] = 0;
        buildingCounts[1] = 0;
        buildingCounts[2] = 0;
        buildingCounts[3] = 0;
        _uiController.UpdateCityData();
        _uiController.UpdateDayCount();
    }

    public void EndTurn()
    {
        ++Day;

        CalculateCash();
        CalculatePopulation();
        CalculateJob();
        CalculateFood();

        _uiController.UpdateCityData();
        _uiController.UpdateDayCount();
    }

    private void CalculateJob()
    {
        JobsCeiling = buildingCounts[3] * 10.0f;
        JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
    }

    private void CalculateCash()
    {
        Cash += (int)(JobsCurrent * 2.0f);
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
    }

    private void CalculateFood()
    {
        Food += (buildingCounts[2] * 4.0f);
    }

    private void CalculatePopulation()
    {
        PopulationCeiling = (buildingCounts[1] * 5.0f);

        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            Food -= PopulationCurrent * 0.25f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * 0.25f, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * 0.5f;
        }
    }
}
