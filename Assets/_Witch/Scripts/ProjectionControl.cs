using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionControl : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = gameObject.GetComponent<Animator>();
    }
    // void Update(){
    //     if(Input.GetKeyDown(KeyCode.P))OpenProjection();
    // }

    public void OpenProjection(){
        Debug.Log("Open Projectoin");
        animator.SetBool("open", true);
        SoundControl.instance.playProjectionSE();
    }
}