using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject chestItem;
    private bool chestOpened;
    private bool playerInTrigger;
    private SpriteRenderer sr;

    void Start()
    {
        chestOpened = false;
        playerInTrigger = false;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for input only if the player is inside the trigger
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !chestOpened)
        {
            OpenChest();
        }
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

    public void OpenChest()
    {
        Debug.Log("Open Chest");
        Instantiate(chestItem, transform.position, Quaternion.identity);
        chestOpened = true;
        Destroy(gameObject);
    }
}
