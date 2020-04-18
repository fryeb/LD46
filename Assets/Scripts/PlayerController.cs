using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float speed = 1.0f;
    [Range(0.0f, 1.0f)]
    public float threshold = 0.5f;

    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x * x + y * y <= threshold * threshold) {
            m_Rigidbody.MovePosition(Snap(m_Transform.position));
            return;
        }

        Vector2 pos = m_Transform.position;
        Vector2 dest = Snap(new Vector2(x, y) + pos);
        Vector2 delta = (dest - pos).normalized * speed;
        m_Rigidbody.velocity = delta;
    }
}
