using UnityEngine;

public class Laboratory : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public bool inTrigger;
    [SerializeField] private PlayerController controller;
    [HideInInspector] public Interactive[] interactives;
    [SerializeField] Camera cam;
    private void Start()
    {
        interactives = GetComponentsInChildren<Interactive>();
        foreach (Interactive interactive in interactives)
            interactive.enabled = false;
        cam.enabled = false;
    }
    private void Update()
    {
        if (inTrigger)
        {
            canvas.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Return))
            {
                foreach (Interactive interactive in interactives)
                {
                    interactive.enabled = true;
                    this.enabled = false;
                    canvas.SetActive(false);
                    controller.enabled = false;
                    controller.gameObject.SetActive(false);
                    cam.enabled = true;
                }
            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
