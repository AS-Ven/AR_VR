using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextToSpeech tts;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject DialogueBG;
    [SerializeField] private GameObject CommanderButton;
    [SerializeField] private GameObject MenuCommande;

    [SerializeField] private GameObject PrefabWhisky;
    [SerializeField] private GameObject PrefabMojito;
    [SerializeField] private GameObject PrefabVin;
    [SerializeField] private Transform SpawnPoint;

    [TextArea]
    public string[] dialogues = {
        "Bienvenue au bar, qu'est-ce que je vous sers ce soir ?",
        "On a un excellent whisky tourbé qui vient d'arriver.",
        "Le cocktail du jour, c'est un mojito au basilic. Je vous en prépare un ?",
        "Vous avez l'air de passer une longue journée. Un verre de rouge pour se détendre ?",
        "Dernière commande dans vingt minutes, profitez-en !"
    };

    private int currentIndex = 0;
    private bool isWaitingForOrder = false;
    private bool dialogueShown = false;

    void Start()
    {
        if (DialogueText != null)
            DialogueText.text = "";
        if (DialogueBG != null)
            DialogueBG.SetActive(false);
        if (CommanderButton != null)
            CommanderButton.SetActive(false);
        if (MenuCommande != null)
            MenuCommande.SetActive(false);
    }

    private bool triggerPressedLastFrame = false;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame && !isWaitingForOrder)
            OnInteract();

        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

        bool triggerPressed = triggerValue > 0.5f;
        if (triggerPressed && !triggerPressedLastFrame && !isWaitingForOrder)
            OnInteract();
        triggerPressedLastFrame = triggerPressed;
    }

    public void OuvrirMenu()
    {
        if (CommanderButton != null)
            CommanderButton.SetActive(false);
        if (MenuCommande != null)
            MenuCommande.SetActive(true);
    }

    public void Commander(string article)
    {
        tts.Speak("Bien sûr ! " + article + ", tout de suite !");
        if (DialogueText != null)
            DialogueText.text = "Bien sûr ! " + article + ", tout de suite !";
        if (MenuCommande != null)
            MenuCommande.SetActive(false);

        Vector3 pos = SpawnPoint != null ? SpawnPoint.position : Vector3.zero;

        if (article == "Whisky" && PrefabWhisky != null)
            Instantiate(PrefabWhisky, pos, Quaternion.identity);
        else if (article == "Mojito" && PrefabMojito != null)
            Instantiate(PrefabMojito, pos, Quaternion.identity);
        else if (article == "Vin Rouge" && PrefabVin != null)
            Instantiate(PrefabVin, pos, Quaternion.identity);

        OnOrderDone();
    }

    public void OnOrderDone()
    {
        isWaitingForOrder = false;
        dialogueShown = false;
        if (CommanderButton != null)
            CommanderButton.SetActive(false);
        if (DialogueBG != null)
            DialogueBG.SetActive(false);
        if (DialogueText != null)
            DialogueText.text = "";
        if (MenuCommande != null)
            MenuCommande.SetActive(false);
    }

    private void OnInteract()
    {
        if (!dialogueShown)
        {
            // 1er appui : affiche le dialogue
            currentIndex = Random.Range(0, dialogues.Length);
            PlayCurrent();
            dialogueShown = true;
        }
        else
        {
            // 2ème appui : affiche le bouton Commander
            if (CommanderButton != null)
                CommanderButton.SetActive(true);
            isWaitingForOrder = true;
        }
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
        if (CommanderButton != null)
            CommanderButton.SetActive(false);
        tts.Speak(dialogues[currentIndex]);
        if (DialogueText != null)
            DialogueText.text = dialogues[currentIndex];
    }
}
