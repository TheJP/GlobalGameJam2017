using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuData : MonoBehaviour
    {
        public bool IsReadyToPlay = false;
        public List<Golem> Golems;

        public void Reset()
        {
            IsReadyToPlay = false;
            //Debug.Log("CLEAR: " + Golems.Count);
            this.Golems.Clear();
        }

        void Start()
        {
            this.Golems = new List<Golem>();
        }

        void Update()
        {
            if (IsReadyToPlay)
            {
                var allgolems = "";
                foreach (var golem in GetGolems())
                {
                    allgolems += golem.Color + " : " + golem.AttackType + " | ";
                }
                //Debug.Log(allgolems);
            }
        }

        public IEnumerable<Golem> GetGolems()
        {
            return this.Golems.GroupBy(x => x.Color).Select(y => y.First());
        } 
    }

    public struct Golem
    {
        public GolemAttackType AttackType;
        public GolemColor Color;

    }

    public enum GolemColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    public enum GolemAttackType { Pilar, Slam, Beam }
}

public static class GolemColorExtensions
{
    public static string GetPlayerName(this Assets.Scripts.Menu.GolemColor color)
    {
        switch (color)
        {
            case Assets.Scripts.Menu.GolemColor.Red: return "A";
            case Assets.Scripts.Menu.GolemColor.Blue: return "B";
            case Assets.Scripts.Menu.GolemColor.Green: return "C";
            case Assets.Scripts.Menu.GolemColor.Yellow: return "D";
            default: throw new System.ArgumentException("Unkown color");
        }
    }
}
