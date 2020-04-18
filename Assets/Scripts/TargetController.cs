using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class TargetController : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float speed = 1.0f;

    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;
    private CircleCollider2D m_CircleCollider;
    private Vector2 direction = Vector2.down;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_CircleCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {

        RaycastHit2D[] results = new RaycastHit2D[0]; // TODO: Pre-allocate this
        Vector2 pos = new Vector2(m_Transform.position.x, m_Transform.position.y);
        Vector2 delta = direction * speed * Time.deltaTime;
        m_Rigidbody.MovePosition(pos + delta);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        m_Transform.position = new Vector3(
            (float)Mathf.RoundToInt(m_Transform.position.x),
            (float)Mathf.RoundToInt(m_Transform.position.y),
            (float)Mathf.RoundToInt(m_Transform.position.z));

        Vector2 normal = collision.contacts[0].normal;
        Vector2 originalNormal = normal;

        if (normal.y > normal.x)
        {
            normal.x = 0.0f;
            normal.y = 1.0f;
        }
        else
        {
            normal.x = 1.0f;
            normal.y = 0.0f;
        }

        Vector2 newDirection = new Vector2(normal.y, normal.x);

        // Randomize direction
        if (Random.Range(0, 2) == 1) newDirection *= -1.0f;

        // TODO: Pre-allocate this
        RaycastHit2D[] results = new RaycastHit2D[2];
        // Make sure we aren't about to hit something
        if (m_CircleCollider.Cast(newDirection, results, 0.5f) > 1) newDirection *= -1;
        if (m_CircleCollider.Cast(newDirection, results, 0.5f) > 1) newDirection = originalNormal;

        direction = newDirection;
    }
}
