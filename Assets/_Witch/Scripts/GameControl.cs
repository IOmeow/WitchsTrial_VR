using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{   // 拿起法杖→（教學+練習）→場景→飛進信紙→決定場景→3次施咒→藥水動畫
    MagicController magic;
    PotControl pot;
    FadeControl fade;

    bool pickUp = false;
    GameObject hint;
    int tutorial_magic = 0, scene_choose = 0;

    GameObject method, space;

    void Start()
    {
        magic = GameObject.Find("stick").GetComponent<MagicController>();
        hint = GameObject.Find("TrackHint");
        hint.SetActive(false);

        space = GameObject.Find("=== Space ===");
        space.SetActive(false);
        method = GameObject.Find("=== Method ===");
        method.SetActive(false);

        pot = gameObject.GetComponent<PotControl>();

        fade = GameObject.Find("Fade").GetComponent<FadeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))pickUp=true;
        if(Input.GetKeyDown(KeyCode.O))tutorial_magic=2;

        if(!pickUp)return;  //還沒拿法仗
        else if(tutorial_magic<2){
            hint.SetActive(true);
        }
        else if(tutorial_magic==2){
            tutorial_magic++;
            fade.FadeOut();
            Invoke("gameStart", 3f);
        }
    }
    void gameStart(){
        method.SetActive(true);
        space.SetActive(true);
        pot.tutorialEnd();
        fade.FadeIn();
    }

    public void choose_scene(int scene){
        scene_choose = scene;
    }
    public void pickup_stick(){
        pickUp = true;
    }
    public void AddTutorialMagic(){
        if(tutorial_magic<2)tutorial_magic++;
        // Debug.Log("magicfinish");
    }
}
