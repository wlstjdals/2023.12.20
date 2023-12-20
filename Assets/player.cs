using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player : MonoBehaviour
{
    public float Speed;
    public float laneSpeed;
    public float jumpLength;
    public float jumpHeigth;
    public float slideLength;
    public int maxLife = 3;
    public float minSpeed = 10f;
    public float mexSpeed = 30f;
    public float invincibleTime;
    public GameObject model;


    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    private bool jumping = false;
    private float jumpStart;
    private bool sliding = false;
    private float slideStart;
    private Vector3 boxColliderSize;
    private int currentLife;
    private bool invincible = false;
    static int blinkingValue;
    private UIManager uiManager;
    private int coins;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        anim.Play("runStart");
        currentLife = maxLife;
        Speed = minSpeed;
        blinkingValue = Shader.PropertyToID("_BlinkingValue");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            changeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            changeLane(1);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if (ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeigth;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }
        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if (ratio >= 1f)
            {
                sliding = false;
                anim.SetBool("Sliding", false);
                boxCollider.size = boxColliderSize;
            }
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * Speed;

    }



    void changeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }
    void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", Speed / jumpLength);
            anim.SetBool("Jumping", true);
            jumping = true;
        }
    }
    void Slide()
    {
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            anim.SetFloat("JumpSpeed", Speed / slideLength);
            anim.SetBool("Sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            sliding = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            uiManager.UpdateCoins(coins);
            other.gameObject.SetActive(false);
        }
        if (invincible)
            return;


        if (other.CompareTag("Obstacle"))
        {
            currentLife--;
            anim.SetTrigger("Hit");
            Speed = 4;

            if (currentLife <= 0)
            {
                StartCoroutine(Blinking(invincibleTime, true));
            }
            else
            {
                StartCoroutine(Blinking(invincibleTime, false));
            }
        }
    }

    IEnumerator Blinking(float time, bool gameOver)
    {
        invincible = true;
        float timer = 0;
        float currertBlink = 1f;
        float IastBlink = 0;
        float blinkPeriod = 0.1f;

        yield return new WaitForSeconds(1f);

        Speed = minSpeed;

        while (timer < time && invincible)
        {
            Shader.SetGlobalFloat(blinkingValue, currertBlink);
            yield return null;
            timer += Time.deltaTime;
            IastBlink += Time.deltaTime;

            if (blinkPeriod < IastBlink)
            {
                IastBlink = 0;
                currertBlink = 1f - currertBlink;
            }
        }

        Shader.SetGlobalFloat(blinkingValue, 0);
        invincible = false;
    }
}