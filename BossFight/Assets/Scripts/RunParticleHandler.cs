using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticleHandler : MonoBehaviour
{
    [SerializeField] GameObject systemPrefab;
    [SerializeField] Transform player;
    Vector3 lastPos;
    private float spawnEvery = 0.25f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (player.position != lastPos)
        {
            Instantiate(systemPrefab, this.transform.position, Quaternion.identity);
            lastPos = player.position; 
        }
        yield return new WaitForSeconds(spawnEvery);
        StartCoroutine(Spawn());
    }
}
