using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform bar;
    float ultCharge = 90;
    float ultReq = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ultCharge < ultReq){
            ultCharge += Time.deltaTime;
        }
        SetUltBarState(ultCharge, ultReq);
        
    }
    public void SetUltBarState(float charge, float maxCharge){
        float state = (float)charge;
        state /= maxCharge;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
