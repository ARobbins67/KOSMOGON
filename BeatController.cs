using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    [SerializeField] GameObject LeftSpawner;
    [SerializeField] GameObject UpSpawner;
    [SerializeField] GameObject RightSpawner;
    [SerializeField] ParticleSystem DeathParticles;

    private float BeatScoreValue = 10f;

    private SpawnScript spawnScript;
    private GameObject MainCube;
    private BoxCollider2D cubeCol;
    private ParticleSystem particles;
    private ScoreManager score;
    private bool clickable = false;
    private Animator anim;
    private bool emitting = false;
    private bool wasHit = false;
    private int Index;
    private MainCubeController cubeController;
    private string align;
    private GameObject Spawner;
    private float speed = 8f;
    

    void Awake()
    {
        MainCube = GameObject.FindGameObjectWithTag("Main Cube");
        cubeCol = MainCube.GetComponent<BoxCollider2D>();
        score = GameObject.FindObjectOfType<ScoreManager>();
        spawnScript = GetComponentInParent<SpawnScript>();
        anim = GetComponent<Animator>();
        particles = GetComponentInChildren<ParticleSystem>();
        cubeController = MainCube.GetComponent<MainCubeController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up*.01f*speed);
    }

    void Update()
    {
        CheckClick();
        
    }

    private void CheckClick()
    {
        if (clickable)
        {
            CubeState state = cubeController.GetAlignment();

            //left spawner
            if (Spawner == LeftSpawner && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                particles.Play();
                emitting = true;
                wasHit = true;
                score.IncrementScore(BeatScoreValue);
                clickable = false;
            }

            // up spawner
            else if (Spawner == UpSpawner && state == CubeState.Up)
            {
                particles.Play();
                emitting = true;
                wasHit = true;
                score.IncrementScore(BeatScoreValue);
                clickable = false;
            }
            if (Spawner == RightSpawner && state == CubeState.Right)
            {
                particles.Play();
                emitting = true;
                wasHit = true;
                score.IncrementScore(BeatScoreValue);
                clickable = false;
            }
        }

        if (!particles.IsAlive() && wasHit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == cubeCol)
        {
            clickable = true;
        }
    }

    private void StartDeath()
    {

    }

    public void GetSpawner(GameObject spawner)
    {
        Spawner = spawner;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col == cubeCol)
        {
            if (!wasHit)
            {
                // fade out
                anim.enabled = true;
                clickable = false;
                score.AddStrike();
            }
        }
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    void DestroyBeat()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Played particles");
        DeathParticles.Play();
    }
}
