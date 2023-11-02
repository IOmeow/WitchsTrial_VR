using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private bool p;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        p = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!p)_open();
            else _close();
        }

    }

    public void _open(){
        if (!p){
            Debug.Log("envelope opened");
            animator.SetBool("press", true);
            p = true;
        }
    }

    public void _close(){
        if(p){
            Debug.Log("envelope closed");
            animator.SetBool("press", false);
            p = false;
        }
    }

    public bool isOpen(){
        return p;
    }
}
