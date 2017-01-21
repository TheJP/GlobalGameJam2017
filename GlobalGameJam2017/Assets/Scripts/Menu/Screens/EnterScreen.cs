using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Screens
{
    public class EnterScreen : MonoBehaviour {

        // Use this for initialization
        private string[] _players = {"A", "B", "C", "D"};

        public MenuController MenuController;

        public GameObject BannerSingleplayer;
        public GameObject BannerMultiplayer;
        public GameObject BannerCredits;

        public bool _isInMultiplayerMode = false;
        //private int _currentIndex = 0;

        void Start () {
		
        }
	
        // Update is called once per frame
        void Update ()
        {
            foreach (string playerName in _players)
            {
                if (Input.GetButtonDown(playerName + "_a"))
                {
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
                    BannerCredits.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    
                    _isInMultiplayerMode = false;
                }

                //UP
                if (Math.Abs(Input.GetAxisRaw(playerName + "_Vertical") + 1) < 0.1f)
                {
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerCredits.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    _isInMultiplayerMode = true;
                }

                if (Input.GetButtonDown("Start"))
                {
                    BannerSingleplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerMultiplayer.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
                    BannerCredits.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    MenuController.GotoCreditsScreen();
                    //MenuController.GotoCreditsScreen();
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
