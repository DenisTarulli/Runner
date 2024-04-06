using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Vector3 leftPosition = new (-2f, 2f, 0f);
    private Vector3 rightPosition = new (2f, 2f, 0f);
    private Vector3 midPosition = new (0f, 2f, 0f);

    private int positionIndex = 0;

    private void Start()
    {
        positionIndex = 0;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.A) && positionIndex != -1)
        {
            if (positionIndex == 0)
            {
                transform.position = leftPosition;
                positionIndex -= 1;
            }
            else if (positionIndex == 1)
            {
                transform.position = midPosition;
                positionIndex -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && positionIndex != 1)
        {
            if (positionIndex == 0)
            {
                transform.position = rightPosition;
                positionIndex += 1;
            }
            else if (positionIndex == -1)
            {
                transform.position = midPosition;
                positionIndex += 1;
            }
        }
    }
}
