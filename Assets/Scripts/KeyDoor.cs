using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_Text collectText;
    [SerializeField] private TextAppear textAppear;
    [SerializeField] private Animator animator;
    public void Interact()
    {
        if (KeyCollectable.hasKey == true)
        {
            Debug.Log("Key door opened.");
            collectText.text = "Door unlocked!";
            collectText.enabled = true;
            textAppear.TextDisappear();

            animator.enabled = true;

            KeyCollectable.hasKey = false;
        }
        else
        {
            collectText.text = "You need a key.";
            collectText.enabled = true;
            textAppear.TextDisappear();
        }
    }
}
