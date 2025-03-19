using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float speed = 3f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_player != null)
        {
            // Calcula a dire��o no eixo X
            Vector2 direction = new Vector2(_player.position.x - transform.position.x, 0).normalized;

            // Move o inimigo
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

            // Ativar anima��o de movimento
            bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
            anim.SetBool("isMoving", isMoving);

            // Flip do sprite para virar na dire��o correta
            if (rb.linearVelocity.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (rb.linearVelocity.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            anim.SetBool("isMoving", false); // Se o player sumir, volta para Idle
        }
    }
}





