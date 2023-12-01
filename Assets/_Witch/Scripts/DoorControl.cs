using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Animator animator;
    GameObject grab;

    void Start(){
        grab = GameObject.Find("door_grab");
        animator = gameObject.GetComponent<Animator>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.D))OpenDoor();
    }

    public void OpenDoor(){
        Debug.Log("Open Door");
        Destroy(grab);
        animator.SetBool("open", true);
        GameManager.instance.toClassrom();
    }
}