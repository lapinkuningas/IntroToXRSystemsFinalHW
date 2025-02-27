using UnityEngine;

public class DoorClose : MonoBehaviour
{
    public DoorControl door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.CloseDoor();
        }
    }
}
