using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Menu;
using UnityEngine;

public class GameController : MonoBehaviour, IGameStart
{
    public CameraController cameraController;
    public Transform[] playerSpawnLocations;
    public Transform[] enemySpawnLocations;

    public GameObject playerPrefab;
    public GameObject shockwaveSpellPrefab;
    public GameObject enemyPrefab;

    public Transform playersGroup;
    public Transform enemiesGroup;

    [Tooltip("Seconds how long wave spawning is delayed")]
    public float waveDelay = 3f;
    [Tooltip("How many enemies are initially spawned per player")]
    public int initalWaveSizePerGolem = 2;
    [Tooltip("How many enemies are added per wave per player (sum will be rounded down)")]
    public float addEnemyPerWavePerPlayer = 1.5f;

    private int enemiesToSpawn = 0;
    private int playerCount = 0;
    private int waveCount = 1;
    private bool spawning = true;

    void FixedUpdate()
    {
        if (enemiesToSpawn > 0)
        {
            foreach (var spawn in enemySpawnLocations
                .Where(s => FindObjectsOfType<Entity>().All(e => (s.transform.position - e.transform.position).sqrMagnitude > 1f)))
            {
                --enemiesToSpawn;
                Instantiate(enemyPrefab, spawn.position, Quaternion.identity, enemiesGroup);
                if(enemiesToSpawn <= 0) { break; }
            }
        }

        if(enemiesGroup.childCount <= 0 && !spawning)
        {
            spawning = true;
            Invoke("InitWave", waveDelay);
        }
    }

    private void InitWave()
    {
        var waveSize = (int) Mathf.Floor((initalWaveSizePerGolem + addEnemyPerWavePerPlayer * waveCount) * playerCount);
        waveCount++;
        spawning = false;
        SpawnWave(waveSize);
    }

    private void SpawnWave(int size)
    {
        enemiesToSpawn = size;
    }

    public void StartGame(MenuData data)
    {
        //Turn camera
        cameraController.SwitchToGame();

        //Start the game
        var letter = 'A';
        var possibleSpawns = new Stack<Transform>(playerSpawnLocations.ToList());
        waveCount = 1;
        playerCount = data.GetGolems().Count();
        foreach (var golem in data.GetGolems())
        {
            if (possibleSpawns.Count <= 0)
            {
                Debug.LogError("Too many golems. Next golem: " + golem.Color.ToString());
                continue;
            }
            //Spawn player and add spell
            var spawn = possibleSpawns.Pop();
            var player = Instantiate(playerPrefab, spawn.position, Quaternion.identity, playersGroup);
            Instantiate(shockwaveSpellPrefab, player.transform.position, Quaternion.identity, player.transform);

            //Assign the player a unique name (so he will be controlled by different keys)
            player.GetComponent<Player>().playerName = letter.ToString();
            ++letter;
        }
        spawning = true;
        Invoke("InitWave", waveDelay);
    }
}
