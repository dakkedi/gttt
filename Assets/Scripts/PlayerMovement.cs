using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed = 5f;

    private Vector2 _movementInput = Vector2.zero;
    private Rigidbody2D _rb = null;
	
	void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
    }

	private void FixedUpdate()
	{
        _rb.MovePosition(_rb.position + (_movementInput.normalized) * _speed * Time.fixedDeltaTime);
	}
}
