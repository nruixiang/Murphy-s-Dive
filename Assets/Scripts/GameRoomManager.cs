using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{

    private HealthManager healthManager;
    public List<GameObject> rooms = new List<GameObject>(); // List of rooms (tilemaps/room objects)
    public List<GameObject> roomWalls = new List<GameObject>(); // List of colliders that act as walls/doors for each room
    public List<int> enemyAmounts = new List<int>(); // List of enemy counts for each room
    //public List<Transform> roomTeleportPoints = new List<Transform>();
    private Dictionary<int, Dictionary<string, Transform>> roomTeleportPoints = new Dictionary<int, Dictionary<string, Transform>>();

    private int currentRoomIndex = 0; // Tracks the current room index
    public bool roomCleared = false;
    public bool isPlayerAlive;
    
    private AudioSource audioSource;
    [SerializeField] AudioClip doorOpen;

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        audioSource = GetComponent<AudioSource>();

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
        for (int i = 0; i < roomWalls.Count; i++)
        {
            SetAllCollidersEnabled(roomWalls[i], i == roomIndex);
        }

        // Initialize enemy count if not already set
        if (enemyAmounts[roomIndex] == 0)
        {
            enemyAmounts[roomIndex] = CountEnemiesInRoom(rooms[roomIndex]);
        }

        SetEnemiesActiveInRoom(roomIndex, true); // Activate enemies in the current room

        roomCleared = false; // Reset cleared status
    }
    public void RegisterTeleportPoint(int roomIndex, string entranceName, Transform tpPoint)
    {
        if (!roomTeleportPoints.ContainsKey(roomIndex))
        {
            roomTeleportPoints[roomIndex] = new Dictionary<string, Transform>();
        }
        roomTeleportPoints[roomIndex][entranceName] = tpPoint;
    }

    private int CountEnemiesInRoom(GameObject room)
    {
        // Count the number of enemies in the room
        return room.GetComponentsInChildren<Enemy>(true).Length;
    }

    // This method is called by the trigger to teleport the player to the specified room
    public void TeleportPlayerToRoom(int roomIndex, GameObject player, string entranceName)
    {
        if (roomIndex >= 0 && roomIndex < rooms.Count)
        {
            // Deactivate the current room's walls
            SetAllCollidersEnabled(roomWalls[currentRoomIndex], false);
            SetEnemiesActiveInRoom(currentRoomIndex, false); // Deactivate current room's enemies

            // Move to the new room
            currentRoomIndex = roomIndex;

            // Teleport the player to the selected room's teleport point
            //player.transform.position = roomTeleportPoints[currentRoomIndex].position;
            if (roomTeleportPoints.ContainsKey(roomIndex) && roomTeleportPoints[roomIndex].ContainsKey(entranceName))
            {
                // Teleport the player to the specified entrance's position
                player.transform.position = roomTeleportPoints[roomIndex][entranceName].position;
            }
            else
            {
                Debug.LogError("Invalid entrance name for teleportation.");
            }

            // Initialize the new room
            InitializeRoom(currentRoomIndex);
        }
        else
        {
            Debug.LogError("Invalid room index for teleportation.");
        }
    }
    private void SetAllCollidersEnabled(GameObject parent, bool isEnabled)
    {
        Collider2D[] colliders = parent.GetComponentsInChildren<Collider2D>(true);
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isEnabled;
            //Debug.Log($"{collider.gameObject.name} collider enabled: {isEnabled}");
        }
        //Some Jank, for some reason I need to reset the whole object for the colliders to work
        parent.SetActive(false);
        parent.SetActive(true);                                                              
    }

    private void RoomClearCheck()
    {
        // Check if the current room is cleared (no enemies left)
        if (enemyAmounts[currentRoomIndex] == 0 && !roomCleared)
        {
            roomCleared = true;
            audioSource.PlayOneShot(doorOpen);
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
