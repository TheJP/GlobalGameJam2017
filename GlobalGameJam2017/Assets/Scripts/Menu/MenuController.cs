using System.Collections;
using System.Collections.Generic;
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
            IsMultiplayer = false;
            IsSinglelayer = false;

            ScreenEnter.SlideIn();
            ScreenMultiplayer.SlideOut(SlideDirection.left);
            ScreenSingleplayer.SlideOut(SlideDirection.left);
        }




    }
}
