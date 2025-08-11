using UnityEngine;
using TMPro;

public class LightingText : Interactive
{
    [SerializeField] TMP_Text text;
    // Update is called once per frame
    public void Enable()
    {
        if(text.color == Color.yellow)
            text.color = Color.black;
        else
            text.color = Color.yellow;
        
    }
}
