using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;//to check if in the array

namespace ScrabbleESILV
{
    class Sac_Jetons
    {
        private List<Jeton> sac = new List<Jeton>();
        Dictionary<char, int> calcul = new Dictionary<char, int>();
        Dictionary<char, Jeton> ConversionJeton = new Dictionary<char, Jeton>();

        public Sac_Jetons(string path)
        {
            string ligne;
            StreamReader sr = new StreamReader(path);
            
            while ((ligne=sr.ReadLine()) != null)
            {
                string[] car = ligne.Split(";");
                Jeton actuelle = new Jeton(Convert.ToChar(car[0]), Convert.ToInt32(car[1]), Convert.ToInt32(car[2]));
                sac.Add(actuelle);
                calcul.Add(Convert.ToChar(car[0]), Convert.ToInt32(car[1]));
                ConversionJeton.Add(Convert.ToChar(car[0]), actuelle);
            }
         



        }
       
        public int Valeur_calcul(char c)
        {
            c = char.ToUpper(c);
            return calcul[c];
        }
        public Jeton conversion_jeton(char c)
        {
            c = char.ToUpper(c);
            return ConversionJeton[c];
        }
        public int Sac_restant()
        {
            int somme = 0;
            for (int i = 0; i < sac.Count; i++)
            {
                somme = somme + sac[i].Restant;
            }
            return somme;
        }
        public Jeton Retire_Jeton(Random r)
        {
            Jeton pioche = new Jeton('w',-1,-1);
            int n = this.Sac_restant();
            int p = r.Next(1, n);
            int somme = 0;
            int sommeancien = 0;
            
            for (int i = 0; i < sac.Count; i++)
            {
                somme = somme + sac[i].Restant;
                if (p<= somme && p>sommeancien)
                {
                    
                    pioche = sac[i];
                    sac[i].Restant = sac[i].Restant - 1;
                   
                    break;
                }
                sommeancien = somme;

            }
           
            return pioche;

            

        }
        
        public string toString()
        {
            string res = "Le sac est composée de: ";
            List<char> Jetons_vue = new List<char>();
            for (int i = 0; i < sac.Count; i++)
            {
                if (!Jetons_vue.Contains(sac[i].Lettre))
                {
                    res = res + sac[i].toString()+"\n";
                    Jetons_vue.Add(sac[i].Lettre);

                }

            }
            return res;

        }




        }


    }

