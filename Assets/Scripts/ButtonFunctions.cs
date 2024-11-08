using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CloseGame(){
        Application.Quit();
    }
    public void ResumeGame(){
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager.isPauseMenuLoaded && uiManager.IsSceneLoaded("PauseMenu")) // Only unload if the pause menu is loaded
        {
            Time.timeScale = 1;
            uiManager.paused = false;
            uiManager.isPauseMenuLoaded = false;
            SceneManager.UnloadSceneAsync("PauseMenu");
            Cursor.SetCursor(uiManager.crosshair, Vector2.zero, CursorMode.Auto);
        }
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Cursor.SetCursor(uiManager.cursor, Vector2.zero, CursorMode.Auto);
    }
    public IEnumerator GoGameScene(){
        yield return new WaitForSeconds(2f);
        NextScene();
    }
}
