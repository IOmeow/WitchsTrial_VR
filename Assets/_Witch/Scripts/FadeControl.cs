using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    private Animator animator;
    bool isBlack = true;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Invoke("FadeIn", 3f);
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
