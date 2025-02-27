using UnityEngine;

public class RotateGame : MonoBehaviour
{
    public GameObject paintingPivot;
    public Transform cylinder;
    public float rotationMultiplier = 1f;
    public float rotationSpeed = 5f;

    private Quaternion initialCylinderRotation;
    private Quaternion initialPaintingRotation;

    private void Start()
    {
        initialCylinderRotation = cylinder.rotation;
        initialPaintingRotation = paintingPivot.transform.rotation;
    }

    private void Update()
    {
        // Calculate the difference in rotation between the current and initial cylinder rotations
        Quaternion cylinderDelta = Quaternion.Inverse(initialCylinderRotation) * cylinder.rotation;

        // Scale the rotation by the multiplier
        Quaternion targetRotation = initialPaintingRotation * Quaternion.Slerp(Quaternion.identity, cylinderDelta, rotationMultiplier);

        // Smoothly interpolate the painting's rotation
        paintingPivot.transform.rotation = Quaternion.Slerp(paintingPivot.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
