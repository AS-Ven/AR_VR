using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> sphereObj;
    [SerializeField] InputAction action;
    [SerializeField] InputActionAsset action_vr;
    InputAction my_action;

    bool isLightOn = true;

    void Start()
    {
        my_action = action_vr.FindAction("interact");
    }

    public void SwitchLight()
    {
        if (action.triggered || my_action.triggered)
        {
            isLightOn = !isLightOn;
            Debug.Log("Light toggled: " + (isLightOn ? "On" : "Off"));
            sphereObj.ForEach(sphere => sphere.SetActive(isLightOn));
        }
    }

    void Update()
    {
        SwitchLight();
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
