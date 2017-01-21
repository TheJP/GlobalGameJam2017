using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Menu.Utilities;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Screens
{
    public class MultiplayerScreen : MonoBehaviour
    {

        private string[] _players = { "A", "B", "C", "D" };

        public List<MenuPlayer> Players;

        void Start()
        {

        }


        void Update()
        {
            foreach (string playerName in _players)
            {
                if (Input.GetButtonDown(playerName + "_a"))
                {
                    var btnA = Players.Where(p => p.transform.parent.name == "Player" + playerName + "Slot").ToList()[0].btnA
                        .GetComponent<Button>();
                    btnA.onClick.Invoke();

                }
                if (Input.GetButtonDown(playerName + "_b"))
                {
                    var btnA = Players.Where(p => p.transform.parent.name == "Player" + playerName + "Slot").ToList()[0].btnB
                    .GetComponent<Button>();
                    btnA.onClick.Invoke();
                }

                //DOWN
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Vertical") - 1) < 0.1f)
                {

                }

                //UP
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Vertical") + 1) < 0.1f)
                {

                }

                //Left
                if (Input.GetButtonDown(playerName + "_Horizontal") && Input.GetAxisRaw(playerName + "_Horizontal") < 0)
                {
                    var btnLeft = Players.Where(p => p.transform.parent.name == "Player" + playerName + "Slot").ToList()[0].btnLeft
                    .GetComponent<Button>();
                    btnLeft.onClick.Invoke();
                }

                //Right
                if (Input.GetButtonDown(playerName + "_Horizontal") && Input.GetAxisRaw(playerName + "_Horizontal") > 0)
                {
                    var btnRight = Players.Where(p => p.transform.parent.name == "Player" + playerName + "Slot").ToList()[0].btnRight
                    .GetComponent<Button>();
                    btnRight.onClick.Invoke();
                }
            }

            // Magic
            var allPlayers =
                this.transform.GetComponentsInChildren<CanvasGroup>()
                    .Where(c => c.transform.name == "Menu_PlayerSelection")
                    .ToList().Where(c => c.alpha == 1).ToList()
                    .Count;
            var allReadyPlayers =
                transform.GetComponentsInChildren<CanvasGroup>()
                    .Where(c => c.transform.name == "ReadyBanner")
                    .ToList().Where(c => c.alpha == 1).ToList()
                    .Count;

            //Debug.Log("allPlayers:" + allPlayers);
            //Debug.Log("allReadyPlayers:" + allReadyPlayers);
            if (allPlayers == allReadyPlayers && allPlayers != 0)
            {
                GameObject.Find("MenuCanvas").GetComponent<MenuData>().IsReadyToPlay = true;
            }
            else
            {
                GameObject.Find("MenuCanvas").GetComponent<MenuData>().IsReadyToPlay = false;

            }


        }
    }
}
