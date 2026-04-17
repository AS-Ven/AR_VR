using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightHandler : MonoBehaviour
{
    [SerializeField] Transform thisTransform;
    [SerializeField] float spinSpeedX = 0;
    [SerializeField] float spinSpeedY = 2;
    [SerializeField] float spinSpeedZ = 0;
    [SerializeField] GameObject thisGameObject;
    [SerializeField] List<GameObject> sphereObj;
    [SerializeField] InputAction action;

    bool isLightOn = false;

    int n = 1;

    void Start()
    {
        print("print : LightHandler started.");
        Debug.Log("debug : LightHandler started.");

        thisTransform = GetComponent<Transform>();
        //Instantiate(thisGameObject, thisTransform.position + new Vector3(0,0,1), Quaternion.identity);
    }

    void Update()
    {
        thisTransform.Rotate(spinSpeedX * 1f * n, spinSpeedY * 1f * n, spinSpeedZ * 1f * n);
        if (action.triggered)
        {
            isLightOn = !isLightOn;
            Debug.Log("Light toggled: " + (isLightOn ? "On" : "Off"));
            sphereObj.ForEach(sphere => sphere.SetActive(isLightOn));
            //sphereObj.SetActive(isLightOn);
        }
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
}
