using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public float speedMultiplier;
    public float speedMultiplierOrig;
    private Vector2[] randomVelocity = new Vector2[] { new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, -0.5f)};
    public Vector2 velocity;
    public Animator uiAnimator;
    public TextMeshProUGUI uiText;
    public Player player;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioSource audioSource;
    public AudioSource music;

    void Start()
    {
       speedMultiplierOrig = speedMultiplier;
       velocity = randomVelocity[Random.Range(0, 4)];
    }

    void Update()
    {
        transform.position += new Vector3(velocity.x, velocity.y) * Time.deltaTime * speedMultiplier;
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * speedMultiplier * 100);

        if(transform.position.x < -10 || transform.position.x > 10 || transform.position.y > 5 || transform.position.y < -5)
        {
            ResetLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Vertical Paddle" || col.gameObject.name == "Horizontal Paddle")
        {
            if (col.gameObject.name == "Vertical Paddle")
            {
                velocity.x = -velocity.x;
            }

            if (col.gameObject.name == "Horizontal Paddle")
            {
                velocity.y = -velocity.y;
            }

            uiAnimator.SetTrigger("ScoreEffect");
            player.score++;
            uiText.text = player.score.ToString();
            speedMultiplier += .1f;
            audioSource.pitch = Random.Range(.8f, 1.3f);
            audioSource.PlayOneShot(hitSound);
        }
    }

    void ResetLevel()
    {
        music.Stop();
        transform.position = Vector2.zero;
        player.score = 0;
        uiText.text = "OOPS!";
        velocity = randomVelocity[Random.Range(0, 4)];
        speedMultiplier = speedMultiplierOrig;
        audioSource.PlayOneShot(dieSound);
        music.Play();
    }
}
