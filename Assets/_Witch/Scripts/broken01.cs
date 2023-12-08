/*using UnityEngine;

[SelectionBase]
public class broken01 : MonoBehaviour
{
    [SerializeField] GameObject photo_frame_1;
    [SerializeField] GameObject broken_photo_frame_1_1;
    [SerializeField] GameObject broken_photo_frame_1_2;
    
    BoxCollider bc;

    private void Awake()
    {
        photo_frame_1.SetActive(true);
        broken_photo_frame_1_1.SetActive(false);
        broken_photo_frame_1_2.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        Break();
    }

    private void Break()
    {
        photo_frame_1.SetActive(false);
        broken_photo_frame_1_1.SetActive(false);
        broken_photo_frame_1_2.SetActive(true);

        // 停用碰撞器
        bc.enabled = false;
    }
  
}
*/
using UnityEngine;

[SelectionBase]
public class broken01 : MonoBehaviour
{
    [SerializeField] GameObject photo_frame_1;
    [SerializeField] GameObject broken_photo_frame_1_1;
    [SerializeField] private GameObject broken_photo_frame_1_2;
    
    BoxCollider bc;
    int clickCount = 0;  // 新增一個變量來追蹤點擊次數

    private void Awake()
    {
        photo_frame_1.SetActive(true);
        broken_photo_frame_1_1.SetActive(false);
        broken_photo_frame_1_2.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0))Break();
    }

    // private void OnMouseDown()
    // {
    //     Break();
    // }

    private void Break()
    {
        clickCount++;  // 每次點擊時增加計數

        // 根據點擊次數來切換不同的物件
        if (clickCount == 1)
        {
            photo_frame_1.SetActive(false);
            broken_photo_frame_1_1.SetActive(true);
            broken_photo_frame_1_2.SetActive(false);
        }
        else if (clickCount == 2)
        {
            photo_frame_1.SetActive(false);
            broken_photo_frame_1_1.SetActive(false);
            broken_photo_frame_1_2.SetActive(true);
        }

        // 當點擊次數超過2時，停用碰撞器
        if (clickCount >= 2)
        {
            // bc.enabled = false;
        }
    }
}


