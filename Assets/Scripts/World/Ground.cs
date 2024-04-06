using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;

    private const string IS_EDGE = "Edge";

    private void Update()
    {
        transform.position += Vector3.back * scrollSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (!collision.gameObject.CompareTag(IS_EDGE)) return;

        Destroy(gameObject);
    }
}
