using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public DoorControl door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();
        }
    }
}
