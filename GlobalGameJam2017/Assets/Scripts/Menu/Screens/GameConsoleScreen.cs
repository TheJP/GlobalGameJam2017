using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameConsoleScreen : MonoBehaviour
{


    public GameObject PlayerA;
    
    public GameObject PlayerB;
    public GameObject PlayerC;
    public GameObject PlayerD;

    private GameController _gameController;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

	// Update is called once per frame
	void Update () {
        
	    if (PlayerA.activeInHierarchy)
	    {
	        var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "A");
	        if (player != null)
	        {
	            var health = player.Health;
	            var maxHealth = player.maxHealth;

	            PlayerA.transform.FindChild("HealthALayer03").GetComponent<Text>().text = (maxHealth/100*health).ToString(CultureInfo.InvariantCulture) + "%";
	            //var posX = PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>().rect.x;
             //   var posY = PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>().rect.y;
             //   var width = PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>().rect.width;
             //   var height = PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>().rect.height;

             //   PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>().rect.Set((posX - (maxHealth / 100 * health)), posY, width,height);

	        }
	    }
	    if (PlayerB.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "B");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthALayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
            }
        }
        if (PlayerC.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "C");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthALayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
            }
        }
        if (PlayerD.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "D");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthALayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
            }
        }
    }
}
