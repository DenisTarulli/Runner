using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    private int positionIndex = 0;
    private bool canMove = true;

    [SerializeField] private Image image;

    private const float movementCooldown = 0.08f;
    private const string MID_TO_LEFT = "MidtoLeft";
    private const string MID_TO_RIGHT = "MidtoRight";
    private const string LEFT_TO_MID = "LefttoMid";
    private const string RIGHT_TO_MID = "RighttoMid";
    private const string IS_OBSTACLE = "Obstacle";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        positionIndex = 0;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.A) && positionIndex != -1 && canMove)
        {
            if (positionIndex == 0)
            {
                animator.SetTrigger(MID_TO_LEFT);
                positionIndex -= 1;
            }
            else if (positionIndex == 1)
            {
                animator.SetTrigger(RIGHT_TO_MID);
                positionIndex -= 1;
            }

            StartCoroutine(nameof(MovementCooldown));
        }

        if (Input.GetKeyDown(KeyCode.D) && positionIndex != 1 && canMove)
        {
            if (positionIndex == 0)
            {
                animator.SetTrigger(MID_TO_RIGHT);
                positionIndex += 1;
            }
            else if (positionIndex == -1)
            {
                animator.SetTrigger(LEFT_TO_MID);
                positionIndex += 1;
            }

            StartCoroutine(nameof(MovementCooldown));
        }
    }

    private IEnumerator MovementCooldown()
    {
        canMove = false;

        yield return new WaitForSeconds(movementCooldown);

        canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(IS_OBSTACLE)) return;

        GameManager.Instance.currentHp--;
        GameManager.Instance.HpUpdate(GameManager.Instance.currentHp);
    }
}
