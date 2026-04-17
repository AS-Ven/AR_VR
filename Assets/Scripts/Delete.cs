using UnityEngine;
using UnityEngine.XR;

public class DeleteObject : MonoBehaviour
{
    [SerializeField] private GameObject deletePanel;
    private bool gripPressedLastFrame = false;

    void Start()
    {
        if (deletePanel != null)
            deletePanel.SetActive(false);
    }

    void Update()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);

        if (gripPressed && !gripPressedLastFrame)
            TogglePanel();

        gripPressedLastFrame = gripPressed;
    }

    private void OnMouseDown()
    {
        TogglePanel();
    }

    private void TogglePanel()
    {
        if (deletePanel != null)
            deletePanel.SetActive(!deletePanel.activeSelf);
    }

    public void Supprimer()
    {
        Destroy(gameObject);
    }
}