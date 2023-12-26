using UnityEngine;

public class change_page : MonoBehaviour
{
    private Collider stick;
    private MeshRenderer page_hint;
    void Start(){
        gameObject.SetActive(false);
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
            Invoke("openCollider", 3f);
            page_hint.enabled = false;
        }
    }

    void openCollider(){
        stick.isTrigger = true;
        Invoke("openHint", 3f);
    }

    void openHint(){
        page_hint.enabled = true;
    }
}
