using UnityEngine;

public class FallReset : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetZone"))
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            transform.position = respawnPoint.position;
        }
    }
}
