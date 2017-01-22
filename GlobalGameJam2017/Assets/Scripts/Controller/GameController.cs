using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
        var letter = 'A';
        foreach(var spawn in playerSpawnLocations)
        {
            //Spawn player and add shockwave spell
            var player = Instantiate(playerPrefab, spawn.position, Quaternion.identity, playersGroup);
            Instantiate(shockwaveSpellPrefab, player.transform.position, Quaternion.identity, player.transform);

            //Assign the player a unique name (so he will be controlled by different keys)
            player.GetComponent<Player>().playerName = letter.ToString();
            
            ++letter;
        }
        SpawnWave(5);
    }

    void FixedUpdate()
    {
        if (enemiesToSpawn > 0)
        {
            foreach (var spawn in enemySpawnLocations
                .Where(s => FindObjectsOfType<Entity>().All(e => (s.transform.position - e.transform.position).sqrMagnitude > 1f)))
            {
                --enemiesToSpawn;
                var enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity, enemiesGroup);
                if(enemiesToSpawn <= 0) { break; }
            }
        }
    }

    private void SpawnWave(int size)
    {
        enemiesToSpawn = size;
    }
}
