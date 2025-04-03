using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class OBF : MonoBehaviour

{
    public ARRaycastManager RaycastManager;
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase == TouchPhase.Began)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        ARRaycast raycast = RaycastManager.AddRaycast(touch.position,estimatedDistance:0f);
        if (raycast != null)
        {
            Instantiate(prefab, raycast.pose.position, Quaternion.identity);
        }
    }
    
}
