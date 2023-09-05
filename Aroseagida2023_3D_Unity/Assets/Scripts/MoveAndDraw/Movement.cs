using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region 기존 코드
    /*
    public Vector2 turn;
    public float sensitiv = 0.5f;
    public Vector3 deltaMove;
    public float speed = 1;
    public GameObject Mover;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        turn.x += Input.GetAxis("Mouse X") * sensitiv;
        turn.y += Input.GetAxis("Mouse Y") * sensitiv;
        //Mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        deltaMove = new Vector3(Input.GetAxisRaw("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxisRaw("Vertical")*speed*Time.deltaTime);
        transform.Translate(deltaMove);


        
    }
    */
    #endregion


    
    public float speed = 5f;
 
    void Update(){
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
    }
    
    

}
