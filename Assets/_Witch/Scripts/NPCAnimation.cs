using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    public void animateStart(){
        animator.enabled = true;
    }

    public void toBlackBoard(){
        animator.SetBool("toBlackboard", true);
    }
    public void returnFromBlackBoard(){
        animator.SetBool("toBlackboard", false);
    }
}
