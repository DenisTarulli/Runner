using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float scroll;
    [SerializeField] private float scrollSpeedGain;
    [SerializeField] private float scrollSpeed;

    private Material _material;

    private const string SCROLL_Y = "_ScrollY";

    private void Start()
    {
        scrollSpeed = 0.5f;

        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        scrollSpeed += scrollSpeedGain * Time.deltaTime;

        scroll -= scrollSpeed * Time.deltaTime;

        _material.SetFloat(SCROLL_Y, scroll);
    }

    
}
