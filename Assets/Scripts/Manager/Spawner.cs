using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool recentlySpawned = false;

    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
    }

    [SerializeField] private SpawnableObject[] objects;

    public void Spawn()
    {
        if (recentlySpawned) return;

        StartCoroutine(nameof(SpawnDelay));

        int randomIndex = Mathf.FloorToInt(Random.Range(0f, objects.GetLength(0)));

        Instantiate(objects[randomIndex].prefab, transform.position, Quaternion.identity);
    }

    private IEnumerator SpawnDelay() // To prevent spawn overlapping
    {
        recentlySpawned = true;

        yield return new WaitForSeconds(0.15f);

        recentlySpawned = false;
    }
}
