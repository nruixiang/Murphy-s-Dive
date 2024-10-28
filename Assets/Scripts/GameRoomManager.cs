using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{
    private HealthManager healthManager;
    public List<GameObject> rooms = new List<GameObject>(); // List of rooms (tilemaps/room objects)
    public List<GameObject> roomWalls = new List<GameObject>(); // List of colliders that act as walls/doors for each room
    public List<int> enemyAmounts = new List<int>(); // List of enemy counts for each room
    public List<Transform> roomTeleportPoints = new List<Transform>();

    private int currentRoomIndex = 0; // Tracks the current room index
    public bool roomCleared = false;
    [SerializeField] private int targetRoomIndex; //Choose which room you want the trigger to teleport you to

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        InitializeRoom(currentRoomIndex); // Start with the first room

        for (int i = 1; i < rooms.Count; i++)
        {
            SetEnemiesActiveInRoom(i, false); // Deactivate enemies in other rooms
        }
    }

    void Update()
    {
        RoomClearCheck(); // Check if the current room is cleared
    }

    private void InitializeRoom(int roomIndex)
    {
        // Activate the current room and enable its walls
        rooms[roomIndex].SetActive(true); // Activate room tilemap
        roomWalls[roomIndex].GetComponentInChildren<Collider2D>().enabled = true; // Enable room walls

        // Initialize enemy count if not already set
        if (enemyAmounts[roomIndex] == 0)
        {
            enemyAmounts[roomIndex] = CountEnemiesInRoom(rooms[roomIndex]);
        }

        SetEnemiesActiveInRoom(roomIndex, true); // Activate enemies in the current room

        roomCleared = false; // Reset cleared status
    }

    private int CountEnemiesInRoom(GameObject room)
    {
        // Count the number of enemies in the room
        return room.GetComponentsInChildren<Enemy>(true).Length;
    }

    // This method is called by the trigger to teleport the player to the specified room
    public void TeleportPlayerToRoom(int roomIndex, GameObject player)
    {
        if (roomIndex >= 0 && roomIndex < rooms.Count)
        {
            // Deactivate the current room's walls
            roomWalls[currentRoomIndex].GetComponentInChildren<Collider2D>().enabled = false;
            SetEnemiesActiveInRoom(currentRoomIndex, false); // Deactivate current room's enemies

            // Move to the new room
            currentRoomIndex = roomIndex;

            // Teleport the player to the selected room's teleport point
            player.transform.position = roomTeleportPoints[currentRoomIndex].position;

            // Initialize the new room
            InitializeRoom(currentRoomIndex);
        }
        else
        {
            Debug.LogError("Invalid room index for teleportation.");
        }
    }

    private void RoomClearCheck()
    {
        // Check if the current room is cleared (no enemies left)
        if (enemyAmounts[currentRoomIndex] == 0 && !roomCleared)
        {
            roomCleared = true;
            Debug.Log("Room Cleared! Player can now proceed to the next room.");
        }
    }

    // This function is called by enemies when they are defeated
    public void EnemyDefeated()
    {
        if (enemyAmounts[currentRoomIndex] > 0)
        {
            enemyAmounts[currentRoomIndex]--;
        }
    }

    private void SetEnemiesActiveInRoom(int roomIndex, bool isActive)
    {
        // Activate/deactivate enemies based on the isActive flag
        Enemy[] enemies = rooms[roomIndex].GetComponentsInChildren<Enemy>(true);
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(isActive);
        }
    }
    public List<Enemy> GetEnemiesInCurrentRoom()
    {
        List<Enemy> activeEnemies = new List<Enemy>();
        Enemy[] enemies = rooms[currentRoomIndex].GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeSelf) // Only add active enemies
            {
                activeEnemies.Add(enemy);
            }
        }
        return activeEnemies;
    }
       
    
}
