using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] Vector2 _move;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _jumpF;
    [SerializeField] bool _checkGround;
    [SerializeField] Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
   
    private void Update()
    {
        _rb.linearVelocity = new Vector2(_move.x * _speed, _rb.linearVelocity.y);

        // Atualiza a anima��o de movimento
        _animator.SetFloat("Speed", Mathf.Abs(_move.x));

        // Ativa a anima��o de queda quando estiver caindo
        if (!_checkGround && _rb.linearVelocity.y < 0)
        {
            _animator.SetBool("IsFalling", true);
        }


        // Ajusta a dire��o do sprite
        if (_move.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_move.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
    }

    public void SetJump(InputAction.CallbackContext value)
    {
        if (_checkGround && value.performed)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * _jumpF, ForceMode2D.Impulse);

            // Ativa a anima��o de pulo
            _animator.SetBool("IsJumping", true);
            _animator.SetBool("IsFalling", false); // Garante que n�o entre em queda imediatamente
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _checkGround = true;

        // Volta para Idle ao tocar o ch�o
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("IsFalling", false);
        _animator.SetBool("IsGrounded", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _checkGround = false;
        _animator.SetBool("IsGrounded", false);
    }
}

