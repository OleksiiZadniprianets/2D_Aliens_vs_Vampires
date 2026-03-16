using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class UFOAbilityManager : MonoBehaviour
{
    public static UFOAbilityManager instance;

    [Header("UFO Visual")]
    public GameObject ufoPrefab;
    public Sprite[] ufoFrames; // 4 кадри

    [Header("Settings")]
    public float ufoHeight = 2.5f;
    public float frameDelay = 0.15f;
    public float abductDuration = 0.8f;

    public LayerMask enemyLayer;
    [Header("Sound")]
    public AudioClip ufoUseSound;
    AudioSource audioSource;

    bool isSelectingTarget = false;
    bool isAbducting = false;

    void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        // --- Кулдаун UFO ---
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownText != null)
                cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0;

                if (cooldownText != null)
                    cooldownText.text = "";

                if (ufoButton != null)
                    ufoButton.interactable = true;
            }
        }

        if (!isSelectingTarget || isAbducting)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked for UFO");

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapPoint(mousePos2D, enemyLayer);

            if (hit == null)
            {
                Debug.Log("Nothing hit");
                return;
            }

            Debug.Log("Hit object: " + hit.name);

            EnemyController enemy = hit.GetComponent<EnemyController>();

            if (enemy == null)
                enemy = hit.GetComponentInParent<EnemyController>();

            if (enemy == null)
            {
                Debug.Log("Not an enemy");
                return;
            }

            StartCoroutine(AbductEnemy(enemy));
        }
    }

    public void ActivateUFOSelection()
    {
        if (isAbducting || cooldownTimer > 0)
            return;

        isSelectingTarget = true;
    }
    [Header("Cooldown")]
    public float cooldown = 30f;
    float cooldownTimer = 0f;

    public TextMeshProUGUI cooldownText;
    public Button ufoButton;
    public bool IsSelectingTarget()
    {
        return isSelectingTarget;
    }

    IEnumerator AbductEnemy(EnemyController enemy)
    {
        if (enemy == null)
            yield break;

        isSelectingTarget = false;
        isAbducting = true;

        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        enemy.enabled = false;

        Vector3 startEnemyPos = enemy.transform.position;
        Vector3 ufoPos = startEnemyPos + Vector3.up * ufoHeight;

        GameObject ufoObject = Instantiate(ufoPrefab, ufoPos, Quaternion.identity);
        if (audioSource != null && ufoUseSound != null)
            audioSource.PlayOneShot(ufoUseSound);
        SpriteRenderer renderer = ufoObject.GetComponent<SpriteRenderer>();

        // --- Анімація UFO ---
        for (int i = 0; i < ufoFrames.Length; i++)
        {
            renderer.sprite = ufoFrames[i];
            yield return new WaitForSeconds(frameDelay);
        }

        // --- Підняття ворога ---
        float timer = 0f;
        Vector3 targetEnemyPos = ufoPos;

        while (timer < abductDuration)
        {
            if (enemy == null)
                break;

            timer += Time.deltaTime;

            float t = timer / abductDuration;

            enemy.transform.position =
                Vector3.Lerp(startEnemyPos, targetEnemyPos, t);

            yield return null;
        }

        if (enemy != null)
            Destroy(enemy.gameObject);

        Destroy(ufoObject);

        isAbducting = false;
        cooldownTimer = cooldown;

        if (ufoButton != null)
            ufoButton.interactable = false;
    }
}