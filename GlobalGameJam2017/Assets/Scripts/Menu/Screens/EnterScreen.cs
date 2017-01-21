using System;
using System.Collections.Generic;
using Assets.Scripts.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class EnterScreen : MonoBehaviour {

        // Use this for initialization
        private string[] _players = {"A", "B", "C", "D"};

        public MenuController MenuController;

        public GameObject BannerSingleplayer;
        public GameObject BannerMultiplayer;

        public bool _isInMultiplayerMode = false;

        void Start () {
		
        }
	
        // Update is called once per frame
        void Update ()
        {
            foreach (string playerName in _players)
            {
                if (Input.GetButtonDown(playerName + "_a"))
                {
                    Debug.Log(_isInMultiplayerMode);
                    if (_isInMultiplayerMode)
                    {
                        MenuController.GotoSingleplayer();
                    }
                    else
                    {
                        MenuController.GotoMultiplayer();
                    }
                }

                //DOWN
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Vertical") - 1) < 0.1f)
                {
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    
                    _isInMultiplayerMode = false;
                }

                //UP
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Vertical") + 1) < 0.1f)
                {
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    _isInMultiplayerMode = true;
                }

            }
        }

        public void ToggleSingleMultiplayer(bool isMulti)
        {

            _isInMultiplayerMode = isMulti;
                if (isMulti)
                {
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                Debug.Log("Hier");
                _isInMultiplayerMode = true;
                }
                else{
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    _isInMultiplayerMode = false;
                }

            
        }
    }
}
