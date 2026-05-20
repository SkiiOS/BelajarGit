using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerData playerData;
    public float currentHP;

    [Header("Shooting")]
    public float shootCooldown = 0.3f;
    private float shootTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float attackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerData != null)
        {
            currentHP = playerData.maxHP;
        }
        else
        {
            currentHP = 100f; // Fallback
        }
    }
    
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        float currentSpeed = playerData != null ? playerData.moveSpeed : 5f;
        transform.Translate(new Vector3(h, v, 0) * currentSpeed * Time.deltaTime);

        // Track last move direction for shooting
        if (moveInput.sqrMagnitude > 0.001f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        // Cooldown timer
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        // Shoot when attack is pressed and cooldown has elapsed
        if (attackInput > 0 && shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Shoot()
    {
        if (BulletPool.Instance != null)
        {
            GameObject bulletObj = BulletPool.Instance.GetBullet();
            if (bulletObj != null)
            {
                bulletObj.transform.position = transform.position;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.Setup(lastMoveDirection);
                }
            }
        }
        else
        {
            Debug.LogWarning("BulletPool not found! Player shoots!");
        }
    }
}