using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public float Speed;
    public float laneSpeed;
    public int maxLife = 3;
    public float minSpeed = 10f;
    public float mexSpeed = 30f;
    public float invincibleTime;


    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
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
    }
}

