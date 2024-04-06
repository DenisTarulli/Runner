using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;

    private Spawner spawner;

    private const string IS_EDGE = "Edge";
    private const string IS_TRIGGER = "Trigger";
    private const string IS_SPAWNER = "Spawner";

    private void Start()
    {
        spawner = GameObject.FindWithTag(IS_SPAWNER).GetComponent<Spawner>();
    }

    private void Update()
    {
        transform.position += Vector3.back * scrollSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(IS_EDGE))
            Destroy(gameObject);

        else if (collision.gameObject.CompareTag(IS_TRIGGER))
            spawner.Spawn();
    }
}
