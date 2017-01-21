using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MenuPlayer : MonoBehaviour
    {

        public GameObject btnA;
        public GameObject btnB;

        public GameObject ReadyLabel;

        public bool IsReady;


        public void PreConfirm()
        {
            this.GetComponent<CanvasGroup>().alpha = 1.0f;

            btnA.GetComponent<Button>().onClick.RemoveListener(this.PreConfirm);
            btnA.GetComponent<Button>().onClick.AddListener(this.Confirm);

            switch (transform.parent.name)
            {
                case "Player1Slot":
                    this.GetComponent<Image>().color = Color.red;
                    break;
                case "Player2Slot":
                    this.GetComponent<Image>().color = Color.blue;
                    break;
                case "Player3Slot":
                    this.GetComponent<Image>().color = Color.yellow;
                    break;
                case "Player4Slot":
                    this.GetComponent<Image>().color = Color.green;
                    break;
            }
        }

        public void Confirm()
        {
            IsReady = true;
            ReadyLabel.GetComponent<CanvasGroup>().alpha = 1f;
            Debug.Log(IsReady);
        }

        public void Cancel()
        {
            btnA.GetComponent<Button>().onClick.RemoveAllListeners();
            btnA.GetComponent<Button>().onClick.AddListener(this.PreConfirm);
            this.GetComponent<Image>().color = Color.white;
            this.GetComponent<CanvasGroup>().alpha = .1f;
            ReadyLabel.GetComponent<CanvasGroup>().alpha = 0f;
        }

    }
}
