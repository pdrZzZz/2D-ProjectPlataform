using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float speed = 3f;
    [SerializeField] float stopDistance = 0.5f; // Distância mínima antes de parar
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
            // Calcula a diferença de posição no eixo X
            float distanceX = _player.position.x - transform.position.x;

            // Verifica se a distância é maior que o valor de parada
            if (Mathf.Abs(distanceX) > stopDistance)
            {
                // Normaliza a direção para evitar variações bruscas
                Vector2 direction = new Vector2(Mathf.Sign(distanceX), 0);

                // Move o inimigo
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

                // Ativar animação de movimento
                anim.SetBool("isMoving", true);

                // Flip do sprite para virar na direção correta
                transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);

            }
            else
            {
                // Para a movimentação caso o player esteja muito próximo
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            anim.SetBool("isMoving", false); // Se o player sumir, volta para Idle
        }
    }
}





