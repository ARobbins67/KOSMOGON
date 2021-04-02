using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject BeatPrefab;
    [SerializeField] private Vector2 SpawnDelayRange = new Vector2(.5f,1.5f);
    [SerializeField] List<GameObject> Spawners = new List<GameObject>();

    private float BeatSpeed = 8f;
    private BeatController controller;
    private float SpawnDelay;
    private int RandEnd;
    private bool spawning = true;
    private int index;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        RandEnd = Spawners.Count;
        StartCoroutine(Spawn());        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopSpawning()
    {
        spawning = false;
        StopCoroutine(Spawn());
    }

    public float GetSpeed()
    {
        return BeatSpeed;
    }

    public void SetSpeed(float speed)
    {
        BeatSpeed = speed;
    }

    IEnumerator Spawn()
    {
        while (spawning)
        {
            SpawnDelay = Random.Range(SpawnDelayRange.x, SpawnDelayRange.y);
            index = Random.Range(0, RandEnd);
            GameObject BeatObject;
            BeatObject = Instantiate(BeatPrefab,Spawners[index].transform.position,Spawners[index].transform.rotation,
                Spawners[index].transform);
            controller = BeatObject.GetComponent<BeatController>();
            controller.SetSpeed(BeatSpeed);
            controller.GetSpawner(Spawners[index]);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    
}
