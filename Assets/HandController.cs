using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private InputActionReference triggerAction;
    [SerializeField] private Hand hand;
    void Start()
    {
        if (hand == null)
        {
       hand = GetComponentInChildren<Hand>(); 
    }
    }

    // Update is called once per frame
    void Update()
    {
        if (hand == null) return;
        hand.SetGrip(gripAction.action.ReadValue<float>());
        hand.SetTrigger(triggerAction.action.ReadValue<float>());
    }
}