using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_page : MonoBehaviour
{
    private Collider stick;
    private MeshRenderer page_hint;
    void Start(){
        page_hint = GameObject.Find("TurnPageHint").GetComponent<MeshRenderer>();
        page_hint.enabled = false;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger page");
            GameManager.instance.ChangePage();
            stick = other;
            stick.isTrigger = false;
            CancelInvoke("openHint");
            Invoke("openCollider", 2f);
            page_hint.enabled = false;
        }
    }

    void openCollider(){
        if(gameObject.GetComponent<BoxCollider>().enabled == false){
            stick.isTrigger = true;
            Invoke("openHint", 3f);
        }
        else StartCoroutine(WaitForlLookAtSlide());
    }
    IEnumerator WaitForlLookAtSlide()
    {
        yield return new WaitUntil(() => GameManager.instance.lookAtSlide == true);
        
        stick.isTrigger = true;
        Invoke("openHint", 3f);
    }

    void openHint(){
        page_hint.enabled = true;
    }
}
