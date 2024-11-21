using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private int blinkCount = 5;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private bool isInvincible = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        
        // Set appropriate material based on tag
        if (gameObject.CompareTag("Player"))
            spriteRenderer.material = playerMaterial;
        else
            spriteRenderer.material = enemyMaterial;
    }

    public bool IsInvincible => isInvincible;

    private IEnumerator BlinkRoutine()
    {
        isInvincible = true;

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false;
    }

    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(BlinkRoutine());
        }
    }
}