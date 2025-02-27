using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PillarMovement : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    public Transform pillar;
    public Vector3 targetPosition;
    public float moveSpeed = 2.0f;

    private void Start()
    {
        socket.selectEntered.AddListener(OnObjectPlaced);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        StopAllCoroutines();
        StartCoroutine(MovePillar());
    }

    private System.Collections.IEnumerator MovePillar()
    {
        float startY = pillar.position.y;
        float targetY = targetPosition.y;
        float time = 0.2f;

        while (Mathf.Abs(startY - targetY) > 0.01f)
        {
            time += Time.deltaTime * moveSpeed;
            float newY = Mathf.Lerp(startY, targetY, time);
            pillar.position = new Vector3(pillar.position.x, newY, pillar.position.z);
            yield return null;
        }
    }
}
