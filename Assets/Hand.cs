using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    public float animationSpeed;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private const float threshold = 0.01f;

    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform followTarget;
    private Rigidbody body;

    private Collider handCollider;
    private Collider grabbedObjectCollider;

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        body.maxAngularVelocity = 20f;

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

        // Get the hand collider
        handCollider = GetComponent<Collider>();
    }

    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.linearVelocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
    }

    void AnimateHand()
    {
        if (MathF.Abs(gripCurrent - gripTarget) > threshold)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (Mathf.Abs(triggerCurrent - triggerTarget) > threshold)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }

    public void GrabObject(GameObject grabbedObject)
    {
        if (grabbedObject.TryGetComponent(out Collider objectCollider))
        {
            grabbedObjectCollider = objectCollider;
            Physics.IgnoreCollision(handCollider, grabbedObjectCollider, true); // Ignore collision
        }
    }

    public void ReleaseObject()
    {
        if (grabbedObjectCollider != null)
        {
            Physics.IgnoreCollision(handCollider, grabbedObjectCollider, false); // Restore collision
            grabbedObjectCollider = null;
        }
    }
}
