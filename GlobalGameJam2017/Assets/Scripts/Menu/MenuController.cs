using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {

        public GameObject BtnMultiplayer;
        public GameObject BtnSingleplayer;


        public GameObject ScreenEnter;
        public GameObject ScreenMultiplayer;
        public GameObject ScreenSingleplayer;
        public GameObject ScreenOptions;


        public bool IsMultiplayer = false;
        public bool IsSinglelayer = false;

        private GameObject ScreenSlotRight;
        private GameObject ScreenSlotLeft;
        private GameObject ScreenSlotTop;
        private GameObject ScreenSlotBottom;


        public void Singleplayer()
        {
            Debug.Log("Singleplayer");
            IsSinglelayer = true;

            ScreenEnter.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
            ScreenSingleplayer.GetComponent<MenuScreen>().SlideIn();
         
        }

        public void Multiplayer()
        {
            Debug.Log("Muliplayer");
            IsMultiplayer = true;

            ScreenEnter.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
            ScreenMultiplayer.GetComponent<MenuScreen>().SlideIn();
        }


       

    }
}
