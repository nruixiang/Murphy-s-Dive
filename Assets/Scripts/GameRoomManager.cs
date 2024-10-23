using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>(); // List of rooms (tilemaps/room objects)
    public List<Collider2D> roomWalls = new List<Collider2D>(); // List of colliders that act as walls/doors for each room
    public List<int> enemyAmounts = new List<int>(); // List of enemy counts for each room

    private int currentRoomIndex = 0; // Tracks the current room index
    private bool roomCleared = false;

    void Start()
    {
        InitializeRoom(currentRoomIndex); // Start with the first room
    }

    void Update()
    {
        RoomClearCheck(); // Check if the current room is cleared
    }

    private void InitializeRoom(int roomIndex)
    {
        // Activate the room and enable its walls
        rooms[roomIndex].SetActive(true); // Activate the room's tilemap

        roomWalls[roomIndex].enabled = true; // Enable the walls collider to block the player

        // Initialize enemy count for this room
        if (enemyAmounts[roomIndex] == 0)
        {
            enemyAmounts[roomIndex] = CountEnemiesInRoom(rooms[roomIndex]);
        }

        roomCleared = false; // Reset room cleared status
    }

    private int CountEnemiesInRoom(GameObject room)
    {
        // Count all enemies in the room
        return room.GetComponentsInChildren<Enemy>().Length;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // When the player enters the room and it's cleared
        if (col.gameObject.tag == "Player" && roomCleared)
        {
            Debug.Log("Room Cleared");
            roomWalls[currentRoomIndex].enabled = false; // Disable the room's wall collider to "open" it

            // Move to the next room if available
            if (currentRoomIndex < rooms.Count - 1)
            {
                currentRoomIndex++;
                InitializeRoom(currentRoomIndex);
            }
            else
            {
                Debug.Log("All rooms cleared!");
            }
        }
    }

    private void RoomClearCheck()
    {
        // If no more enemies, mark the room as cleared
        if (enemyAmounts[currentRoomIndex] == 0 && !roomCleared)
        {
            roomCleared = true;
            Debug.Log("Room Cleared! Player can now proceed to the next room.");
        }
    }

    // Called by the enemy when they are defeated
    public void EnemyDefeated()
    {
        if (enemyAmounts[currentRoomIndex] > 0)
        {
            enemyAmounts[currentRoomIndex]--;
        }
    }
}
