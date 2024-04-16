using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private GameObject body;
    private float rollSpeed = 1f;
    private float rollCompensation = 16f;

    private int positionIndex = 0;
    private bool canMove = true;
    public bool invulnerable = false;

    public GameObject invulnerabilityEffect;
    private float movementCooldown = 0.12f;
    private float invulnerabilityTime = 2f;
    private const string MID_TO_LEFT = "MidtoLeft";
    private const string MID_TO_RIGHT = "MidtoRight";
    private const string LEFT_TO_MID = "LefttoMid";
    private const string RIGHT_TO_MID = "RighttoMid";
    private const string IS_OBSTACLE = "Obstacle";
    private const string HIT = "Hit";
    private const string IS_JUMPING = "isJumping";

    private PauseMenu pauseMenu;
    private Animator playerAnim;
    private PowerUps powerUps;
    public Animator invAnim;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();

        playerAnim = GetComponent<Animator>();
        powerUps = GetComponent<PowerUps>();
        positionIndex = 0;
    }

    private void Update()
    {
        Movement();
        PowerUpInputs();

        rollSpeed = GameManager.Instance.gameSpeed * rollCompensation * Time.deltaTime;
        body.transform.Rotate(rollSpeed, 0, 0);
    }

    private void Movement()
    {
        if (!pauseMenu.onCountdown)
        {
            if (Input.GetKeyDown(KeyCode.A) && positionIndex != -1 && canMove)
            {
                if (positionIndex == 0)
                {
                    playerAnim.SetTrigger(MID_TO_LEFT);
                    positionIndex -= 1;
                }
                else if (positionIndex == 1)
                {
                    playerAnim.SetTrigger(RIGHT_TO_MID);
                    positionIndex -= 1;
                }

                StartCoroutine(nameof(MovementCooldown));
            }

            if (Input.GetKeyDown(KeyCode.D) && positionIndex != 1 && canMove)
            {
                if (positionIndex == 0)
                {
                    playerAnim.SetTrigger(MID_TO_RIGHT);
                    positionIndex += 1;
                }
                else if (positionIndex == -1)
                {
                    playerAnim.SetTrigger(LEFT_TO_MID);
                    positionIndex += 1;
                }

                StartCoroutine(nameof(MovementCooldown));
            }
        }
        
    }

    private void PowerUpInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pauseMenu.gameIsPaused && 
            !pauseMenu.onCountdown && !powerUps.starActive && powerUps.starReady)
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

        invulnerabilityEffect.SetActive(true);
        invAnim.SetBool(HIT, true);

        yield return new WaitForSeconds(invulnerabilityTime);

        if (!powerUps.starActive)
        {
            invulnerable = false;
            invulnerabilityEffect.SetActive(false);
        }             

        invAnim.SetBool(HIT, false);
    }
}
