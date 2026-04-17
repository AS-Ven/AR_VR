using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string apiKey = "60ac4be9522c4ac585cc8d3fdd90c571"; // voicerss.org/registration

    public void Speak(string text)
    {
        StartCoroutine(FetchAudio(text));
    }

    private IEnumerator FetchAudio(string text)
    {
        string url = $"https://api.voicerss.org/?key={apiKey}&hl=fr-fr&src={UnityWebRequest.EscapeURL(text)}&c=OGG&f=16khz_16bit_mono";

        using var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            audioSource.clip = DownloadHandlerAudioClip.GetContent(request);
            audioSource.Play();
        }
        else
        {
            Debug.LogError("TTS Error: " + request.error);
        }
    }
}