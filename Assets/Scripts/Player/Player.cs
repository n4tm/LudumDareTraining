using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRig;
    private void Awake()
    {
        gameObject.AddComponent<Rigidbody2D>();
        _playerRig = GetComponent<Rigidbody2D>();
    }
}
