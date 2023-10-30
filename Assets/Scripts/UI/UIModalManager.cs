using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModalManager : MonoBehaviour
{
    public GameObject UIModal;
    [SerializeField]
    float cooldown = 0f;

    void Start()
    {
        DoorBehaviour.TriedOpeningDoorEvent += ShowDoorLockedModal;
    }

    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void ShowDoorLockedModal(object sender, OnDoorUnlockEventArgs e)
    {
        if (cooldown > 0) return;
        GameObject modal = Instantiate(UIModal, gameObject.transform);
        string message;
        if (e.isKeyInLock)
            message = "Ce n'est pas la bonne clé !";
        else
            message = "C'est verrouillé.";
        

        modal.GetComponent<UIModal>().Show(message);
        cooldown = 10f;
    }
}
