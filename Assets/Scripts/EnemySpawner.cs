using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Tilemap _groundTiles;
    [SerializeField] float _spawnCooldown;
    [SerializeField] List<Enemy> _enemyPrefabs;
    [SerializeField] float _spawnCooldownReductionMultiplier;
    [SerializeField] Transform _player;
    [SerializeField] float _minimumSpawnDistanceFromPlayer = 5f;
    float _currentCooldown;
    List<Vector3> _spawnPositions = new();

    void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(HandleGameDifficultyIncrease), 1f, 1f);
    }
     void Update()
    {
        HandleEnemySpawing();
    }
    void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }
    void HandleEnemySpawing()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > Time.time)
            return;

        _currentCooldown = Time.time + _spawnCooldown;
        SpawnEnemyToRandomLocation();
    }
    Vector3 GetRandomPosition()
    {
    for (int i = 0; i < 100; i++)
    {
        Vector3 randomPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];

        if (Vector3.Distance(randomPosition, _player.position) >= _minimumSpawnDistanceFromPlayer)
        {
            return randomPosition;
        }
    }

    return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
    }

    void SpawnEnemyToRandomLocation()
    {
         if (_enemyPrefabs.Count == 0)
        return;

        Enemy randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

        Instantiate(randomEnemy, GetRandomPosition(), Quaternion.identity);
    }

    void HandleGameDifficultyIncrease()
    {
        _spawnCooldown *= _spawnCooldownReductionMultiplier;
    }

}
