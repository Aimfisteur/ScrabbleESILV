using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScrabbleESILV
{
    class Joueur
    {
        private string nom;
        private int score=0;
        private List<string> mots=new List<string>();
        private List<Jeton> main = new List<Jeton>();
        
        public Joueur(string nom)
        {
             
            if (nom.Length >= 4 &&  nom.Substring(nom.Length - 4) == ".txt")
            {
                StreamReader sr = new StreamReader(nom);
                string ligne;
                int c = 0;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] s = ligne.Split(";");
                    if (c == 0)
                    {
                        this.nom = s[0];
                        this.score = Convert.ToInt32(s[1]);
                    }
                    if (c == 1)
                    {
                        for(int i = 0; i < s.Length; i++)
                        {
                            mots.Add(s[i]);
                        }
                    }
                    if (c == 2)
                    {
                        for(int i = 0; i < s.Length; i++)
                        {
                            StreamReader jet = new StreamReader("Jetons.txt");
                            string j;
                            while ((j = jet.ReadLine()) != null)
                            {
                                string[] car = j.Split(";");
                                if (car[0] == s[i])
                                {
                                    Jeton jeto = new Jeton(Convert.ToChar(car[0]), Convert.ToInt32(car[1]), Convert.ToInt32(car[2]));
                                    main.Add(jeto);
                                    break;
                                }
                            }
                        }
                    }
                    c = c + 1;
                }
            }
            else
            {


                this.nom = nom;
            }

        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }


        }
        public string Nom
        {
            get { return this.nom; }
            


        }

        public List<Jeton> Main 
        {
            get { return this.main; }
            

        }
        public List<string> Mots
        {
            get { return this.mots; }
            


        }
        public void Add_Mot(string mot)
        {
            mots.Add(mot);
        }
        public string toString()
        {
            
            string res = "";
            res = res + "Joueur : " + this.nom + "\n" + "Score : "  + score + "\n" + "Les mots trouvés par le joueurs sont: ";
            for (int i=0;i < mots.Count; i++)
            {
                res = res  + mots[i] + " ";
            }
            res = res + "\net sa main courante est:";
            for (int i = 0; i < main.Count; i++)
            {
                res = res + " " + Convert.ToString(main[i].Lettre);
                
            }
            return res;
        }
        public void Add_Score(int val)
        {
            this.score += val;
        }
        public void Add_Main_Courante(Jeton monjeton)
        {
            if (main.Count < 7)
            {
                main.Add(monjeton);
            }
            else
            {
                Console.WriteLine("il y a déjà 7 jetons dans la main");
            }
        }
        public void Remove_Main_Courante(Jeton monjeton)// 'c'
        {
            bool savoir = false;
            int k = -1;
            for(int i = 0; i < main.Count; i++)
            {
                
                if (Jeton.Egale(main[i], monjeton))
                {
                    savoir = true;
                    k = i;
                    break;
                    
                }
            }
            if (savoir)
            {
                main.RemoveAt(k);
                

            }
            else
            {
                Console.WriteLine("il n'y a pas ce jeton dans la main");
            }
        }



    }
}
