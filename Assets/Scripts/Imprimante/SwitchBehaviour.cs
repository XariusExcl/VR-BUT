using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public bool IsOn {get; private set;} = false;
    
    public void ToggleSwitch()
    {
        // Nous allons restreindre le fait de pouvoir éteindre l'imprimante.
        if (!IsOn)
        {
            IsOn = true;
            transform.Rotate(new Vector3(0.0f, 0.0f, -50f));
        }
    }
}
