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

    [Header("Magazine")]
    public int currentAmmo;
    public int maxAmmo = 10;
    public float reloadTime = 1.5f;
    private float reloadTimer = 0f;
    private bool isReloading = false;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerData != null)
        {
            currentHP = playerData.maxHP;
            maxAmmo = playerData.magazineSize;
            reloadTime = playerData.reloadTime;
        }
        else
        {
            currentHP = 100f;
        }
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;
        if (playerInput == null) return;

        // Movement
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;
        float currentSpeed = playerData != null ? playerData.moveSpeed : 5f;
        transform.Translate(new Vector3(h, v, 0) * currentSpeed * Time.deltaTime);

        // Reload sedang berjalan
        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                Debug.Log("Reload complete! Ammo: " + currentAmmo);
            }
            return; // Tidak bisa tembak saat reload
        }

        // Tekan R untuk reload manual
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartReload();
            return;
        }

        // Cooldown timer
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        // Shoot ke arah mouse saat klik kiri
        if (Input.GetMouseButton(0) && shootTimer <= 0)
        {
            if (currentAmmo > 0)
            {
                ShootTowardMouse();
                shootTimer = shootCooldown;
            }
            else
            {
                Debug.Log("Ammo habis! Tekan R untuk reload.");
            }
        }
    }

    void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
        Debug.Log("Reloading... (" + reloadTime + "s)");
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

    void ShootTowardMouse()
    {
        if (BulletPool.Instance == null)
        {
            Debug.LogWarning("BulletPool not found!");
            return;
        }

        // Konversi posisi mouse di screen ke posisi di world space
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Hitung arah dari player ke mouse
        Vector2 direction = (mouseWorld - transform.position).normalized;

        // Ambil bullet dari pool dan setup
        GameObject bulletObj = BulletPool.Instance.GetBullet();
        if (bulletObj != null)
        {
            bulletObj.transform.position = transform.position;
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Setup(direction);
            }
        }

        currentAmmo--;
        Debug.Log("Ammo: " + currentAmmo + "/" + maxAmmo);
    }
}