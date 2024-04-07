using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Spawner spawner;

    private const string IS_EDGE = "Edge";
    private const string IS_TRIGGER = "Trigger";

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        transform.position += Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(IS_EDGE))
            Destroy(gameObject);

        else if (collision.gameObject.CompareTag(IS_TRIGGER))
            spawner.Spawn();
    }
}
