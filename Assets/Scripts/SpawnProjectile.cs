using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    void Start()
    {
        var rand = Random.Range(0.0f, 3.0f);
        StartCoroutine(Shoot(rand));
    }

    IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var rand = Random.Range(1.0f, 3.0f);
        StartCoroutine(Shoot(rand));
    }

}
