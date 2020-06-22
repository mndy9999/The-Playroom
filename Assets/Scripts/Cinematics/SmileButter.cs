using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Smile Butter", menuName = "Cinematics/Smile Butter")]
public class SmileButter : Cinematic
{
    public override IEnumerator Play()
    {
        CinematicsManager.Instance.butterflyController.Smile();
        yield return new WaitForSeconds(delay);
        GoNext();
    }
}
