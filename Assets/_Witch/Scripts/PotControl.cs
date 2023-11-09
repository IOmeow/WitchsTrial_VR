using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotControl : MonoBehaviour
{
    private GameObject liquid_1, liquid_2, liquid_3;
    private GameObject potion;
    private float final_score = 0f;
    private int count = 0;
    private GameObject magic;
    bool tutorial = true;
    bool potion2float = false;

    private int potion_num = 0; //哪一款魔藥瓶

    SoundControl sound;
    MagicController magicControl;
    // Start is called before the first frame update
    void Awake()
    {
        liquid_1 = GameObject.Find("liquid_1");
        liquid_2 = GameObject.Find("liquid_2");
        liquid_3 = GameObject.Find("liquid_3");
        liquid_1.SetActive(false);
        liquid_2.SetActive(false);
        liquid_3.SetActive(false);

        potion = GameObject.Find("Potion");
        foreach (Transform child in potion.transform){
            child.gameObject.SetActive(false);
        }
        
        magic = GameObject.Find("MagicPot");

        magicControl = GameObject.Find("stick").GetComponent<MagicController>();
        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.L))MagicFinish(0f);
        if(Input.GetKeyDown(KeyCode.K))MagicFinish(0.5f);
        if(Input.GetKeyDown(KeyCode.J))MagicFinish(-0.5f);

        if(potion2float){
            // Debug.Log(potion.transform.position.y);
            if(potion.transform.position.y<1)potion.transform.Translate(Vector3.up * Time.deltaTime*0.5);
        }
    }

    public void MagicFinish(float score){
        if(count==3||tutorial)return;

        float mappedScore = (score + 1) / 2;
        magic.GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.cyan, Color.yellow, mappedScore);
        magic.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.cyan, Color.yellow, mappedScore);
        magic.GetComponent<ParticleSystem>().Play();
        Invoke("stopMagic", 10f);

        sound.playPotSE();
        count++;
        final_score+=score;

        mappedScore = (final_score + 3) / 6;
        Color newColor = Color.Lerp(Color.cyan, Color.yellow, mappedScore);
        switch(count){
        case 1:
            liquid_1.SetActive(true);
            liquid_1.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            liquid_1.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            break;
        case 2:
            liquid_2.SetActive(true);
            liquid_2.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            liquid_2.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            break;
        case 3:
            liquid_3.SetActive(true);
            liquid_3.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            liquid_3.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            
            GameObject p = potion.transform.GetChild(potion_num).gameObject;
            p.SetActive(true);
            p.transform.Find("liquid").gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            break;
        }
    }

    void stopMagic(){
        magic.GetComponent<ParticleSystem>().Stop();
        if(count==3)potion2float = true;
        else magicControl.startHint();
    }

    public void tutorialEnd(){
        tutorial = false;
    }
}
