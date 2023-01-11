using System.Collections;
using UnityEngine;

namespace Assets.Script.Cards
{
    public class AttackCard : Card
    {
        private int shortRange;

        private int farRange;

        private int auraDamage;

        private int lifeDamage;

        private int attackLevel;

        public int ShortRange { get => shortRange; set => shortRange = value; }
        public int FarRange { get => farRange; set => farRange = value; }
        public int AuraDamage { get => auraDamage; set => auraDamage = value; }
        public int LifeDamage { get => lifeDamage; set => lifeDamage = value; }
        public int AttackLevel { get => attackLevel; set => attackLevel = value; }

        public AttackCard(string a, string b) : base(a, b)
        {

        }

        

        public void PlayCard()
        {
            //if(DoCardEffect())
            //{
                
            //}
        }
    }
}