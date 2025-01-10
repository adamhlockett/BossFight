using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticleHandler : MonoBehaviour
{
    [SerializeField] GameObject systemPrefab;
    private float spawnEvery = 0.5f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(systemPrefab, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnEvery);
        StartCoroutine(Spawn());
    }
}
