using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Texture2D crosshair;
    public Texture2D cursor;
    [SerializeField] Transform bar;
    public static float ultCharge;
    public float ultReq;
    public static bool ultReady;
    public bool paused;
    public bool isPauseMenuLoaded;
    public bool isGameOverLoaded;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUi();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ultCharge < ultReq){
            ultCharge += Time.deltaTime;
        }
        SetUltBarState(ultCharge, ultReq);
        
        if(ultCharge >= ultReq){
            ultReady = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) // Only triggers when the Escape key is pressed down
        {
            if (!paused)
            {
                PauseGame(); // Pauses the game and loads the pause menu
            }
            else
            {
                ResumeGame(); // Resumes the game and unloads the pause menu
            }
        }
    }
    public void SetUltBarState(float charge, float maxCharge){
        float state = (float)charge;
        state /= maxCharge;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, bar.localScale.y, 1f);
    }
    public void PauseGame()
    {
        if (!isPauseMenuLoaded) // Only load if the pause menu is not already loaded
        {
            Time.timeScale = 0;
            paused = true;
            isPauseMenuLoaded = true;
            SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    private void ResumeGame()
    {
        if (isPauseMenuLoaded && IsSceneLoaded("PauseMenu")) // Only unload if the pause menu is loaded
        {
            Time.timeScale = 1;
            paused = false;
            isPauseMenuLoaded = false;
            SceneManager.UnloadSceneAsync("PauseMenu");
            Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
        }
    }

    // Check if a scene is currently loaded
    public bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
    public void GameOver(){
        if (!isGameOverLoaded) // Only load if the pause menu is not already loaded
        {
            Time.timeScale = 0;
            paused = true;
            isGameOverLoaded = true;
            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
    private void InitializeUi(){
        ultCharge = 0;
        ultReq = 30;
        ultReady = false;
        paused = false;
        isPauseMenuLoaded = false;
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
    }
}
