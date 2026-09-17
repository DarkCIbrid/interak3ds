using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportDestination != null)
            {
                other.transform.position = teleportDestination.position;
                Debug.Log("Teleportiran!");
            }
            else
            {
                Debug.LogWarning("Nema postavljene teleport destinacije!");
            }
        }
    }
}
