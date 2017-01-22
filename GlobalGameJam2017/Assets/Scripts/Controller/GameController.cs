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

    private int enemiesToSpawn = 0;

    void Start()
    {
    }

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
        foreach (var golem in data.Golems)
        {
            //Spawn player and add shockwave spell
            var spawn = possibleSpawns.Pop();
            var player = Instantiate(playerPrefab, spawn.position, Quaternion.identity, playersGroup);
            Instantiate(shockwaveSpellPrefab, player.transform.position, Quaternion.identity, player.transform);

            //Assign the player a unique name (so he will be controlled by different keys)
            player.GetComponent<Player>().playerName = letter.ToString();
            ++letter;
        }
        SpawnWave(5);
    }
}
