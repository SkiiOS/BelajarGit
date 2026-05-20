using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    public float maxHP;
    public float moveSpeed;
    public int magazineSize = 10;
    public float reloadTime = 1.5f;
}