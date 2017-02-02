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

    private float _maxHealthWidth;
    private float _maxHealthPos;
    private float _minHealthPos;


    void Start()
    {
        _gameController = FindObjectOfType<GameController>();

        _maxHealthWidth = 60.7f;
        _maxHealthPos = -390.8f;
        _minHealthPos = -420.0f;

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
	            var rt = PlayerA.transform.FindChild("HealthALayer02").GetComponent<RectTransform>();
                
                rt.sizeDelta = new Vector2((_maxHealthWidth/100) * (maxHealth/100*health), rt.sizeDelta.y);
	            //rt.localPosition = new Vector2(Mathf.Clamp((maxHealth / health / rt.localPosition.x) - rt.localPosition.x,  _maxHealthPos, _minHealthPos), rt.localPosition.y);
                //Debug.Log(_minHealthPos + " " + (maxHealth / health) / rt.localPosition.x + " " + _maxHealthPos);

            }
        }
	    if (PlayerB.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "B");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthBLayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
                var rt = PlayerA.transform.FindChild("HealthBLayer02").GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2((_maxHealthWidth / 100) * (maxHealth / 100 * health), rt.sizeDelta.y);
            }
        }
        if (PlayerC.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "C");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthCLayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
                var rt = PlayerA.transform.FindChild("HealthCLayer02").GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2((_maxHealthWidth / 100) * (maxHealth / 100 * health), rt.sizeDelta.y);
            }
        }
        if (PlayerD.activeInHierarchy)
        {
            var player = _gameController.playersGroup.GetComponentsInChildren<Player>().FirstOrDefault(p => p.playerName == "D");
            if (player != null)
            {
                var health = player.Health;
                var maxHealth = player.maxHealth;

                PlayerA.transform.FindChild("HealthDLayer03").GetComponent<Text>().text = (maxHealth / 100 * health).ToString(CultureInfo.InvariantCulture) + "%";
                var rt = PlayerA.transform.FindChild("HealthDLayer02").GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2((_maxHealthWidth / 100) * (maxHealth / 100 * health), rt.sizeDelta.y);
            }
        }
    }
}
