using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Screens
{
    public class MultiplayerScreen : MonoBehaviour {

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
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Horizontal") - 1) < 0.1f)
                {

                }

                //Right
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Horizontal") + 1) < 0.1f)
                {

                }
            }
        }
    }
}
