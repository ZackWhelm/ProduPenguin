using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PenguinState
{
    Idle,
    Watching,
    Mining,
    Studying
}

public class PenguinActionRenderer : MonoBehaviour
{
    [Header("State Management")]
    public PenguinState currentState = PenguinState.Idle;
    public bool isPaused = false;
    
    [Header("Screen Watching")]
    public Transform targetTransform;
    public float watchSpeed = 5f;
    
    [Header("Jumping Behavior")]
    public float jumpForce = 5f;
    
    [Header("State Behaviors")]
    public float idleBobSpeed = 2f;
    public float idleBobHeight = 0.5f;
    public float miningSpeed = 3f;
    public float miningIntensity = 1f;
    public float studyingSpeed = 1f;
    public float studyingIntensity = 0.3f;
    
    private bool isJumping = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float stateTimer = 0f;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isPaused) return;
        
        stateTimer += Time.deltaTime;
        
        switch (currentState)
        {
            case PenguinState.Idle:
                HandleIdleState();
                break;
            case PenguinState.Watching:
                HandleWatchingState();
                break;
            case PenguinState.Mining:
                HandleMiningState();
                break;
            case PenguinState.Studying:
                HandleStudyingState();
                break;
        }
    }
    
    public void SetState(PenguinState newState)
    {
        if (isPaused) return;
        
        currentState = newState;
        stateTimer = 0f;
        
        switch (newState)
        {
            case PenguinState.Idle:
                StopAllCoroutines();
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                break;
            case PenguinState.Watching:
                StopAllCoroutines();
                transform.rotation = originalRotation;
                break;
            case PenguinState.Mining:
                StopAllCoroutines();
                transform.rotation = originalRotation;
                StartCoroutine(MiningSequence());
                break;
            case PenguinState.Studying:
                StopAllCoroutines();
                transform.rotation = originalRotation;
                StartCoroutine(StudyingSequence());
                break;
            default:
                PenguinMiningController miningController = GetComponent<PenguinMiningController>();
                if (miningController != null)
                {
                    miningController.StopMining();
                }
                break;
        }
    }

    public void SetStateFromGenre(ActivityGenre genre) {
        switch (genre) {
            case ActivityGenre.Working:
                SetState(PenguinState.Mining);
                break;
            case ActivityGenre.Studying:
                SetState(PenguinState.Studying);
                break;
            case ActivityGenre.Playing:
                SetState(PenguinState.Watching);
                break;
            case ActivityGenre.Rest:
                SetState(PenguinState.Idle);
                break;
            case ActivityGenre.Recap:
                SetState(PenguinState.Idle);
                break;
        }
    }
    
    public void Pause()
    {
        isPaused = true;
        StopAllCoroutines();
    }
    
    public void Resume()
    {
        isPaused = false;
        SetState(currentState);
    }
    
    private void HandleIdleState()
    {
        float jumpProgress = Mathf.Sin(stateTimer * idleBobSpeed * 2f); // Double the speed
        float jumpHeight = Mathf.Max(0, jumpProgress) * idleBobHeight;
        
        jumpHeight = Mathf.Pow(jumpHeight / idleBobHeight, 2.5f) * idleBobHeight;
        
        transform.position = originalPosition + Vector3.up * jumpHeight;
    }
    
    private void HandleWatchingState()
    {
        if (targetTransform != null)
        {
            LookAtTarget();
        }
    }
    
    private void HandleMiningState()
    {
        // The mining behavior is now handled by PenguinMiningController
        // This method can be used for additional mining animations if needed
    }
    
    private void HandleStudyingState()
    {

    }
    
    public void StartWatchScreen(Transform target)
    {
        targetTransform = target;
        SetState(PenguinState.Watching);
    }
    
    public void StopWatchScreen()
    {
        targetTransform = null;
        SetState(PenguinState.Idle);
    }
    
    private void LookAtTarget()
    {
        Vector3 direction = targetTransform.position - transform.position;
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, watchSpeed * Time.deltaTime);
        }
    }
    

    
    private IEnumerator MiningSequence()
    {
        // Start the mining controller when entering mining state
        PenguinMiningController miningController = GetComponent<PenguinMiningController>();
        if (miningController != null)
        {
            miningController.StartMining();
        }
        
        yield return null;
    }
    
    private IEnumerator StudyingSequence()
    {
        yield return null;
    }
}
