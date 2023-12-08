using UnityEngine;

public class change_page : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone!");
            GameManager.instance.ChangePage();
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
            Invoke("openCollider", 2f);
        }
    }

    void openCollider(){
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }
}
