using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private float floatForce = 20;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    // BONUS NO.9 I - Add a audioclip variable
    public AudioClip BounceSound;

    // BONUS NO.8 I - Add a isLowEnough boolean variable.
    public bool isLowEnough;

    // Extra Variables
    public float Boundary = 9;
    private float AddForceMultiplier = 15;



    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        //-- PROBLEM NO.3 (Add GetComponent)
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        // BONUS NO.8 II - (Add a bool variable to serve as condition on whether the balloon is in a low enough height...)
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y <= Boundary)
        {
            isLowEnough = true;

        }
        else
        {
            isLowEnough = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        // BONUS NO.9 II - (This elseif Basically says that if we hit a gameobject tagged ground the balloon will bounce off the ground and play a sound effect.
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * AddForceMultiplier, ForceMode.Impulse);
            playerAudio.PlayOneShot(BounceSound, 1.5f);

        }

    }

}
