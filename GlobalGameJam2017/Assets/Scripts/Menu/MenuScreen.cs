using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MenuScreen : SuperScreenClass
    {
        void Update()
        {
            if (this.transform.name == "screenCredits")
            {
                if (Input.GetButtonDown("Start"))
                {

                    GameObject.Find("MenuCanvas").GetComponent<MenuController>().GotoEnterScreen();
                }
            }
        }
    }


}

