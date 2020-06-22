using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceMap : Interactable
{
    public GameObject leftSection;
    public GameObject rightSection;

    public bool Instant;

    private void Start()
    {
        IsInstant = Instant;
    }

    public override void Interact(Transform obj)
    {
        var goTo = GameSceneManager.Instance.currentScene == leftSection ? rightSection : leftSection;
        if (goTo != null)
        {
            AudioManager.Instance.Play("Switch");
            GameSceneManager.Instance.ChangeToScene(goTo);
            IsInstant = false;
        }
    }
}
