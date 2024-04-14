using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    private int positionIndex = 0;
    private bool canMove = true;
    public bool invulnerable = false;

    [SerializeField] private Image image;

    private float movementCooldown = 0.08f;
    private float invulnerabilityTime = 2f;
    private const string MID_TO_LEFT = "MidtoLeft";
    private const string MID_TO_RIGHT = "MidtoRight";
    private const string LEFT_TO_MID = "LefttoMid";
    private const string RIGHT_TO_MID = "RighttoMid";
    private const string IS_OBSTACLE = "Obstacle";
    private const string HIT = "Hit";

    private Animator animator;
    private PowerUps powerUps;

    private void Start()
    {
        animator = GetComponent<Animator>();
        powerUps = GetComponent<PowerUps>();
        positionIndex = 0;
    }

    private void Update()
    {
        Movement();
        PowerUpInputs();
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

    private void PowerUpInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(powerUps.Star());
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
        if (collision.collider.CompareTag(IS_OBSTACLE))
        {
            if (!invulnerable)
            {
                StartCoroutine(nameof(Invulnerability));
                GameManager.Instance.currentHp--;
                GameManager.Instance.HpUpdate(GameManager.Instance.currentHp);
            }
            
            if (powerUps.starActive)
            {
                Destroy(collision.collider.gameObject);
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;

        animator.SetLayerWeight(1, 1);
        animator.SetBool(HIT, true);

        yield return new WaitForSeconds(invulnerabilityTime);

        if (!powerUps.starActive)
        {
            animator.SetLayerWeight(1, 0);
            invulnerable = false;
        }

        animator.SetBool(HIT, false);
    }
}
