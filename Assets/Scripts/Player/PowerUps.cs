using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float starDuration = 7f;
    [SerializeField] private GameObject starIconFull;
    [SerializeField] private TextMeshProUGUI starCooldownText;
    [SerializeField] private float starMaxCooldown = 30f;

    private PlayerActions playerActions;
    private Animator animator;

    private float starCooldown;
    public bool starActive = false;
    public bool starReady = false;

    private const string STAR = "Star";

    private void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        animator = GetComponent<Animator>();
        starCooldown = starMaxCooldown;
    }

    private void Update()
    {
        if (!starActive && !starReady)
            StarCooldown();
    }

    private void StarCooldown()
    {
        if (starCooldown > 0)
        {
            starCooldown -= 1f * Time.deltaTime;
            starCooldownText.text = Mathf.CeilToInt(starCooldown).ToString("D2");
        }        
        else
        {
            starIconFull.SetActive(true);
            starReady = true;
            starCooldownText.gameObject.SetActive(false);
        }
    }

    public IEnumerator Star()
    {
        if (starActive) yield break;

        playerActions.invulnerable = true;
        starActive = true;
        playerActions.invulnerabilityEffect.SetActive(true);

        animator.SetLayerWeight(1, 1);
        animator.SetBool(STAR, true);

        yield return new WaitForSeconds(starDuration - 2f);

        playerActions.invAnim.SetBool("Hit", true);

        yield return new WaitForSeconds(2f);

        playerActions.invAnim.SetBool("Hit", false);

        playerActions.invulnerable = false;
        starActive = false;
        playerActions.invulnerabilityEffect.SetActive(false);

        animator.SetLayerWeight(1, 0);
        animator.SetBool(STAR, false);

        starReady = false;
        starCooldownText.gameObject.SetActive(true);
        starIconFull.SetActive(false);

        starCooldown = starMaxCooldown;
    }
}
