using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WallBall : MonoBehaviour
{
    public GameObject button;
    public Material material;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    public float cooldownTime = 10f;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
            StartCoroutine(Cooldown());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isPressed = false;
    }

    public void MakeBall()
    {
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        ball.transform.position = button.transform.position + new Vector3(4, 8, 0);
        Rigidbody rb = ball.AddComponent<Rigidbody>();
        rb.mass = 10.0f;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        ball.AddComponent<XRGrabInteractable>();

        Renderer ballRenderer = ball.GetComponent<Renderer>();
        ballRenderer.material = material;
    }

}
