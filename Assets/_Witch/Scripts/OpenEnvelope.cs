using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEnvelope : MonoBehaviour
{
    AnimationControl ac1, ac2, ac3, ac4;
    bool opening = false;
    MagicController magic;

    GameControl gc;

    void Awake()
    {
        ac1 = GameObject.Find("envelope1").transform.GetChild(0).gameObject.GetComponent<AnimationControl>();
        ac2 = GameObject.Find("envelope2").transform.GetChild(0).gameObject.GetComponent<AnimationControl>();
        ac3 = GameObject.Find("envelope3").transform.GetChild(0).gameObject.GetComponent<AnimationControl>();
        ac4 = GameObject.Find("envelope4").transform.GetChild(0).gameObject.GetComponent<AnimationControl>();

        gc = GameObject.Find("=== System ===").GetComponent<GameControl>();
        magic = GameObject.Find("stick").GetComponent<MagicController>();
    }


    void OnTriggerEnter(Collider other)
    {
        switch(other.name){
        case "wax1":
            open_letter(ac1);
            break;
        case "wax2":
            open_letter(ac2);
            break;
        case "wax3":
            open_letter(ac3);
            break;
        case "wax4":
            open_letter(ac4);
            break;
        case "no":
            close_letter();
            break;
        case "yes":
            if(ac1.isOpen()){
                ac1.flytopot();ac2.flyaway();ac3.flyaway();ac4.flyaway();
            }
            if(ac2.isOpen()){
                ac2.flytopot();ac1.flyaway();ac3.flyaway();ac4.flyaway();
            }
            if(ac3.isOpen()){
                ac3.flytopot();ac1.flyaway();ac2.flyaway();ac4.flyaway();
            }
            if(ac4.isOpen()){
                ac4.flytopot();ac1.flyaway();ac2.flyaway();ac3.flyaway();
            }
            magic.startHintDelay(3f);
            break;
        }
    }

    void open_letter(AnimationControl ac){
        opening = ac1.isOpen()||ac2.isOpen()||ac3.isOpen()||ac4.isOpen();
        if(!opening)ac._open();
    }

    void close_letter(){
        if(ac1.isOpen())ac1._close();
        if(ac2.isOpen())ac2._close();
        if(ac3.isOpen())ac3._close();
        if(ac4.isOpen())ac4._close();
    }
}
