using UnityEngine;
using UnityEngine.InputSystem;

public class UIButton : MonoBehaviour
{
    [SerializeField] InputAction action;

    public void Test()
    {
        Debug.Log("Test bouton ");
    }
}
