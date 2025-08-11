using UnityEngine;

public class Rotate : Interactive
{
    [SerializeField] GameObject arrows;
    private float mouseSensitivity = 100f;
    private int step = 18;
    [SerializeField] private DraggingWire[] draggingWire;
    
    private void Start()
    {
        
    }
    private void OnMouseOver()
    {
        if (enabled && !draggingWire[0].IsDragging && !draggingWire[1].IsDragging)
        {
            arrows.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0)) //если зажата ЛКМ
            {
                transform.Rotate(0f, 0f, Input.GetAxis("Mouse X") * mouseSensitivity); //обьект вращаеться по z относительно перемещения мышки по вектору x
                transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * mouseSensitivity);
            }
        }
    }
    private void OnMouseUp()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, transform.eulerAngles.y - (transform.eulerAngles.y % step));
    }
    private void OnMouseExit()
    {
        arrows.SetActive(false);
    }
}
