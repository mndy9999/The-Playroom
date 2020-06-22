using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frown Butter", menuName = "Cinematics/Frown Butter")]
public class FrownButter : Cinematic
{

    public override IEnumerator Play()
    {
        CinematicsManager.Instance.butterflyController.Frown();
        yield return new WaitForSeconds(delay);
        GoNext();
    }
}
