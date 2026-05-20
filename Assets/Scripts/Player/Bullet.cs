using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    private Vector2 direction;
    private float lifetimeTimer;

    public void Setup(Vector2 dir)
    {
        direction = dir.normalized;
        lifetimeTimer = bulletData != null ? bulletData.lifetime : 2f;

        // Optional: Rotate bullet to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.RadDeg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;

        float speed = bulletData != null ? bulletData.speed : 10f;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0)
        {
            Deactivate();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Deactivate();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        if (BulletPool.Instance != null)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
