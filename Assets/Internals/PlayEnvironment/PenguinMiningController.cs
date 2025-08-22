using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMiningController : MonoBehaviour
{
    [Header("Dependencies")]
    
    [Header("Mining Settings")]
    public float miningDuration = 3f;
    public float sizeReductionAmount = 0.25f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    
    [Header("Prefab Management")]
    public GameObject glacierPrefab;
    public int numberOfMiningNodes = 5;
    public float spawnRadius = 10f;
    public float minDistanceToPrefab = 2f;
    
    private List<Transform> miningPrefabs = new List<Transform>();
    
    private int currentPrefabIndex = 0;
    private bool isMining = false;
    private bool isMoving = false;
    private bool miningStarted = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float miningTimer = 0f;
    
    private PenguinActionRenderer penguinRenderer;
    
    void Start()
    {
        penguinRenderer = GetComponent<PenguinActionRenderer>();
        GenerateMiningNodes();
    }
    
    void Update()
    {
        if (miningPrefabs.Count == 0 || !miningStarted) return;
        
        if (isMoving)
        {
            HandleMovement();
        }
        else if (isMining)
        {
            HandleMining();
        }
        else
        {
            StartMiningSequence();
        }
    }
    
    private void StartMiningSequence()
    {
        if (currentPrefabIndex >= miningPrefabs.Count)
        {
            currentPrefabIndex = 0;
        }
        
        Transform targetPrefab = miningPrefabs[currentPrefabIndex];
        if (targetPrefab == null)
        {
            currentPrefabIndex++;
            return;
        }
        
        Vector3 directionToPrefab = (targetPrefab.position - transform.position).normalized;
        targetPosition = targetPrefab.position - directionToPrefab * minDistanceToPrefab;
        targetPosition.y = transform.position.y;
        
        targetRotation = Quaternion.LookRotation(-directionToPrefab);
        
        isMoving = true;
    }
    
    private void HandleMovement()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
        float angleToTarget = Quaternion.Angle(transform.rotation, targetRotation);
        if (angleToTarget > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        if (distanceToTarget <= 0.1f && angleToTarget <= 1f)
        {
            isMoving = false;
            isMining = true;
            miningTimer = 0f;
        }
    }
    
    private void HandleMining()
    {
        miningTimer += Time.deltaTime;
        
        if (miningTimer >= miningDuration)
        {
            bool nodeDestroyed = MineCurrentPrefab();
            
            if (nodeDestroyed)
            {
                currentPrefabIndex++;
            }
            
            isMining = false;
        }
    }
    
    private bool MineCurrentPrefab()
    {
        if (currentPrefabIndex < miningPrefabs.Count && miningPrefabs[currentPrefabIndex] != null)
        {
            Transform prefab = miningPrefabs[currentPrefabIndex];
            
            Vector3 currentScale = prefab.localScale;
            Vector3 newScale = currentScale - (sizeReductionAmount * Vector3.one);
            
            if (newScale.x > 0.1f && newScale.y > 0.1f && newScale.z > 0.1f)
            {
                prefab.localScale = newScale;
                return false;
            }
            else
            {
                Destroy(prefab.gameObject);
                miningPrefabs.RemoveAt(currentPrefabIndex);
                return true;
            }
        }
        return false;
    }
    
    public void StartMining()
    {
        if (miningPrefabs.Count > 0)
        {
            currentPrefabIndex = 0;
            isMining = false;
            isMoving = false;
            miningStarted = true;
            SetMiningNodesVisibility(true);

        }
    }
    
    public void StopMining()
    {
        isMining = false;
        isMoving = false;
        miningStarted = false;
        SetMiningNodesVisibility(false);
    }
    
    public void AddMiningPrefab(Transform prefab)
    {
        if (prefab != null && !miningPrefabs.Contains(prefab))
        {
            miningPrefabs.Add(prefab);
        }
    }
    
    public void RemoveMiningPrefab(Transform prefab)
    {
        if (miningPrefabs.Contains(prefab))
        {
            miningPrefabs.Remove(prefab);
            if (prefab != null)
            {
                Destroy(prefab.gameObject);
            }
        }
    }
    
    public void ClearAllPrefabs()
    {
        foreach (Transform prefab in miningPrefabs)
        {
            if (prefab != null)
            {
                Destroy(prefab.gameObject);
            }
        }
        miningPrefabs.Clear();
        StopMining();
    }
    
    private void GenerateMiningNodes()
    {
        if (glacierPrefab == null)
        {
            Debug.LogError("Glacier prefab not assigned to PenguinMiningController!");
            return;
        }
        
        ClearAllPrefabs();
        
        for (int i = 0; i < numberOfMiningNodes; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            
            float distanceFromPenguin = Vector3.Distance(spawnPosition, transform.position);
            if (distanceFromPenguin < minDistanceToPrefab)
            {
                Vector3 direction = (spawnPosition - transform.position).normalized;
                spawnPosition = transform.position + direction * minDistanceToPrefab;
            }
            
            GameObject spawnedNode = Instantiate(glacierPrefab, spawnPosition, Quaternion.identity);
            miningPrefabs.Add(spawnedNode.transform);
        }
        
        SetMiningNodesVisibility(false);
        Debug.Log($"Generated {miningPrefabs.Count} mining nodes");
    }
    
    public void RegenerateMiningNodes()
    {
        GenerateMiningNodes();
    }
    
    public int GetMiningNodesCount()
    {
        return miningPrefabs.Count;
    }
    
    public List<Transform> GetMiningNodes()
    {
        return new List<Transform>(miningPrefabs);
    }
    
    private void SetMiningNodesVisibility(bool visible)
    {
        foreach (Transform prefab in miningPrefabs)
        {
            if (prefab != null)
            {
                prefab.gameObject.SetActive(visible);
            }
        }
    }
}
