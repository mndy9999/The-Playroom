using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "new fly animation", menuName = "Cinematics/Fly Through Portal")]
public class FlyButterThroughPortal : Cinematic
{
    public GameObject animation;
    public bool needsInstantiating = true;
    private ButterflyController controller;
    private void OnEnable()
    {
        controller = animation.GetComponentInChildren<ButterflyController>();
    }

    public override IEnumerator Play()
    {
        Debug.Log("Butter now flying!");
        if(needsInstantiating)
            Instantiate(animation);
        yield return  new WaitForSeconds(delay);
        GoNext();
    }

}
