using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtrl : MonoBehaviour
{
    [SerializeField] private float speedScroll;

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= speedScroll* Time.fixedDeltaTime;
        if (pos.x <=-30)
        {
            pos.x = 30;
        }
        transform.position = pos;
    }
}
