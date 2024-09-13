using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float followSpeed = 2f;
    [SerializeField] float yOffSet = 1f;
    public Transform target;
    public bool wallNear = false;
    public float xMin, xMax, yMin, yMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wallNear == false){
            Vector3 newPos = new Vector3(target.position.x, target.position.y +yOffSet, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Wall"){
            wallNear = true;
            Debug.Log("Bump");
        }
    }
    public void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Wall"){
            wallNear = false;
            Debug.Log("Not Bump");
        }
    }
    private void UpdateCameraBounds(Bounds wallBounds){
        
    }
}