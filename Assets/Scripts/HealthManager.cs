using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
    public UnityEngine.UI.Image [] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(UnityEngine.UI.Image img in hearts){
            img.sprite = emptyHeart;

        }
        for (int i = 0; i < health; i++){
            hearts[i].sprite = fullHeart;
        }
    }
}
