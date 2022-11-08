using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class LaptopBehaviour : MonoBehaviour
{
    public VideoPlayer LaptopScreen;
    public VideoPlayer AmphiScreen;
    public TMP_Text interactableText;
    public Outline outline;
    bool isVideoPlaying;
    Dictionary<bool, string> interactableTextMessage;

    void Start()
    {
        interactableTextMessage = new Dictionary<bool, string>();
        interactableTextMessage.Add(false, "Lancer la vidéo");
        interactableTextMessage.Add(true, "Arrêter la vidéo");
        outline.enabled = false;
    }

    public void ToggleVideo()
    {
        isVideoPlaying = !isVideoPlaying;
        if (isVideoPlaying)
        {
            LaptopScreen.Play();
            AmphiScreen.Play();
        } else {
            LaptopScreen.Stop();
            AmphiScreen.Stop();
        }
        interactableText.text = interactableTextMessage[isVideoPlaying];
    }

    public void SendDebugMessage()
    {
        Debug.Log(gameObject.name);
    }
}
