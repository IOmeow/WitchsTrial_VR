using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private Animator letter_animator;
    private bool fly;
    SoundControl sound;
    private bool p;
    GameObject button;

    float envelope_time;
    float envelope_reload_time;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        letter_animator = transform.GetChild(2).gameObject.GetComponent<Animator>();
        fly = false;

        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
        p = false;

        envelope_time = Time.time;
        envelope_reload_time = 5f;

        button = transform.GetChild(4).gameObject;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!p)_open();
            else _close();
        }

        if(fly == true){
            transform.parent.transform.Translate(Vector3.up * Time.deltaTime);
            if(transform.parent.transform.position.y >= 3.0f){
                fly = false;
            }
        }

    }

    public void _open(){
        if (!p && envelope_time+envelope_reload_time < Time.time){
            Debug.Log("envelope opened");
            animator.SetBool("press", true);
            Invoke("openLetter", 2f);
            p = true;
            button.SetActive(true);

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
            button.SetActive(false);

            envelope_time = Time.time;
            sound.playEnvelopeSE();
        }
    }

    public void flytopot(){
        animator.SetInteger("choose", 2);
        letter_animator.SetBool("letter_pos", false);
        button.SetActive(false);
    }

    public void flyaway(){
        fly = true;
    }

    public bool isOpen(){
        return p;
    }

    void openLetter(){
        letter_animator.SetBool("letter_pos", true);
    }
}
