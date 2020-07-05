using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    public AudioSource jumpAudio, coinAudio, hitAudio;
    private Rigidbody2D rb;
    private Animator animator;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public Collider2D collider;
    public Collider2D disCollider;
    public Text cherryNum;
    public int cherry;
    public Transform cellingPoint;
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cherryNum.text = cherry.ToString();
    }

    void FixedUpdate()
    {
                jump();

        if (!isHurt)
        {
            Moovement();
        }
        SwitchAnimtor();
    }

    void Moovement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float faecdireaction = Input.GetAxisRaw("Horizontal");
        float verticalmove = Input.GetAxis("Vertical");

        //移動
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            animator.SetFloat("running", Mathf.Abs(faecdireaction));
        }
        //轉向
        if (faecdireaction != 0)
        {
            transform.localScale = new Vector3(faecdireaction, 1, 1);
        }

        //爬
        if (!Physics2D.OverlapCircle(cellingPoint.position, 0.2f, ground))
        {
            if (verticalmove < 0.0)
            {
                disCollider.enabled = false;
                animator.SetBool("creeping", true);
            }
            else
            {
                disCollider.enabled = true;
                animator.SetBool("creeping", false);
            }
        }
    }
    //切換動畫
    void SwitchAnimtor()
    {
        animator.SetBool("idleing", false);
        //掉落動畫
        if (rb.velocity.y < 0.1f && !collider.IsTouchingLayers(ground))
        {
            animator.SetBool("falling", true);
        }
        //跳躍動畫
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        else if (collider.IsTouchingLayers(ground))
        {
            animator.SetBool("falling", false);
            animator.SetBool("idleing", true);
        }
        if (isHurt)
        {
            animator.SetBool("hurting", true);

            if (Mathf.Abs(rb.velocity.x) < 0.05f)
            {
                animator.SetBool("idleing", true);
                animator.SetFloat("running", 0);
                animator.SetBool("hurting", false);

                isHurt = false;
            }
        }
    }
    //觸發器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集
        if (collision.tag == "Deadline")
        {
            Invoke("Restart", 2f);
        }

        if (collision.tag == "Collection")
        {
            coinAudio.Play();
            collision.GetComponent<Animator>().Play("Collected");
        }
    }
    //消滅敵人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (animator.GetBool("falling"))
            {
                enemy.jumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
                animator.SetBool("jumping", true);
            }
            else
            {
                hitAudio.Play();
                if (transform.position.x > collision.transform.position.x)
                {
                    isHurt = true;
                    rb.velocity = new Vector2(5, rb.velocity.y);
                }
                else
                {
                    isHurt = true;
                    rb.velocity = new Vector2(-5, rb.velocity.y);
                }

            }
        }
    }
    //重新開始
    void Restart()
    {
        GetComponent<AudioSource>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    //跳
    void jump()
    {
        if (Input.GetButton("Jump") && collider.IsTouchingLayers(ground))
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            animator.SetBool("jumping", true);
        }
    }
    public void cherryAdd()
    {
        cherry++;

    }
}