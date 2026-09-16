using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private Animator anim;
    [SerializeField] private SanityManager sanityManager;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    // SanityPlayerObserver controla esto ahora
    private float speedMultiplier = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Público para que SanityPlayerObserver lo llame
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    void Update()
    {
        movement = Vector2.zero;

        // Velocidad final = speed base × multiplicador de cordura
        float currentSpeed = speed * speedMultiplier;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
            movement.x = -1;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            movement.x = 1;
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, currentSpeed * Time.deltaTime, 0);
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -currentSpeed * Time.deltaTime, 0);
            movement.y = -1;
        }

        if (movement != Vector2.zero)
            AudioManager.Instance.StartFootsteps();
        else
            AudioManager.Instance.StopFootsteps();

        anim.SetBool("isMoving", movement != Vector2.zero);
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DangerZone"))
            sanityManager.SetDraining(true);
        if (other.CompareTag("Objective"))
            GameManager.Instance.ObjectiveReached(false);
        if (other.CompareTag("Bed"))
            GameManager.Instance.ObjectiveReached(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DangerZone"))
            sanityManager.SetDraining(false);
    }
}