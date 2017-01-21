using System.Collections.Generic;
using Assets.Scripts.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Utilities
{
    public class MenuPlayer : MonoBehaviour
    {
        public GolemAttackType GolemAttackType;
        public GameObject btnA;
        public GameObject btnB;
        public GameObject btnLeft;
        public GameObject btnRight;


        public GameObject MenuController;

        public List<Sprite> Attacks;
        public List<Sprite> Golems;

        public GameObject ReadyLabel;

        public bool IsReady;

        private int _currentAttackIndex = 0;
        private bool _swapList = false;

        private bool hasCanceldTwice = false;
        private Golem _golem;

        void Start()
        {
            this.gameObject.transform.FindChild("SelectedAttack").GetComponent<Image>().sprite = Attacks[0];
            switch (this.transform.parent.name)
            {
                case "PlayerASlot":
                    this.gameObject.transform.FindChild("Golem").GetComponent<Image>().sprite = Golems.Find(g => g.name == "GolemA");
                    break;
                case "PlayerBSlot":
                    this.gameObject.transform.FindChild("Golem").GetComponent<Image>().sprite = Golems.Find(g => g.name == "GolemB");
                    break;
                case "PlayerCSlot":
                    this.gameObject.transform.FindChild("Golem").GetComponent<Image>().sprite = Golems.Find(g => g.name == "GolemC");
                    break;
                case "PlayerDSlot":
                    this.gameObject.transform.FindChild("Golem").GetComponent<Image>().sprite = Golems.Find(g => g.name == "GolemD");
                    break;
            }
            
        }

        public void PreConfirm()
        {

            this.GetComponent<CanvasGroup>().alpha = 1.0f;

            btnA.GetComponent<Button>().onClick.RemoveListener(this.PreConfirm);
            btnA.GetComponent<Button>().onClick.AddListener(this.Confirm);

            switch (transform.parent.name)
            {
                case "PlayerASlot":
                    this.GetComponent<Image>().color = Color.red;
                    break;
                case "PlayerBSlot":
                    this.GetComponent<Image>().color = Color.blue;
                    break;
                case "PlayerCSlot":
                    this.GetComponent<Image>().color = Color.green;
                    break;
                case "PlayerDSlot":
                    this.GetComponent<Image>().color = Color.yellow;
                    break;
            }
        }

        public void Confirm()
        {
            hasCanceldTwice = false;
            IsReady = true;
            ReadyLabel.GetComponent<CanvasGroup>().alpha = 1f;

            _golem = new Golem
            {

                AttackType = this.GolemAttackType
            };

            switch (transform.parent.name)
            {
                case "PlayerASlot":
                    _golem.Color = GolemColor.Red;
                    break;
                case "PlayerBSlot":
                    _golem.Color = GolemColor.Blue;
                    break;
                case "PlayerCSlot":
                    _golem.Color = GolemColor.Green;
                    break;
                case "PlayerDSlot":
                    _golem.Color = GolemColor.Yellow;
                    break;
            }

            GameObject.Find("MenuCanvas").GetComponent<MenuData>().Golems.Add(_golem);
        }

        public void Cancel()
        {
            GameObject.Find("MenuCanvas").GetComponent<MenuData>().Golems.RemoveAll((x) => x.Color == this._golem.Color);
            if (hasCanceldTwice)
            {
                GameObject.Find("MenuCanvas").GetComponent<MenuData>().Reset();
                this.transform.parent.transform.parent.GetComponent<MenuScreen>().SlideOut(SlideDirection.left);
                MenuController.transform.FindChild("screenEnter").GetComponentInChildren<MenuScreen>().SlideIn();

                hasCanceldTwice = false;
            }
            btnA.GetComponent<Button>().onClick.RemoveAllListeners();
            btnA.GetComponent<Button>().onClick.AddListener(this.PreConfirm);
            this.GetComponent<Image>().color = Color.white;
            this.GetComponent<CanvasGroup>().alpha = .1f;
            ReadyLabel.GetComponent<CanvasGroup>().alpha = 0f;
            if (!hasCanceldTwice)
            {
                hasCanceldTwice = true;
            }
        }

        public void ChangeGolemType(string direction)
        {
            
            switch (direction)
            {
                case "Left":
                    _currentAttackIndex = Mathf.Clamp(--_currentAttackIndex, 0, 2);
                    if (_swapList)
                    {
                        _currentAttackIndex = Attacks.Count - 1;
                        _swapList = false;
                    }

                    this.gameObject.transform.FindChild("SelectedAttack").GetComponent<Image>().sprite = Attacks[_currentAttackIndex];

                    if (_currentAttackIndex == 0)
                    {
                        _swapList = true;
                    }
                    break;
                case "Right":
                    _currentAttackIndex = Mathf.Clamp(++_currentAttackIndex, 0, 2);
                    if (_swapList)
                    {
                        _currentAttackIndex = 0;
                        _swapList = false;
                    }

                    this.gameObject.transform.FindChild("SelectedAttack").GetComponent<Image>().sprite = Attacks[_currentAttackIndex];

                    if (_currentAttackIndex == Attacks.Count - 1)
                    {
                        _swapList = true;
                    }
                    
                    break;
            }

            switch (this.gameObject.transform.FindChild("SelectedAttack").GetComponent<Image>().sprite.name)
            {
                case "Attack1":
                    this.GolemAttackType = GolemAttackType.Pilar;
                    break;
                case "Attack2":
                    this.GolemAttackType = GolemAttackType.Slam;
                    break;
                case "Attack3":
                    this.GolemAttackType = GolemAttackType.Beam;
                    break;
            }
            

        }

    }

    
}
