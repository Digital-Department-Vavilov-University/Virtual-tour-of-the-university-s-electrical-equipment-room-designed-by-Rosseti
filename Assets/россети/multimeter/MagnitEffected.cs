using UnityEngine;

public class MagnitEffected : Interactive
{
    private Outline outline;
    private bool enable;

    void Start()
    {
        outline = GetComponent<Outline>();
    }
    public void Play()
    {
        transform.parent.GetComponent<ElectricityComponent>().ChangeConnected();
        enable = true;
    }
    public void Stop()
    {
        transform.parent.GetComponent<ElectricityComponent>().ChangeConnected();
        enable = false;
    }

    private void Update()
    {
        if (enable)
        {
            outline.OutlineColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
            outline.OutlineWidth = Mathf.Lerp(0f, 10f, Mathf.PingPong(Time.time, 1));
        }
        outline.enabled = enable;
    }
}
