using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] InputAction action;
    [SerializeField] GameObject thisGameObject;
    Transform thisTransform;

    bool isDoorOpen = false;
    int doorAngle = 0;
    bool triggered = false;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (action.triggered && triggered == false)
        {
            triggered = true;
            isDoorOpen = !isDoorOpen;
            Debug.Log("Door toggled: " + (isDoorOpen ? "Open" : "Closed"));

            if (isDoorOpen)
                doorAngle = -1;
            else
                doorAngle = 1;
            thisTransform.Rotate(0, 90f * doorAngle, 0);
            triggered = false;
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
