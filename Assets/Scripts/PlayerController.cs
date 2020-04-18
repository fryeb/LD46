using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float speed = 1.0f; 

    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 delta = new Vector2(x, y) * speed * Time.deltaTime;
        Vector2 pos = new Vector2(m_Transform.position.x, m_Transform.position.y);
        m_Rigidbody.MovePosition(pos + delta);
    }
}
