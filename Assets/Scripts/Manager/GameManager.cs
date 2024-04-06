using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            spawner.SetActive(true);
    }
}
