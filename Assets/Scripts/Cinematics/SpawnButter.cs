using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spawn Butter ", menuName = "Cinematics/Spawn Butter")]
public class SpawnButter : Cinematic
{
    public GameObject butterflyPrefab;

    public override IEnumerator Play()
    {
        if (butterflyPrefab == null)
            yield return null;
        CinematicsManager.Instance.CreateButter(butterflyPrefab);
        yield return new WaitForSeconds(delay);
        GoNext();
    }
}
