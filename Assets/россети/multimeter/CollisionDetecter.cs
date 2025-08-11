using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    MagnitEffected effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<MagnitEffected>(out effect))
        {
            effect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (effect != null)
        {
            effect.Stop();
        }
    }
}
