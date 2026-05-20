using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Game/Bullet Data")]
public class BulletData : ScriptableObject
{
    public float speed = 10f;
    public float damage = 10f;
    public float lifetime = 2f;
}
