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

    void Start()
    {
        chestOpened = false;
        playerInTrigger = false;
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
            Debug.Log("Player entered chest trigger");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInTrigger = false;
            Debug.Log("Player exited chest trigger");
        }
    }

    public void OpenChest()
    {
        Debug.Log("Open Chest");
        Instantiate(chestItem, transform.position, Quaternion.identity);
        chestOpened = true;
    }
}
