using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public float speed;

    private Vector2 pos;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        pos = transform.position;

        Despawn();
    }
    public void Despawn()
    {
        rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;

        if (pos.x <= -13)
        {
            SpawnManager.Instance.ReleaseItem(this);
        }
    }
}
