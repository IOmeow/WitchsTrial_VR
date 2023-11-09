using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{   // 拿起法杖→（教學+練習）→飛進信紙→決定場景→3次施咒→藥水動畫
    MagicController magic;

    bool start = false;
    int scene_choose = 0;
    void Start()
    {
        magic = GameObject.Find("stick").GetComponent<MagicController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!start && scene_choose!=0){
            start = true;
            magic.startHint();
        }
    }

    public void choose_scene(int scene){
        scene_choose = scene;
    }
}
