using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotControl : MonoBehaviour
{
    private GameObject liquid_1, liquid_2, liquid_3;
    private float final_score = 0f;
    private int count = 0;
    private GameObject magic;

    SoundControl sound;
    // Start is called before the first frame update
    void Start()
    {
        liquid_1 = GameObject.Find("liquid_1");
        liquid_2 = GameObject.Find("liquid_2");
        liquid_3 = GameObject.Find("liquid_3");
        liquid_1.SetActive(false);
        liquid_2.SetActive(false);
        liquid_3.SetActive(false);

        magic = GameObject.Find("MagicPot");

        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
    }

    public void MagicFinish(float score){
        if(count==3)return;

        float mappedScore = (score + 1) / 2;
        magic.GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.cyan, Color.yellow, mappedScore);
        magic.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.cyan, Color.yellow, mappedScore);
        magic.GetComponent<ParticleSystem>().Play();
        Invoke("stopMagic", 10f);

        sound.playPotSE();
        count++;
        final_score+=score;

        mappedScore = (final_score + 3) / 8;
        Color newColor = Color.Lerp(Color.black, Color.white, mappedScore);
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
            break;
        }
    }

    void stopMagic(){
        magic.GetComponent<ParticleSystem>().Stop();
    }
}
