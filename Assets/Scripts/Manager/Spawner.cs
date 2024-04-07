using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
    }

    [SerializeField] private SpawnableObject[] objects;

    public void Spawn()
    {
        int randomIndex = Mathf.FloorToInt(Random.Range(0f, objects.GetLength(0)));

        Instantiate(objects[randomIndex].prefab, transform.position, Quaternion.identity);
    }
}
