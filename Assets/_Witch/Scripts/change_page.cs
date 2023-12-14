using UnityEngine;

public class change_page : MonoBehaviour
{
    private Collider stick;
    void Start(){
        gameObject.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger page");
            GameManager.instance.ChangePage();
            stick = other;
            stick.isTrigger = false;
            GetComponent<SphereCollider>().isTrigger = false;
            Invoke("openCollider", 3f);
        }
    }

    void openCollider(){
        stick.isTrigger = true;
        GetComponent<SphereCollider>().isTrigger = true;
    }
}
