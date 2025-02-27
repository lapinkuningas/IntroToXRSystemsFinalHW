using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandGrabber : MonoBehaviour
{
    private Hand hand;
    private Collider handCollider;
    private Collider grabbedObjectCollider;

    private void Start()
    {
        hand = GetComponent<Hand>();
        handCollider = GetComponent<Collider>(); // Ensure the hand has a collider
    }

    private void OnSelectEntered(XRBaseInteractable interactable)
    {
        if (hand != null)
        {
            hand.GrabObject(interactable.gameObject);

            // Get the collider of the grabbed object
            grabbedObjectCollider = interactable.GetComponent<Collider>();
            if (handCollider != null && grabbedObjectCollider != null)
            {
                Physics.IgnoreCollision(handCollider, grabbedObjectCollider, true); // Disable collision
            }
        }
    }

    private void OnSelectExited(XRBaseInteractable interactable)
    {
        if (hand != null)
        {
            hand.ReleaseObject();

            // Restore collision after release
            if (handCollider != null && grabbedObjectCollider != null)
            {
                Physics.IgnoreCollision(handCollider, grabbedObjectCollider, false);
            }
            grabbedObjectCollider = null;
        }
    }
}
