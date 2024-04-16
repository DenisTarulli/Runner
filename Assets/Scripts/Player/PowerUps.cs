using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float starDuration = 7f;

    private PlayerActions playerActions;
    private Animator animator;

    public bool starActive = false;

    private const string STAR = "Star";

    private void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        animator = GetComponent<Animator>();
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
    }
}
