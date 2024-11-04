using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHECK : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject player;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~LayerMask.GetMask("Camera", "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Transform child = this.gameObject.transform.GetChild(0);
        RaycastHit2D hit = Physics2D.Raycast(child.transform.position, player.transform.position - transform.position, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            // Check if the ray hit the player
            if (hit.collider.gameObject.tag == "Player")
            {
                    Debug.Log("Player is in line of sight.");
                    // Additional behavior, e.g., setting a state or triggering an alert
            }
            else
            {
                    Debug.Log("Player is not in line of sight, hit: " + hit.collider.gameObject.name);
            }
        } else{
            Debug.Log("hit nothing");
        }
        
        // Optional: Draw a debug line in the editor for visualization
        Debug.DrawRay(child.transform.position, player.transform.position - transform.position, Color.red);
    }
}
