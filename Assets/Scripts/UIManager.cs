using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform bar;
    public static float ultCharge = 0;
    float ultReq = 5;
    public static bool ultReady;
    // Start is called before the first frame update
    void Start()
    {
        ultReady = false;
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
    }
    public void SetUltBarState(float charge, float maxCharge){
        float state = (float)charge;
        state /= maxCharge;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, bar.localScale.y, 1f);
    }
}
