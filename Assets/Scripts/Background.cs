using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Material m_Material;
    [SerializeField] float speed;
    [SerializeField] float distance;
    private void Awake()
    {
        m_Material = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        distance += Time.deltaTime * speed;
        m_Material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
