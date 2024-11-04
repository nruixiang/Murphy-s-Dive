using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : MonoBehaviour
{
    public float jumpHeight = 2f;       // Height of the jump
    public float jumpDuration = 0.5f;   // Time to complete the jump

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime;

    void Awake()
    {
        // Set the start and target positions based on the instantiated position
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * jumpHeight;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float normalizedTime = Mathf.Clamp01(elapsedTime / jumpDuration);

        // Calculate Y position along the jump arc
        float yPosition = Mathf.Lerp(startPosition.y, targetPosition.y, Mathf.Sin(normalizedTime * Mathf.PI));
        transform.position = new Vector3(startPosition.x, yPosition, startPosition.z);

        if (normalizedTime >= 1f)
        {
            Destroy(this);
        }
    }
}
