using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform bar;
    public static float ultCharge = 0;
    float ultReq = 5;
    public static bool ultReady;
    private bool paused;
    private bool isPauseMenuLoaded;

    // Start is called before the first frame update
    void Start()
    {
        ultReady = false;
        paused = false;
        isPauseMenuLoaded = false;
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

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            HealthManager.health = 3;
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
    private void PauseGame()
    {
        if (!isPauseMenuLoaded) // Only load if the pause menu is not already loaded
        {
            Time.timeScale = 0;
            paused = true;
            isPauseMenuLoaded = true;
            SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
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
        }
    }

    // Check if a scene is currently loaded
    private bool IsSceneLoaded(string sceneName)
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
    
}
