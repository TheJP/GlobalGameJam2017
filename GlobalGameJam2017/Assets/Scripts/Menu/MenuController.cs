using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Menu.Utilities;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {

        public GameObject BtnMultiplayer;
        public GameObject BtnSingleplayer;


        public MenuScreen ScreenEnter;
        public MenuScreen ScreenMultiplayer;
        public MenuScreen ScreenSingleplayer;
        public MenuScreen ScreenGameConsole;
        public MenuScreen ScreenOptions;
        public MenuScreen ScreenCredits;

        private List<MenuScreen> _allScreens;
        private int deactivatedScreens  = 0;


        public bool IsMultiplayer = false;
        public bool IsSinglelayer = false;

        private GameObject ScreenSlotRight;
        private GameObject ScreenSlotLeft;
        private GameObject ScreenSlotTop;
        private GameObject ScreenSlotBottom;


        public void GotoSingleplayer()
        {
            IsSinglelayer = true;

            ScreenEnter.SlideOut(SlideDirection.left);
            ScreenSingleplayer.SlideIn();
         
        }

        public void GotoMultiplayer()
        {
            IsMultiplayer = true;

            ScreenEnter.SlideOut(SlideDirection.left);
            ScreenMultiplayer.SlideIn();
        }

        public void GotoEnterScreen()
        {
            GameObject.Find("MenuCanvas").GetComponent<MenuData>().Reset();
            IsMultiplayer = false;
            IsSinglelayer = false;

            ScreenEnter.SlideIn();
            foreach (var player in ScreenMultiplayer.GetComponentsInChildren<MenuPlayer>())
            {
                player.Cancel();
            }
            foreach (var player in ScreenSingleplayer.GetComponentsInChildren<MenuPlayer>())
            {
                player.Cancel();
            }
            ScreenMultiplayer.SlideOut(SlideDirection.left);
            ScreenSingleplayer.SlideOut(SlideDirection.left);
            ScreenCredits.SlideOut(SlideDirection.top);
            
        }

        public void GotoCreditsScreen()
        {
            IsMultiplayer = false;
            IsSinglelayer = false;


            ScreenEnter.SlideOut(SlideDirection.top);
            ScreenCredits.SlideIn();
            
            //ScreenMultiplayer.SlideOut(SlideDirection.left);
            //ScreenSingleplayer.SlideOut(SlideDirection.left);
        }

        void Start()
        {
            _allScreens = new List<MenuScreen>();
            _allScreens.Add(this.ScreenEnter);
            _allScreens.Add(this.ScreenMultiplayer);
            _allScreens.Add(this.ScreenSingleplayer);
            _allScreens.Add(this.ScreenGameConsole);
            _allScreens.Add(this.ScreenOptions);
            _allScreens.Add(this.ScreenCredits);
        }

        void Update()
        {
            deactivatedScreens = 0;

            foreach (var menuScreen in this._allScreens)
            {
                if (menuScreen.CurrentState == ScreenState.inactive)
                {
                    deactivatedScreens = deactivatedScreens + 1;
                }
            }

            if (_allScreens.Count <= deactivatedScreens && ScreenEnter != null)
            {
                ScreenEnter.SlideIn();
            }

            //Debug.Log(_allScreens.Count + "<-- All Screens");
            //Debug.Log(deactivatedScreens + "<-- Deactivated SCreens");
        }


    }
}
