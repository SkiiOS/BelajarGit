using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream
    public PlayerData playerData;
    public float currentHP;
=======
<<<<<<< Updated upstream
    public float currentHP = 100;
    public float speed = 5f;
=======
    public PlayerData playerData;

    public float currentHP;
>>>>>>> Stashed changes
>>>>>>> Stashed changes
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
    }
    
    
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

<<<<<<< Updated upstream
        float currentSpeed = playerData != null ? playerData.moveSpeed : 5f;
        transform.Translate(new Vector3(h, v, 0) * currentSpeed * Time.deltaTime);
=======
<<<<<<< Updated upstream
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
=======
        float currentSpeed = playerData != null ? playerData.moveSpeed : 5f;
        transform.Translate(new Vector3(h, v, 0) * currentSpeed * Time.deltaTime);

        if (attackInput > 0) 
        {
            Shoot();
        }
>>>>>>> Stashed changes
>>>>>>> Stashed changes
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
        Debug.Log("Player shoots!");
    }
}