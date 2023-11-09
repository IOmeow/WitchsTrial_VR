using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEnvelope : MonoBehaviour
{
    GameObject envelopes;
    AnimationControl ac1, ac2, ac3, ac4;
    bool opening = false;

    GameControl gc;

    void Awake()
    {
        envelopes = GameObject.Find("Envelope");
        ac1 = envelopes.transform.GetChild(0).gameObject.GetComponent<AnimationControl>();
        ac2 = envelopes.transform.GetChild(1).gameObject.GetComponent<AnimationControl>();
        ac3 = envelopes.transform.GetChild(2).gameObject.GetComponent<AnimationControl>();
        ac4 = envelopes.transform.GetChild(3).gameObject.GetComponent<AnimationControl>();

        gc = GameObject.Find("=== System ===").GetComponent<GameControl>();
    }


    void OnTriggerEnter(Collider other)
    {
        switch(other.name){
        case "wax1":
            open_or_close(ac1);
            break;
        case "wax2":
            open_or_close(ac2);
            break;
        case "wax3":
            open_or_close(ac3);
            break;
        case "wax4":
            open_or_close(ac4);
            break;
        }
        // if(ac1.isOpen() && other.name=="paper1")choose_scene(1);
        // if(ac2.isOpen() && other.name=="paper2")choose_scene(2);
        // if(ac3.isOpen() && other.name=="paper3")choose_scene(3);
        // if(ac4.isOpen() && other.name=="paper4")choose_scene(4);

    }

    void open_or_close(AnimationControl ac){
        opening = ac1.isOpen()||ac2.isOpen()||ac3.isOpen()||ac4.isOpen();
        if(!opening)ac._open();
        else ac._close();
    }

    void choose_scene(int scene){
        //light for 3 sec and choose

    }
}
