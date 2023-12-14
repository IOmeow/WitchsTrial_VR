using UnityEngine;

public class pc_debug : MonoBehaviour
{
    void Update(){
        if(!GameManager.instance.debug)Destroy(this);
        if(Input.GetKeyDown(KeyCode.P))GameManager.instance.pickUp();
    }
}