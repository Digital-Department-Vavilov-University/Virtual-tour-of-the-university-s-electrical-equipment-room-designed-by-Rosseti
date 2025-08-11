using UnityEngine;

[CreateAssetMenu(fileName = "electricityElement", menuName = "Electicity/New element", order = 1)]
public class ElectricityElement : ScriptableObject
{
    public string prefabName;

    [Header("DC ���������")]
    public float DCVoltage;
    public float DCmaxVolage;
    public float DCminVolage;
    [Header("AC ���������")]
    public float ACVoltage;
    public float ACmaxVolage;
    public float ACminVolage;
    [Space(1f)]
    [Header("DC ���� ����")]
    public float DCMaxCurrent;
    public float DCoperationCurrent;
    public float DCnoLoadCurrent;
    [Header("AC ���� ����")]
    public float ACMaxCurrent;
    public float ACoperationCurrent;
    public float ACnoLoadCurrent;
    [Space(1f)]
    [Header("�������������")]
    public float resistance;
    [Space(1f)]
    [Header("���������")]
    public bool conductivity = true;
    [Header("AC/DC")]
    public bool DC = true;
}