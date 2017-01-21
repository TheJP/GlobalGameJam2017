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


        public void GotoSingleplayer()
        {
            Debug.Log("Singleplayer");
            IsSinglelayer = true;

            ScreenEnter.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
            ScreenSingleplayer.GetComponent<MenuScreen>().SlideIn();
         
        }

        public void GotoMultiplayer()
        {
            Debug.Log("Muliplayer");
            IsMultiplayer = true;

            ScreenEnter.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
            ScreenMultiplayer.GetComponent<MenuScreen>().SlideIn();
        }

        public void GotoEnterScreen()
        {
            Debug.Log("EnterScreen");
            IsMultiplayer = false;
            IsSinglelayer = false;

            ScreenEnter.GetComponent<MenuScreen>().SlideIn();
            ScreenMultiplayer.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
            ScreenSingleplayer.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
        }




    }
}
