using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    private Animator animator;
    bool isBlack = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void FadeOut(){
        if(!isBlack){
            animator.SetBool("toBlack", true);
            isBlack = true;
        }
    }
    public void FadeIn(){
        if(isBlack){
            animator.SetBool("toBlack", false);
            isBlack = false;
        }
    }
}
