using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextToSpeech tts;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject DialogueBG;

    [TextArea]
    public string[] dialogues = {
        "Bienvenue au bar, qu'est-ce que je vous sers ce soir ?",
        "On a un excellent whisky tourbé qui vient d'arriver.",
        "Le cocktail du jour, c'est un mojito au basilic. Je vous en prépare un ?",
        "Vous avez l'air de passer une longue journée. Un verre de rouge pour se détendre ?",
        "Dernière commande dans vingt minutes, profitez-en !"
    };

    private int currentIndex = 0;

    void Start()
    {
        if (DialogueText != null)
            DialogueText.text = "";
        if (DialogueBG != null)
            DialogueBG.SetActive(false);
    }

    private bool triggerPressedLastFrame = false;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            Next();

        // SecondaryIndexTrigger = gâchette droite (manette droite)
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

        bool triggerPressed = triggerValue > 0.5f;
        if (triggerPressed && !triggerPressedLastFrame)
            Next();
        triggerPressedLastFrame = triggerPressed;
    }

    [ContextMenu("Dialogue Suivant")]
    public void Next()
    {
        currentIndex = Random.Range(0, dialogues.Length);
        PlayCurrent();
    }

    private void PlayCurrent()
    {
        if (DialogueBG != null)
            DialogueBG.SetActive(true);
        tts.Speak(dialogues[currentIndex]);
        if (DialogueText != null)
            DialogueText.text = dialogues[currentIndex];
    }
}