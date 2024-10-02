using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform bar;
    public static float ultCharge = 0;
    float ultReq = 2;
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
