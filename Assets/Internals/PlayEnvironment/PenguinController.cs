using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    
    [Header("References")]
    public Camera renderCamera;
    
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isJumping = false;
    private Vector3 originalPosition;
    
    public void Jump() {
        if (!isJumping) {
            StartCoroutine(PerformJump());
        }
    }

    public void MoveTo(Vector3 destination) {
        if (!isMoving) {
            targetPosition = new Vector3(
                Mathf.Round(destination.x / gridSize) * gridSize,
                transform.position.y,
                Mathf.Round(destination.z / gridSize) * gridSize
            );
            StartCoroutine(PerformMove());
        }
    }

    public void FaceDirection(Vector3 direction) {
        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    public void FaceScreen() {
        if (renderCamera != null) {
            Vector3 directionToCamera = renderCamera.transform.position - transform.position;
            directionToCamera.y = 0;
            if (directionToCamera != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera);
                transform.rotation = targetRotation;
            }
        }
    }
    
    private IEnumerator PerformJump() {
        isJumping = true;
        originalPosition = transform.position;
        float elapsedTime = 0f;
        
        while (elapsedTime < jumpDuration) {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / jumpDuration;
            float height = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
            
            transform.position = originalPosition + Vector3.up * height;
            yield return null;
        }
        
        transform.position = originalPosition;
        isJumping = false;
    }
    
    private IEnumerator PerformMove() {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / moveSpeed;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }
        
        transform.position = targetPosition;
        isMoving = false;
    }
}