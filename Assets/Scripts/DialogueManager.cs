using UnityEngine;
using UnityEngine.InputSystem;
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

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Next();
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