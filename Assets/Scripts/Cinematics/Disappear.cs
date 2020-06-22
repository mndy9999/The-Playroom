using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Disappear Butter", menuName = "Cinematics/Disappear")]
public class Disappear : Cinematic
{

    public override IEnumerator Play()
    {
        yield return new WaitForSeconds(delay);
        CinematicsManager.Instance.butterflyController.Disappear();
        GoNext();
    }
}
