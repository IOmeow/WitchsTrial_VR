using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    MagicController magic;
    ProjectionControl projection;
    GameObject hint;
    GameObject SceneCorridor, SceneClassroom;
    GameObject black_board;

    TrialControl trial1, trial2;

    void Start()
    {
        magic = GameObject.Find("stick").GetComponent<MagicController>();
        projection = GameObject.Find("Projection").GetComponent<ProjectionControl>();
        black_board = GameObject.Find("black_board");
        foreach (Transform child in black_board.transform){
            child.gameObject.SetActive(false);
        }

        SceneCorridor = GameObject.Find("Corridor");
        SceneClassroom = GameObject.Find("Classroom");
        SceneClassroom.transform.position = new Vector3(5000f, 0f,0f);

        instance = this;

        trial1 = GameObject.Find("trial1").GetComponent<TrialControl>();
        trial1 = GameObject.Find("trial2").GetComponent<TrialControl>();
    }

    int magic_counter=0;  //0:tutorial
    public void endMagic(){
        if(magic_counter==0){
            finishTutorial();
        }
        magic_counter++;
    }

    // 到教室場景
    public void toClassrom(){
        Invoke("fadeToStart", 3f);
    }
    void fadeToStart(){
        OVRScreenFade.instance.FadeOut();
        Invoke("gameStart", 3f);
    }
    void gameStart(){
        SceneCorridor.transform.position = new Vector3(5000f, 0f,0f);
        SceneClassroom.transform.position = Vector3.zero;
        OVRScreenFade.instance.FadeIn();
        black_board.transform.GetChild(0).gameObject.SetActive(true);
    }

    // 拿起法杖
    bool picked = false;
    public void pickUp(){
        if(picked)return;
        picked = true;
        black_board.transform.GetChild(0).gameObject.SetActive(false);
        black_board.transform.GetChild(1).gameObject.SetActive(true);
        magic.startHint();
    }
    void finishTutorial(){
        black_board.transform.GetChild(1).gameObject.SetActive(false);
        black_board.transform.GetChild(2).gameObject.SetActive(true);
        projection.OpenProjection();
    }

    public void startToMagic(){
        magic.startHint();
    }

    public void ChangePage(){
        if()trial1.changePage();
    }
}
