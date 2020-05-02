using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void ApplyForce(Vector2 force, ForceMode2D forceMode)
    {
        rb.AddForce(force, forceMode);
    }
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
    static public Vector2 RotateVector(Vector2 vec, float angleDeg)
    {
        float angleRad = angleDeg * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angleRad);
        float cos = Mathf.Cos(angleRad);
        return new Vector2(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);
    }
    public void RotateDirection(float angleDeg)
    {
        ChangeVelocityDirection(RotateVector(Velocity, angleDeg));
    }
    public void SetDirection(float angleDeg)
    {
        ChangeVelocityDirection(RotateVector(new Vector2(1,0), angleDeg));
    }
    public void ChangeVelocityDirection(Vector2 direction)
    {
        rb.velocity = direction.normalized * Velocity.magnitude;
    }
    public Vector2 Velocity
    {
        get => rb.velocity;
        set
        {
            rb.velocity = value;
        }
    }
}
