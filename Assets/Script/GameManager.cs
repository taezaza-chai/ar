
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARSession ARSession;

    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject enmeyPrefab;

    [Header("Enemy Settings")]
    [SerializeField] private int enemyCount = 1;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float despawnRate = 5f;

    private bool _gameStarted = false;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _score = 0;

    void Start()
    {
        ARSession = FindFirstObjectByType<ARSession>();
        //Called from UI button
        UIManager.OnStartButtonPressed += StartGame;
        UIManager.OnRestartButtonPressed += RestartGame;
    }

    void StartGame()
    {
        if (_gameStarted) return;
        _gameStarted = true;
        print("Game started!!!");

        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            var meshVisual = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (meshVisual) meshVisual.enabled = false;

            var lineVisual = plane.GetComponent<LineRenderer>();
            if (lineVisual) lineVisual.enabled = false;
        }
        StartCoroutine(SpawnEnemies());
    }

    void RestartGame()
    {
        _gameStarted = false;
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        while (ARSession.state != ARSessionState.SessionTracking)
        {
            yield return null;
        }
        ARSession.Reset();
        planeManager.enabled = true;
        _score = 0;
        UIManager.UpdateDateScore(_score);

        foreach(var enemy in _spawnedEnemies)
        {
            Destroy(enemy);
        }
        _spawnedEnemies.Clear();
    }

    void SpawnEnemy()
    {
        if (planeManager.trackables.count == 0) return;

        List<ARPlane> planeslist = new List<ARPlane>();
        foreach(var plane in planeManager.trackables)
        {
            planeslist.Add(plane);
        }
        var randomPlane = planeslist[Random.Range(0, planeslist.Count)];
        var randomPosition = GetRandomPosition(randomPlane);
        var enemy = Instantiate(enmeyPrefab,randomPosition,Quaternion.identity);
        _spawnedEnemies.Add(enemy);

        var enemyScript = enemy.GetComponent<EnemyScript>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDestroyed += AddScore;
        }

        StartCoroutine(DespawnEnemies(enemy));
    }

    Vector3 GetRandomPosition(ARPlane plane)
    {
        var center = plane.center;
        var size = plane.size * 0.5f;
        var randomX = Random.Range(-size.x, size.x);
        var randomY = Random.Range(size.y, size.y);
        return new Vector3(center.x + randomX, center.y + randomY, center.z + randomY);
    }

    IEnumerator SpawnEnemies()
    {
        while (_gameStarted)
        {
            if(_spawnedEnemies.Count < enemyCount)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator DespawnEnemies(GameObject enemy)
    {
        yield return new WaitForSeconds(despawnRate);
        if (_spawnedEnemies.Contains(enemy))
        {
            _spawnedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    void AddScore()
    {
        _score++;
        UIManager.UpdateDateScore(_score);
    }
}
