using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private Animator letter_animator;
    SoundControl sound;
    private bool p;

    float envelope_time;
    float envelope_reload_time;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        letter_animator = transform.GetChild(2).gameObject.GetComponent<Animator>();

        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
        p = false;

        envelope_time = Time.time;
        envelope_reload_time = 5f;
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
        if (!p && envelope_time+envelope_reload_time < Time.time){
            Debug.Log("envelope opened");
            animator.SetBool("press", true);
            Invoke("openLetter", 2f);
            p = true;

            envelope_time = Time.time;
            sound.playEnvelopeSE();
        }
    }

    public void _close(){
        if(p && envelope_time+envelope_reload_time < Time.time){
            Debug.Log("envelope closed");
            animator.SetBool("press", false);
            letter_animator.SetBool("letter_pos", false);
            p = false;

            envelope_time = Time.time;
            sound.playEnvelopeSE();
        }
    }

    public bool isOpen(){
        return p;
    }

    void openLetter(){
        letter_animator.SetBool("letter_pos", true);
    }
}
