using UnityEngine;
using System;
public class Multimetr : MonoBehaviour
{

    [SerializeField] private Transform circle;
    [SerializeField] private TMPro.TMP_Text _textMeshPro;
    [HideInInspector] public ElectricityComponent measuredElement;
    float epsilon = 0.01f;
    float DCvoltage = 0.0f;
    float DCcurrent = 0.0f;

    float ACvoltage = 0.0f;
    float ACcurrent = 0.0f;
    float resistance = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (circle.eulerAngles.y != lastAgle)
            ChangeText();
        lastAgle = circle.eulerAngles.y;
    }
    private float lastAgle = 0f;
    public void ChangeText()
    {
        if (measuredElement != null)
        {
            if (measuredElement.GetDC())
            {
                DCvoltage = measuredElement.GetVoltage();
                DCcurrent = measuredElement.GetCurrent();
                ACvoltage = 0; //хз как верно
                ACcurrent = 0; //хз как верно
            }
            else
            {
                ACvoltage = measuredElement.GetVoltage();
                ACcurrent = measuredElement.GetCurrent();
                DCvoltage = 0; //хз как верно
                DCcurrent = 0; //хз как верно
            }
            resistance = measuredElement.GetResistance();
        }
        else {
            ACvoltage = 0.0f;
            DCvoltage = 0.0f;
            ACcurrent = 1f;
            DCcurrent = 1f;
            resistance = 1f;
        }
        string text = "";
        switch (Math.Round(circle.eulerAngles.y))
        {
            case 0:
                text = "";
                break;
            case 18:
                text = $"{DCvoltage = (DCvoltage > 0.2f?1: DCvoltage * 100)}";
                break;
            case 36:
                text = $"{DCvoltage = (DCvoltage > 2f ? 1  : DCvoltage * 1000)}";
                break;
            case 54:
                text = $"{DCvoltage = (DCvoltage > 20f ? 1 : DCvoltage)}";
                break;
            case 72:
                text = $"{DCvoltage = (DCvoltage > 200f ? 1 : DCvoltage)}";
                break;
            case 90:
                text = $"HV {DCvoltage = (DCvoltage > 600f ? 1 : DCvoltage)}";
                break;
            case 108:
                text = $"HV {ACvoltage = (ACvoltage > 600f ? 1 : ACvoltage)}";
                break;
            case 126:
                text = $"HV {ACvoltage = (ACvoltage > 200f ? 1 : ACvoltage)}";
                break;
            case 144:
                text = "-";
                break;
            case 162:
                goto case 216;
            case 180:
                goto case 216;
            case 198:
                goto case 216;
            case 216:
                text = $"{ACcurrent}";
                break;
            case 234:
                text = $"{measuredElement.GetConductivity()}";
                break;
            case 252:
                goto case 342;
            case 270:
                goto case 342;
            case 288:
                goto case 342;
            case 306:
                goto case 342;
            case 324:
                goto case 342;
            case 342:
                text = $"{resistance}";
                break;
            default:
                text = $"";
                break;
        }
        _textMeshPro.text = text;
    }
}
