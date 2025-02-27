using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isOpening;
    private bool isClosing;

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(0, moveDistance, 0);   
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        if (isClosing)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        isClosing = false;
    }

    public void CloseDoor()
    {
        isClosing = true;
        isOpening = false;
    }
}
