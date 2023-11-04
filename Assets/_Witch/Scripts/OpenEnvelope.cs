using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEnvelope : MonoBehaviour
{
    public GameObject envelope1;
    AnimationControl ac;
    SoundControl sound;

    void Start()
    {
        ac = envelope1.GetComponent<AnimationControl>();
        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.name == "wax1"){
            if(!ac.isOpen())ac._open();
            else ac._close();
            
            sound.playEnvelopeSE();
        }
    }
}
