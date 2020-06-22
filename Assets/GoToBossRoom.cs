using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBossRoom : Interactable
{
    public Transform teleportTo;
    public GameObject goTo;

    private void Start()
    {
        IsInstant = true;
    }

    public override void Interact(Transform obj)
    {
        if (teleportTo != null)
        {
            obj.position = teleportTo.position;
        }
        GameSceneManager.Instance.ChangeToScene(goTo);
        AudioManager.Instance.PlayBattleTheme();
        Camera.main.GetComponent<CameraMovementController>().moveSpeed = 30;
    }
}
