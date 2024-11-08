using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int targetRoomIndex; // The room to teleport the player to
    public string entranceName;
    public GameRoomManager gameRoomManager; // Reference to the central room manager
    public Transform teleportPoint;
    private void Start(){
        if(gameRoomManager != null && teleportPoint != null){
            gameRoomManager.RegisterTeleportPoint(targetRoomIndex, entranceName, teleportPoint);
        }else{
            Debug.LogWarning("GameRoomManager or teleportPoint not set on RoomTrigger.");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && gameRoomManager.roomCleared)
        {
            int bulletLayer = LayerMask.NameToLayer("Bullet");
            GameObject[] bullets = FindObjectsOfType<GameObject>(); // Find all active GameObjects
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            GameObject[] enemys = FindObjectsOfType<GameObject>();
            
            foreach (GameObject bullet in bullets)
            {
                if (bullet.layer == bulletLayer)
                {
                    Destroy(bullet);
                }
            }
            foreach(GameObject enemy in enemys){
                if(enemy.layer == enemyLayer){
                    Destroy(enemy);
                }
            }
            // Ask the GameRoomManager to teleport the player to the specific room
            gameRoomManager.TeleportPlayerToRoom(targetRoomIndex, col.gameObject, entranceName);
        }
    }
}
