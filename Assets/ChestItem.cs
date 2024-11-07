using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : MonoBehaviour
{
    public LightningRod lightningBolt;
    public ExplosiveBow explosiveBow;
    public float jumpHeight = 2f;       // Height of the jump
    public float jumpDuration = 0.5f;   // Time to complete the jump

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime;
    private bool playerInTrigger;
    private SpriteRenderer sr;

    void Awake()
    {
        // Set the start and target positions based on the instantiated position
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * jumpHeight;
    }
    void Start(){
        playerInTrigger = false;
        lightningBolt = FindObjectOfType<LightningRod>();
        explosiveBow = FindObjectOfType<ExplosiveBow>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float normalizedTime = Mathf.Clamp01(elapsedTime / jumpDuration);

        // Calculate Y position along the jump arc
        float yPosition = Mathf.Lerp(startPosition.y, targetPosition.y, Mathf.Sin(normalizedTime * Mathf.PI));
        transform.position = new Vector3(startPosition.x, yPosition, startPosition.z);

        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ItemPickedUp();
        }
    }

    public void ItemPickedUp(){
        lightningBolt.enabled = false;
        explosiveBow.enabled = true;
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInTrigger = true;
            sr.color = Color.green;
            Debug.Log("Player entered chest trigger");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInTrigger = false;
            sr.color = Color.white;
            Debug.Log("Player exited chest trigger");
        }
    }
}
