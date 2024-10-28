using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int targetRoomIndex; // The room to teleport the player to
    public GameRoomManager gameRoomManager; // Reference to the central room manager

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && gameRoomManager.roomCleared)
        {
            // Ask the GameRoomManager to teleport the player to the specific room
            gameRoomManager.TeleportPlayerToRoom(targetRoomIndex, col.gameObject);
        }
    }
}
