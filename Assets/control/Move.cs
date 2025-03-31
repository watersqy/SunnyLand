using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;//����ǰ���[SerializeField]�����ô�����unity�п���
    private Animator anim;

    public GameObject relive;

    public Collider2D coll;
    public Collider2D discoll;
    public Transform cellingcheck, groundcheck;
    public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource reAudio;
    public float speed;
    public float jumpforce;
    public float climbspeed;
    public LayerMask ground;
    private int cherry = 0;
    private int gem = 0;

    public AudioSource cherryAudio;
    public AudioSource gemAudio;
    public Text cherrynum;
    public Text gemnum;
    private bool ishurt;//Ĭ��false
    private bool nextlevel;
    private bool isground;
    private bool isladder;
    private int extrajump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()//��Updateǰ��Fix���Ը��ݵ�������Э����Ϸ
    {
        if (!ishurt || Input.GetButtonDown("re"))//��ֹmovement����һֱִ��
        {
            Movement();
        }
        Switchanim();
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, ground);
        cherrynum.text = cherry.ToString();
        gemnum.text = gem.ToString();
    }

    private void Update()
    {
        Jump();
        Crouch();
        Restart();
        if (nextlevel == true && Input.GetKeyDown(KeyCode.E))
        {
            Next();
        }
        newJump();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        float verticalmove = Input.GetAxisRaw("Vertical");

        //�ƶ�
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);//* Time.deltaTime��������ƽ��������֡
            anim.SetFloat("running", Mathf.Abs(facedirection));//Absָ����ֵ
        }

        //ת��
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        if(isladder&&Mathf.Abs(verticalmove)>0f)
        {
            rb.velocity =new Vector2 (verticalmove * climbspeed * Time.deltaTime, verticalmove * climbspeed * Time.deltaTime);//Ҫ�Ľ�
        }

    }

    //anim�л�
    void Switchanim()
    {
        anim.SetBool("idle", false);

        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (ishurt)
        {
            hurtAudio.Play();
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                ishurt = false;
                anim.SetBool("idle", true);
                anim.SetBool("hurt", false);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }

    }

    //��Ծ
    void Jump()
    {

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(0, jumpforce);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }

    //�¶�
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingcheck.position, 0.2f, ground))
        {

            if (Input.GetButton("crouch")&&isladder!)
            {
                discoll.enabled = false;
                anim.SetBool("crouching", true);
            }
            else
            {
                discoll.enabled = true;
                anim.SetBool("crouching", false);

            }
        }
    }


    //������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cherry")
        {
            cherryAudio.Play();
            collision.GetComponent<Animator>().Play("cherryget");
            //cherry += 1;
            
        }
        if (collision.tag == "gem")
        {
            gemAudio.Play();
            collision.GetComponent<Animator>().Play("gemget");
            //gem += 1;
            
        }
        if (collision.tag == "deadline")
        {
            relive.SetActive(true);
            GetComponent<AudioSource>().enabled = false;
        }
        if (collision.tag == "level")
        {
            nextlevel = true;
            relive.SetActive(true);
        }

        if (collision.tag == "ladder")
        {
            rb.gravityScale = 0f;
            anim.SetBool("idle", false);
            anim.SetBool("climbing", true);
            isladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//�뿪����
    {
        if (collision.tag == "ladder")
        {
            anim.SetBool("idle", true);
            anim.SetBool("climbing", false);
            isladder = false;
            rb.gravityScale = 1f;
        }
    }

    //�������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.Jumpon();
                rb.velocity = new Vector2(0, jumpforce);
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                ishurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                ishurt = true;
            }
        }
    }

    void Restart()
    {
        if (Input.GetButtonDown("re"))
        {
            reAudio.Play();
            relive.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Invoke("Restart", 2f);�����ú����ӳ�����2s
        }
    }

    void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void newJump()
    {
        if (isground)
        {
            extrajump = 1;
        }
        if (Input.GetButtonDown("Jump") && extrajump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extrajump--;
            anim.SetBool("jumping", true);
        }
        if (Input.GetButtonDown("Jump") && extrajump == 0 && isground)
        {
            rb.velocity = Vector2.up * jumpforce;
            anim.SetBool("jumping", true);
        }
    }

    public void cherrycount()
    {
        cherry += 1;
    }

    public void gemcount()
    {
        gem += 1;
    }
}
