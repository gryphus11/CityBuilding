using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIController : MonoBehaviour
{
    private CCity _city = null;

    [SerializeField]
    private Text _dayText = null;

    [SerializeField]
    private Text _cityText = null;

    private void Awake()
    {
        _city = GetComponent<CCity>();
    }

    public void UpdateCityData()
    {
        _cityText.text = string.Format("Jobs : {0}/{1}\nCash : ${2} (+${6})\nPop : {3}/{4}\nFood : {5}\n",
            _city.JobsCurrent, _city.JobsCeiling, 
            _city.Cash, 
            (int)_city.PopulationCurrent, (int)_city.PopulationCeiling, 
            (int)_city.Food, (int)_city.JobsCurrent * 2.0f);
    }

    public void UpdateDayCount()
    {
        _dayText.text = string.Format("Day : {0}", _city.Day);
    }
}
