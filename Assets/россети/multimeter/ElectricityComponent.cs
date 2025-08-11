using System;
using UnityEngine;

public class ElectricityComponent : MonoBehaviour
{
    [SerializeField] private Multimetr multimetr;
    [SerializeField] private ElectricityElement element;
    private MagnitEffected[] contacts;


    private void Awake()
    {
        contacts = GetComponentsInChildren<MagnitEffected>();
    }
    public float GetVoltage()
    {
        if (element.DC)
            return element.DCVoltage;
        else
            return element.ACVoltage;
    }
    public bool GetDC()
    {
        return element.DC;
    }
    public int GetConductivity()
    {
        return Convert.ToInt32(element.conductivity);
    }
    public float GetCurrent()
    {
        if (element.DC)
            return element.DCnoLoadCurrent;
        else
            return element.ACnoLoadCurrent;
    }
    public float GetResistance()
    {
        return element.resistance;
    }
    public void ChangeConnected()
    {
        if (contacts[0].enabled && contacts[1].enabled)
        {
            multimetr.measuredElement = this;
            multimetr.ChangeText();
        }
        else
        {
            multimetr.measuredElement = null;
            multimetr.ChangeText();
        }
    }
}
