using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEditor;

public class FMODController : MonoBehaviour
{
    [SerializeField] private float TransitionToScoreRatio = 1f; // higher ratio means bigger transition change
    [SerializeField] private GameObject BeatPrefab;
    [SerializeField] private float ChangeInSpeed = 4f;
    [SerializeField] private SpawnScript spawnScript;
    
    [FMODUnity.EventRef] private string PlayerStateEvent = "event:/score space";
    private FMOD.Studio.EventInstance playerState;
    private ScoreManager scoreMan;
    private StudioEventEmitter studioEvent;
    private StudioParameterTrigger parameterTrigger;
    private FMODUnity.StudioEventEmitter emitter;
    private TextMeshProUGUI text;
    private int completedRounds = 0;
    private MainCubeController cubeController;
    //private SpawnScript spawnScript;

    private FMOD.Studio.PARAMETER_ID scoreAParameterId;
    private FMOD.Studio.PARAMETER_ID scoreBParameterId;
    private FMOD.Studio.PARAMETER_ID scoreCParameterId;
    private FMOD.Studio.PARAMETER_ID scoreDParameterId;
    private FMOD.Studio.PARAMETER_ID scoreEParameterId;
    private FMOD.Studio.PARAMETER_ID scoreGParameterId;
    private int scoreReset = 0;

    void Awake()
    {
        parameterTrigger = GetComponent<StudioParameterTrigger>();
        emitter = GetComponent<StudioEventEmitter>();
        scoreMan = GameObject.FindObjectOfType<ScoreManager>();
        cubeController = GameObject.FindObjectOfType<MainCubeController>();
        //spawnScript = GameObject.FindObjectOfType<SpawnScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
        playerState.start();
        SetupParameters();
        GetComponent<FMODUnity.StudioParameterTrigger>().TriggerParameters();
    }

    public void SetupParameters()
    {
        FMOD.Studio.EventDescription scoreAEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreAParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score A", out scoreAParameterDescription);
        scoreAParameterId = scoreAParameterDescription.id;

        FMOD.Studio.EventDescription scoreBEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreBParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score B", out scoreAParameterDescription);
        scoreBParameterId = scoreAParameterDescription.id;

        FMOD.Studio.EventDescription scoreCEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreCParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score C", out scoreAParameterDescription);
        scoreCParameterId = scoreAParameterDescription.id;

        FMOD.Studio.EventDescription scoreDEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreDParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score D", out scoreAParameterDescription);
        scoreDParameterId = scoreAParameterDescription.id;

        FMOD.Studio.EventDescription scoreEEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreEParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score E", out scoreAParameterDescription);
        scoreEParameterId = scoreAParameterDescription.id;

        FMOD.Studio.EventDescription scoreGEventDescription;
        playerState.getDescription(out scoreAEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION scoreGParameterDescription;
        scoreAEventDescription.getParameterDescriptionByName("score G", out scoreAParameterDescription);
        scoreGParameterId = scoreAParameterDescription.id;
    }


    // Update is called once per frame 
    void Update()
    {
        playerState.getParameterByID(scoreAParameterId, out float scoreA);
        playerState.getParameterByID(scoreBParameterId, out float scoreB);
        playerState.getParameterByID(scoreCParameterId, out float scoreC);
        playerState.getParameterByID(scoreDParameterId, out float scoreD);
        playerState.getParameterByID(scoreEParameterId, out float scoreE);
        playerState.getParameterByID(scoreGParameterId, out float scoreG);

        //text.text = "Score A: " + scoreA + "\nScore B: " + scoreB + "\nScore C: " + scoreC + "\nScore D: " + scoreD +
          //          "\nScore E: " + scoreE + "\nScore G: " + scoreG;

        if (scoreA == 100 && scoreB < 100)
        {
            cubeController.Fray(1);
        }
    }

    public void UpdateTransition(float scoreChange)
    {
        if (GetTransition(scoreAParameterId) < 100)
        {
            IncrementTransition(scoreAParameterId,GetTransition(scoreAParameterId)+scoreChange*TransitionToScoreRatio);
        }
        else if (GetTransition(scoreBParameterId) < 100)
        {
            IncrementTransition(scoreBParameterId, GetTransition(scoreBParameterId) + scoreChange * TransitionToScoreRatio);

        }
        else if (GetTransition(scoreCParameterId) < 100)
        {
            IncrementTransition(scoreCParameterId, GetTransition(scoreCParameterId) + scoreChange * TransitionToScoreRatio);
        }
        else if (GetTransition(scoreDParameterId) < 100)
        {
            IncrementTransition(scoreDParameterId, GetTransition(scoreDParameterId) + scoreChange * TransitionToScoreRatio);
        }
        else if (GetTransition(scoreEParameterId) < 100)
        {
            IncrementTransition(scoreEParameterId, GetTransition(scoreEParameterId) + scoreChange * TransitionToScoreRatio);
        }
        else if (GetTransition(scoreGParameterId) < 100)
        {
            IncrementTransition(scoreGParameterId, GetTransition(scoreGParameterId) + scoreChange * TransitionToScoreRatio);
        }
        else if (GetTransition(scoreGParameterId) >= 100)
        {
            Loop();
        }
    }

    public void Loop()
    {
        completedRounds += 1;
        BeatController beatController = BeatPrefab.GetComponent<BeatController>();
        IncrementTransition(scoreGParameterId,0);
        spawnScript.SetSpeed(spawnScript.GetSpeed()+.5f);

        /*StopPlaying();
        
        IncrementTransition(scoreAParameterId, 0);
        IncrementTransition(scoreBParameterId, 0);
        IncrementTransition(scoreCParameterId, 0);
        IncrementTransition(scoreDParameterId, 0);
        IncrementTransition(scoreEParameterId, 0);
        IncrementTransition(scoreGParameterId, 0);

        playerState.start();*/
    }

    public void StopPlaying()
    {
        playerState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void IncrementTransition(PARAMETER_ID parameterID, float newValue)
    {
        playerState.setParameterByID(parameterID, newValue);
    }

    public float GetTransition(PARAMETER_ID parameterID)
    {
        playerState.getParameterByID(parameterID, out float value);
        return value;
    }
}
