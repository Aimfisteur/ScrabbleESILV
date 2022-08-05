using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleESILV
{
    
    class Jeton
    {
        private char lettre;
        private int valeur;
        private int restant;
        public Jeton(char lettre, int valeur, int restant)
        {
            this.lettre = char.ToUpper(lettre);
            this.valeur = valeur;
            this.restant = restant;

        }
        public char Lettre
        {
            get { return this.lettre; }
            set { this.lettre = value; }
            
        }
        public int Valeur
        {
            get { return this.valeur; }
            set { this.valeur = value; }

        }
        public int Restant
        {
            get { return this.restant; }
            set { this.restant = value; }

        }
        public string toString()
        {
            return "Lettre : " + lettre + " valeur : " + valeur + " restant : " + restant;
        }
        static public bool Egale(Jeton a, Jeton b)
        {
            return ( a.Lettre == b.Lettre);
        }
    }
}
