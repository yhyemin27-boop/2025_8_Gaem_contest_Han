using UnityEngine;
using UnityEngine.SceneManagement;
public class Move : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public Sprite idleSprite;  // Idle 이미지
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        anim.enabled = false;       // 시작할 때 Animator는 꺼둠
        sr.sprite = idleSprite;     // 기본 Idle 이미지로 초기화

    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // 이동 입력
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // 달리기 Shift 키 입력
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;

        // Rigidbody2D 이동
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);


        if (moveX != 0)   // 움직일 때
        {
            if (!anim.enabled)
                anim.enabled = true;
            anim.SetBool("isWalking", !isRunning);
            anim.SetBool("isRunning", isRunning);
        }
        else              // 멈춰 있을 때 (Idle)
        {
            if (anim.enabled)
            {
                anim.enabled = false;  // Animator 끄기
                sr.sprite = idleSprite; // 고정 이미지 표시
            }
        }

        if (moveX < 0) transform.localScale = new Vector3(-1, 1, 1);
        if (moveX > 0) transform.localScale = new Vector3(1, 1, 1);

    }
    
}
