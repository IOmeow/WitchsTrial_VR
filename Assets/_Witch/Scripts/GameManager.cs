using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool debug = true;
    public float score = 0f;
    public float score1 = 0f, score2 = 0f;
    public bool tutorial = true;
    public bool lookAtSlide  = false;

    MagicController magic;
    ProjectionControl projection;
    GameObject hint;
    GameObject SceneCorridor, SceneClassroom;
    GameObject black_board;
    GameObject magic_stick;
    GameObject classroom_light;

    TrialControl trial1, trial2, ending;
    TrialFinishControl frame, file;

    NPCAnimation npc;

    LightControl light;
    ParticleSystem magic_particle;

    GameObject door_hint;

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
        magic_stick = GameObject.Find("MagicStick");

        npc = GameObject.Find("NPC").GetComponent<NPCAnimation>();

        classroom_light = GameObject.Find("env_light").transform.GetChild(0).gameObject;
        classroom_light.SetActive(false);

        light = GameObject.Find("CenterEyeAnchor").GetComponent<LightControl>();
        magic_particle = GameObject.Find("Magicshow").GetComponent<ParticleSystem>();

        door_hint = GameObject.Find("OpenDoorHint");
        door_hint.SetActive(false);
        Invoke("showOpenDoorHint", 10f);
    }
    void showOpenDoorHint(){
        door_hint.SetActive(true);
    }

    int magic_counter=0;  //0:tutorial

    // 到教室場景
    public void toClassrom(){
        CancelInvoke("showOpenDoorHint");
        door_hint.SetActive(false);
        magic_stick.SetActive(false);
        Invoke("fadeToStart", 3f);
    }
    void fadeToStart(){
        OVRScreenFade.instance.FadeOut();
        Invoke("gameStart", 3f);
    }
    void gameStart(){
        SceneCorridor.transform.position = new Vector3(5000f, 0f,0f);
        SceneClassroom.transform.position = Vector3.zero;
        classroom_light.SetActive(true);
        OVRScreenFade.instance.FadeIn();
        VoiceOverControl.instance.playTutorial(0, true);
        npc.animateStart();
        Invoke("npcToBlackboard", 15f);
    }
    void npcToBlackboard(){
        npc.toBlackBoard();
    }

    public void ActiveMagicStick(){
        magic_stick.SetActive(true);
        black_board.transform.GetChild(0).gameObject.SetActive(true);
    }

    // 拿起法杖
    bool picked = false;
    public void pickUp(){
        if(picked)return;
        picked = true;
        black_board.transform.GetChild(0).gameObject.SetActive(false);
        black_board.transform.GetChild(1).gameObject.SetActive(true);
        VoiceOverControl.instance.playTutorial(2, true);
    }
    void finishTutorial(){
        VoiceOverControl.instance.playTutorial(5, true);
        tutorial = false;
    }
    public void ShowEmotionWord(){
        black_board.transform.GetChild(2).gameObject.SetActive(false);
        black_board.transform.GetChild(3).gameObject.SetActive(true);
        SoundControl.instance.playChalkSE();
        Invoke("toStartTrial", 5f);
    }
    void toStartTrial(){
        black_board.transform.GetChild(3).gameObject.SetActive(false);
        black_board.transform.GetChild(4).gameObject.SetActive(true);
        VoiceOverControl.instance.playTutorial(6, false);

        projection.OpenProjection();
        Invoke("startTrial1", 3f);
    }
    public void canStartMagic(){
        magic.startHint();
        VoiceOverControl.instance.startMurmur();
    }

    public void startToTrial(){
        switch(magic_counter){
        case 1:
            frame.StartMethod();    // 相框出現
            VoiceOverControl.instance.playTrial(0, true);
            VoiceOverControl.instance.startMurmur();
            break; 
        case 2:
            VoiceOverControl.instance.playTrial(9, false);
            VoiceOverControl.instance.startMurmur();
            break;
        case 3:
            file.StartMethod();     // 文件出現
            VoiceOverControl.instance.playTrial(1, true);
            VoiceOverControl.instance.startMurmur();
            break;
        case 4:
            VoiceOverControl.instance.playTrial(9, false);
            VoiceOverControl.instance.startMurmur();
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
            SoundControl.instance.playFrameSE(score>0);
            score1+=score;
            Invoke("startToTrial", 10f);
            break; 
        case 2:
            frame.ChangeState(score>0);
            SoundControl.instance.playFrameSE(score>0);
            score1+=score;
            Invoke("startTrial2", 10f);
            break;
        case 3:
            file.ChangeState(score<0);
            SoundControl.instance.playFileSE(score<0);
            score2+=score;
            Invoke("startToTrial", 10f);
            break;
        case 4:
            file.ChangeState(score<0);
            SoundControl.instance.playFileSE(score<0);
            score2+=score;
            Invoke("startEnd", 10f);
            break;
        default:
            break;
        }
        light.ChangeLight(score>=0);
        Invoke("stopLight", 10f);
        magic_particle.Play();
        magic_counter++;
    }
    void stopLight(){
        light.ResetLigt();
        magic_particle.Stop();
    }

    void startTrial1(){
        black_board.transform.GetChild(4).gameObject.SetActive(false);
        black_board.transform.GetChild(5).gameObject.SetActive(true);
        npc.returnFromBlackBoard();
        
        Debug.Log("Trial1 start");
        trial1.ChangePage();
        trial1.CanChangePage();

        SoundControl.instance.playBGM(0);
    }
    void startTrial2(){
        trial1.EndPage();
        VoiceOverControl.instance.playTrial(3, false);
        Debug.Log("Trial2 start");
        trial2.ChangePage();
        trial2.CanChangePage();

        SoundControl.instance.playBGM(1);
    }

    public void ChangePage(){
        if(magic_counter==1)trial1.ChangePage();
        if(magic_counter==3)trial2.ChangePage();
    }

    public int final_score = 0;
    void startEnd(){
        trial2.EndPage();
        VoiceOverControl.instance.playTrial(4, false);
        Debug.Log("Ending");
        if(score1>0)final_score++;
        if(score2<0)final_score++;
        ending.PlayEndSlide(final_score);

        SoundControl.instance.playBGM(2);
    }
}
