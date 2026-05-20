using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab is not assigned in BulletPool!");
            return;
        }

        // Pre-warm the pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.transform.SetParent(transform);
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        GameObject bullet = null;

        if (pool.Count > 0)
        {
            bullet = pool.Dequeue();
            bullet.SetActive(true);
        }
        else
        {
            // Expand pool if exhausted
            bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
        }

        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(transform);
        pool.Enqueue(bullet);
    }
}
