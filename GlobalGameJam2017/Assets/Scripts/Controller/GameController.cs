using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform[] playerSpawnLocations;
    public GameObject playerPrefab;
    public GameObject shockwaveSpellPrefab;

    public Transform playersGroup;

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
    }

    void Update()
    {

    }
}
