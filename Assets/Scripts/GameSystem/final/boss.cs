using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour
{
    public float runSpeed = 10f;
    public float detectionRadius = 8f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    public Sprite idleSprite;

    private Transform target;
    private bool isBossFight = false;
    public ghostfirst ghostEd; // 보스전 시작 전 배드엔딩

    public GameObject door;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // 시작 시 IDLE 상태
        anim.enabled = false;
        sr.sprite = idleSprite;
        door.SetActive(false);
    }

    void OnEnable()
    {
        InsiderSystemScript.OnPlayerTeleported += FollowPlayerTeleport;
        floor.OnPlayerTeleported += FollowPlayerTeleport;
    }

    void OnDisable()
    {
        InsiderSystemScript.OnPlayerTeleported -= FollowPlayerTeleport;
        floor.OnPlayerTeleported -= FollowPlayerTeleport;
    }

    private void FollowPlayerTeleport(Vector2 playerPos)
    {
        if (!isBossFight) return;

        Vector3 newPos = transform.position;
        newPos.y = playerPos.y;
        float distanceX = playerPos.x - transform.position.x;
        float maxTeleportX = 2f;
        newPos.x += Mathf.Clamp(distanceX, -maxTeleportX, maxTeleportX);
        transform.position = newPos;
    }

    public void StartBoss()
    {
        StartCoroutine(StartBossFight());
    }

    private IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(2f);
        isBossFight = true;
    }

    void Update()
    {
        if (!isBossFight)
        {
            // 보스전 전 항상 IDLE 유지
            rb.velocity = Vector2.zero;
            anim.enabled = false;
            sr.sprite = idleSprite;
            return;
        }

        UpdateTarget();
        Run();
    }

    private void UpdateTarget()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj != null ? playerObj.transform : null;
    }

    private void Run()
    {
        if (target == null)
        {
            // 타겟 없으면 IDLE
            rb.velocity = Vector2.zero;
            anim.enabled = false;
            sr.sprite = idleSprite;
            return;
        }

        float directionX = Mathf.Sign(target.position.x - transform.position.x);
        rb.velocity = new Vector2(directionX * runSpeed, rb.velocity.y);
        sr.flipX = directionX < 0;

        if (!anim.enabled) anim.enabled = true;
        anim.SetBool("isRunning", true);
        door.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && anim.GetBool("isRunning"))
        {
            // 충돌 시 IDLE
            rb.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);
            anim.enabled = false;
            sr.sprite = idleSprite;
            if (ghostEd != null)
            {
                ghostEd.badEed();
            }
            isBossFight = false;
            ghostEd.badEed2();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
