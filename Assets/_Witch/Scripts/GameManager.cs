using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool debug = true;
    public float score = 0f;
    public float score1 = 0f, score2 = 0f;

    MagicController magic;
    ProjectionControl projection;
    GameObject hint;
    GameObject SceneCorridor, SceneClassroom;
    GameObject black_board;

    TrialControl trial1, trial2, ending;
    TrialFinishControl frame, file;

    void Start()
    {
        magic = GameObject.Find("stick").GetComponent<MagicController>();
        projection = GameObject.Find("Projection").GetComponent<ProjectionControl>();
        black_board = GameObject.Find("black_board_context");
        foreach (Transform child in black_board.transform){
            child.gameObject.SetActive(false);
        }

        SceneCorridor = GameObject.Find("Corridor");
        SceneClassroom = GameObject.Find("Classroom");
        SceneClassroom.transform.position = new Vector3(5000f, 0f,0f);

        instance = this;

        trial1 = GameObject.Find("trial1").GetComponent<TrialControl>();
        trial2 = GameObject.Find("trial2").GetComponent<TrialControl>();
        ending = GameObject.Find("ending").GetComponent<TrialControl>();

        frame = GameObject.Find("frame").GetComponent<TrialFinishControl>();
        file = GameObject.Find("file").GetComponent<TrialFinishControl>();
    }

    int magic_counter=0;  //0:tutorial

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
        Invoke("startTrial1", 10f);
    }

    public void startToTrial(){
        switch(magic_counter){
        case 1:
            frame.StartMethod();    // 相框出現
            break; 
        case 2:
            break;
        case 3:
            file.StartMethod();     // 文件出現
            break;
        case 4:
            break;
        default:
            break;
        }

        magic.startHint();
    }
    public void endMagic(){
        switch(magic_counter){
        case 0:
            finishTutorial();
            break;
        case 1:
            frame.ChangeState(score>0);
            score1+=score;
            Invoke("startToTrial", 10f);
            break; 
        case 2:
            frame.ChangeState(score>0);
            score1+=score;
            Invoke("startTrial2", 10f);
            break;
        case 3:
            file.ChangeState(score<0);
            score2+=score;
            Invoke("startToTrial", 10f);
            break;
        case 4:
            file.ChangeState(score<0);
            score2+=score;
            Invoke("startEnd", 10f);
            break;
        default:
            break;
        }
        magic_counter++;
    }

    void startTrial1(){
        black_board.transform.GetChild(2).gameObject.SetActive(false);

        Debug.Log("Trial1 start");
        trial1.ChangePage();
        trial1.CanChangePage();
    }
    void startTrial2(){
        trial1.EndPage();
        Debug.Log("Trial2 start");
        trial2.ChangePage();
        trial2.CanChangePage();
    }

    public void ChangePage(){
        if(magic_counter==1)trial1.ChangePage();
        if(magic_counter==3)trial2.ChangePage();
    }

    void startEnd(){
        trial2.EndPage();
        Debug.Log("Ending");
        ending.PlaySlide();
    }
}
