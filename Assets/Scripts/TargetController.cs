using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public enum TargetState
{
    THINK,
    EXPLORE,
    DOOR,
    DEAD
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class TargetController : MonoBehaviour
{
    public TargetState state = TargetState.THINK;

    [Range(0.1f, 10.0f)]
    public float thinkTime = 1.0f;
    [Range(0.1f, 10.0f)]
    public float speed = 1.0f;
    [Range(0.1f, 2.0f)]
    public float senseRadius = 1.0f;
    [Range(0.1f, 10.0f)]
    public float doorEntrySpeed = 1.0f;

    private float thinkRemaining;

    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;
    private CircleCollider2D m_CircleCollider;

    private Vector2 currentDirection = Vector2.zero;
    private DoorController door = null;

    void Start()
    {
        thinkRemaining = thinkTime;
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_CircleCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case TargetState.THINK:
                m_Transform.position = Snap(m_Transform.position);
                thinkRemaining -= Time.deltaTime;
                if (thinkRemaining <= 0.0f)
                {
                    if (IsNearDoor())
                    {
                        state = TargetState.DOOR;
                    }
                    else
                    {
                        thinkRemaining = thinkTime;
                        state = TargetState.EXPLORE;
                        currentDirection = PickDirection();
                    }
                }
                break;
            case TargetState.EXPLORE:
                if (IsNearDoor()) {
                    m_Transform.position = Snap(m_Transform.position);
                    state = TargetState.THINK;
                }
                else if (IsDirectionBlocked(currentDirection)) {
                    m_Transform.position = Snap(m_Transform.position);
                    state = TargetState.THINK;
                }
                else
                {
                    Vector2 pos = m_Transform.position;
                    Vector2 delta = currentDirection * speed * Time.fixedDeltaTime;
                    m_Rigidbody.MovePosition(pos + delta);
                }
                break;
            case TargetState.DOOR:
                {
                    currentDirection = door.transform.position - m_Transform.position;
                    currentDirection.Normalize();
                    Vector2 pos = m_Transform.position;
                    Vector2 delta = currentDirection * doorEntrySpeed * Time.fixedDeltaTime;
                    m_Rigidbody.MovePosition(pos + delta);
                }
                break;
        }
    }

    bool IsNearDoor()
    {
        Vector3 pos = Snap(m_Transform.position);
        if (Vector3.Distance(pos, m_Transform.position) > senseRadius) return false;

        Vector2[] options = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (var option in options) {
            var hit = Physics2D.Raycast(pos, option, 1.0f);
            if (hit.collider == null) continue;
            door = hit.collider.GetComponent<DoorController>();
            if (door == null) continue;
            
            // Hit door
            m_CircleCollider.enabled = false;
            m_Transform.position = pos;
            return true;
        }

        return false;
    }

    bool IsDirectionBlocked(Vector2 direction)
    {
        bool result = Physics2D.Raycast(m_Transform.position, direction, senseRadius).collider != null;
        return result;
    }

    Vector2 PickDirection()
    {
        Vector2[] options = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        int i = options.Length;
        while (i > 1)
        {
            int j = Random.Range(0, i);
            i--;
            Vector2 temp = options[i];
            options[i] = options[j];
            options[j] = temp;
        }

        foreach (var option in options)
        {
            if (!IsDirectionBlocked(option))
                return option;
        }

        // Unable to find valid option
        state = TargetState.THINK;
        return Vector2.zero;
    }
}
