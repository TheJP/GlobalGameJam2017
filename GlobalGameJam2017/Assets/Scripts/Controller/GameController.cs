using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameStart
{
    public CameraController cameraController;
    public Transform[] playerSpawnLocations;
    public Transform[] enemySpawnLocations;

    public GameObject playerPrefab;
    public GameObject shockwaveSpellPrefab;
    public GameObject pillarSpellPrefab;
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
    /// <summary>Variable that prevents the spawning of too many enemies.</summary>
    private bool spawning = true;
    private bool gameRunning = false;

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

        //Spawn next wave if there are no more enemies left
        if(!spawning && enemiesGroup.childCount <= 0)
        {
            spawning = true;
            Invoke("InitWave", waveDelay);
        }

        //Handle gameover 
        if(gameRunning && playersGroup.childCount <= 0)
        {
            cameraController.SwitchToMenu();
            gameRunning = false;
            var menuController = FindObjectOfType<MenuController>();
            menuController.ScreenGameConsole.SlideOut(SlideDirection.top);

            //Workaround until menu works correctly
            menuController.ScreenEnter = null;
            Invoke("ResetScene", 4.0f);
            //menuController.ScreenEnter.SlideIn();
        }
    }

    private void ResetScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
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

        //Remove old enemies
        foreach(var enemy in enemiesGroup.Cast<GameObject>()) { Destroy(enemy); }

        //Start the game
        var possibleSpawns = new Stack<Transform>(playerSpawnLocations.ToList());
        waveCount = 1;
        enemiesToSpawn = 0;
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
            Instantiate(golem.AttackType == GolemAttackType.Pilar ? pillarSpellPrefab : shockwaveSpellPrefab, player.transform.position, Quaternion.identity, player.transform);
            
            //Assign the player a unique name (so he will be controlled by different keys)
            player.GetComponent<Player>().playerName = golem.Color.GetPlayerName();

            //Enable UI Healthbar here
            var screenGameConsole = GameObject.Find("MenuCanvas").GetComponent<MenuController>().ScreenGameConsole.gameObject;
            screenGameConsole.transform.FindChild("Player" + player.GetComponent<Player>().playerName).gameObject.SetActive(true);

        }
        spawning = true;
        Invoke("InitWave", waveDelay);
        gameRunning = true;
    }
}
