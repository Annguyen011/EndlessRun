using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float jumpForce;
    private Animator animator;
    private bool isGroured;
    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {

        isGroured = Physics2D.OverlapCircle(check.position, radius, layerMask);
        if (Input.GetKey(KeyCode.Space) && isGroured)
        {
            AudioManager.Instance.OnJump();
            rb.AddForce(Vector2.up * jumpForce);       
        }
        SetAnimationJump();
    }
    public void SetAnimationJump()
    {
        if (isGroured)
        {
            animator.SetBool("isJump", false);
        }
        else
        {
            animator.SetBool("isJump", true);
        }
    }
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            AudioManager.Instance.OnGameover();
            GameManager.Instance.SetState(GameState.Gameover);
        }
    }
}
