using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ScrabbleESILV
{
    class Plateau
    {
        static string[,] plateau = new string[15, 15];
        Dictionnaire dic;
        static int nombreDeCoup = 0;
        static string[,] multiplicateur = new string[15,15] ;
        static string[,] background = new string[15, 15];
        static Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
        public Plateau(string PathPlateau,Dictionnaire dic)
        {
            
                StreamReader sr = new StreamReader(PathPlateau);
            
                string ligne;
                int l = 0;
                while ((ligne = sr.ReadLine()) != null && l<15)
                {
                
                    string[] car = ligne.Split(";");
                    for(int i = 0; i < 15; i++)
                    {
                        plateau[l, i] = car[i];
                        

                    }
                    l = l + 1;
                }
                this.dic = dic;
                
                string li;
                StreamReader s = new StreamReader("board.txt");
                l = 0;
                while ((li = s.ReadLine()) != null&&l<15)
                {
                    string[] c = li.Split(";");
                    for (int i = 0; i < 15; i++)
                    {
                        multiplicateur[l, i] = c[i];
                        background[l, i] = c[i];

                    }
                    l = l + 1;
                }
                
            
        }
        public string[,] PPlateau
        {
            get { return plateau; }
            


        }
        public int NombreDeCoup
        {
            get { return nombreDeCoup; }
            set { nombreDeCoup = value; }

        }
        public void toString()
        {
            
            for (int i = 0; i < 15; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                char lettre = (char)(65 + i);
                Console.Write(lettre);
                
                Console.ForegroundColor = ConsoleColor.Black;
                for (int j = 0; j < 15; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|");
                    
                    Console.ForegroundColor = ConsoleColor.Black;

                    if (background[i, j] == "T")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(plateau[i,j]);

                    }
                    if (background[i, j] == "_")
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write( plateau[i, j]);

                    }
                    if (background[i, j] == "D")
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.Write( plateau[i, j]);

                    }
                    if (background[i, j] == "d")
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write( plateau[i, j]);

                    }
                    if (background[i, j] == "t")
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write( plateau[i, j]);

                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    
                    
                    

                    
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(" |A B C D E F G H I J K L M N O");
        }
        static public string ChercherMotHorizontal(int l, int c,string s)
        {
            string res = s;
            int debut = c;
            
            
                while (c > 0 && (plateau[l, c - 1] != "_"))//recherche partie gauche du mot formé
                {


                    res = plateau[l, c - 1] + res;
                    c = c - 1;


                }
                c = debut;


                while (c < 14 && (plateau[l, c + 1] != "_"))
                {
                    res = res + plateau[l, c + 1];
                    c = c + 1;

                }
            
            
            return res;
        }
        static public string ChercherMotVertical(int l, int c,string s)
        {
            string res = s;
            int debut = l;
            
            while (l > 0 && (plateau[l-1, c ] != "_"))//recherche partie droite du mot formé
            {


                res = plateau[l-1, c] + res;
                l = l - 1;


            }
            l = debut;
            
            while (l < 14 && (plateau[l+1, c ] != "_"))
            {
                
                res = res + plateau[l+1, c ];
                l = l + 1;

            }
            return res;
        }

        public bool MotFormeBas(int l, int c,string s)
         {
            
            s = s.ToUpper();
            bool res = true;
             if (c > 0)
             {

                

                if (plateau[l, c - 1] != "_")
                {
                    string motcourant = ChercherMotHorizontal(l, c, s);

                    if (!dic.RechDichoRecursif(motcourant))
                    {
                        res = false;

                    }
                }

             }
             if (c < 14)
            {
                if (plateau[l, c + 1] != "_")
                {
                    string motcourant = ChercherMotHorizontal(l, c,s);
                    if (!dic.RechDichoRecursif(motcourant))
                    {
                        res = false;
                    }
                }
            }
            return res;
         }
        public bool MotFormeDroit(int l, int c, string s)
        {
            s = s.ToUpper();
            bool res = true;
            if (l > 0)
            {

                if (plateau[l-1, c ] != "_")
                {
                    string motcourant = ChercherMotVertical(l, c, s);
                    
                    if (!dic.RechDichoRecursif(motcourant))
                    {
                        res = false;

                    }

                }


            }
            if (c < 14)
            {
                if (plateau[l+1, c ] != "_")
                {
                    string motcourant = ChercherMotVertical(l, c, s);
                    if (!dic.RechDichoRecursif(motcourant))
                    {
                        res = false;
                    }
                }
            }
            return res;
        }
        public bool Droite(int l, int c,string word,Joueur joueur, int indice = -1 )//indice
        {
            word=word.ToUpper();
            
            List<string> main = new List<string>();
            bool res = true;
            for(int i = 0; i < joueur.Main.Count; i++)
            {
                main.Add(Convert.ToString(joueur.Main[i].Lettre));
                
            }
            int n = word.Length;
            for(int i = 0; i < n; i++)
            {
                if (plateau[l, c + i] == "_")
                {
                    if (i != indice)
                    {
                        if (main.Contains(Convert.ToString(word[i])))
                        {
                            main.Remove(Convert.ToString(word[i]));

                        }
                        else
                        {
                            Console.WriteLine("vous ne possedez pas les jetons nécéssaire");
                            res = false;

                        }
                    }
                    else
                    {
                        if (main.Contains("*"))
                        {
                            main.Remove("*");

                        }
                        else
                        {
                            Console.WriteLine("vous ne possedez pas *");
                            res = false;

                        }
                    }
                }
                else
                {
                    if(Convert.ToString(word[i]) != plateau[l, c + i])
                    {
                        Console.WriteLine("le mot ne peu pas etre placé");//à modifier peut etre
                        res = false;

                    }
                }
            }
            return res;
        }
        public bool Bas(int l, int c, string word, Joueur joueur,int indice=-1) //indice 
        {
            word = word.ToUpper();

            List<string> main = new List<string>();
            bool res = true;
            for (int i = 0; i < joueur.Main.Count; i++)
            {
                main.Add(Convert.ToString(joueur.Main[i].Lettre));

            }
            int n = word.Length;
            for (int i = 0; i < n; i++)
            {
                if (plateau[l+i, c ] == "_")
                {
                    if (i != indice)
                    {

                        if (main.Contains(Convert.ToString(word[i])))
                        {
                            main.Remove(Convert.ToString(word[i]));

                        }
                        else
                        {
                            Console.WriteLine("vous ne possedez pas les jetons nécéssaire");
                            res = false;
                        }
                    }
                    if (i == indice)
                    {
                        if (main.Contains("*"))
                        {
                            main.Remove("*");

                        }
                        else
                        {
                            Console.WriteLine("vous ne possedez pas *");
                            res = false;
                        }
                    }

                }
                else
                {
                    if (Convert.ToString(word[i]) != plateau[l+i, c ])
                    {
                        Console.WriteLine("le mot ne peut pas etre placé");
                        res = false;

                    }
                }
            }
            return res;
        }
        public bool Check_total(string mot, int ligne, int colonne, char direction)
        {
            bool res = true;
            //copie du tableau, et pose du mot:
            string[,] copie = new string[15,15];
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    copie[i, j] = plateau[i, j];
                }
            }
            try
            {
                if (direction == 'D')
                {
                    for(int i = 0; i < mot.Length; i++)
                    {
                        copie[ligne, colonne + i] = Convert.ToString(mot[i]);
                    }
                    string solutiontotal = copie[ligne, colonne];
                    int debut = colonne;//peut etre va beug
                    int l = ligne;
                    int c = colonne;
                    while(c>0 && copie[l, c - 1] != "_")//prendre la partie gauche du mot
                    {
                        solutiontotal = copie[l, c - 1]+solutiontotal;
                        c = c - 1;
                    }
                    c = debut;
                    while(c<14 && copie[l, c + 1] != "_")
                    {
                        solutiontotal = solutiontotal + copie[l, c + 1];
                        c = c + 1;
                    }
                   
                    


                    if (!dic.RechDichoRecursif(solutiontotal))
                    {
                        res = false;
                    }
                }
                if (direction == 'B')
                {
                    for (int i=0; i < mot.Length; i++)
                    {
                        copie[ligne + i, colonne] = Convert.ToString(mot[i]);
                        
                    }
                    string solutiontotal = copie[ligne, colonne];
                    int debut = ligne;//peut etre va beug
                    int l = ligne;
                    int c = colonne;
                    while (l > 0 && copie[l-1, c ] != "_")//prendre la partie gauche du mot
                    {
                        solutiontotal = copie[l-1, c] + solutiontotal;
                        l = l - 1;
                    }
                    l = debut;
                    while (l < 14 && copie[l+1, c] != "_")
                    {
                        solutiontotal = solutiontotal + copie[l+1, c ];
                        l = l + 1;
                    }

                   
                    if (!dic.RechDichoRecursif(solutiontotal))
                    {
                        res = false;
                    }
                }
            }
            catch
            {
                res = false;
            }

            return res;
        }
        public bool Contact(string mot, int ligne, int colonne, char direction)
        {
            bool res = false;
            if (direction == 'B')
            {
                if (colonne > 0)
                {

                    for (int i = 0; i < mot.Length; i++)//check la partie gauche uniquement
                    {

                        if (plateau[ligne + i, colonne - 1] != "_")
                        {
                            res = true;
                        }
                    }
                }
                if (colonne < 14)
                {
                    for(int i = 0; i < mot.Length; i++)//tcheck la partie droite uniquement
                    {
                        if (plateau[ligne + i, colonne + 1] !="_")
                        {
                            res = true;
                        }
                    }
                }
                if(ligne>0 && plateau[ligne - 1, colonne] != "_")//check juste au dessus
                {
                    res = true;
                }
                if (ligne < 14 && plateau[ligne + mot.Length, colonne] != "_")
                {
                    res = true;
                    //check juste en dessous
                }

            }
            if (direction == 'D')
            {
                if (ligne > 0)
                {
                    for(int i = 0; i < mot.Length; i++)//check tt au dessus
                    {
                        if (plateau[ligne - 1, colonne + i] != "_")
                        {
                            res = true;
                        }
                    }
                }
                if (ligne < 14)
                {
                    for (int i = 0; i < mot.Length; i++)
                    {
                        if (plateau[ligne + 1, colonne + i] != "_")//tcheck tt en dessous
                        {
                            res = true;
                        }
                    }
                }
                //tcheck gauche et droit du mot
                if(colonne>0 && plateau[ligne, colonne - 1] != "_")
                {
                    res = true;
                }
                if(colonne<14 && plateau[ligne, colonne + mot.Length] != "_")
                {
                    res = true;
                }
            }
            return res;
        }
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction)// D comme droite et B comme bas
        {
            
            bool res = true;
            int longueur = mot.Length;
            direction = char.ToUpper(direction);
            if( direction!='D'&& direction != 'B')
            {
                res = false;
                Console.WriteLine("merci de mettre une bonne direction (B pour bas et D pour droite)");
            }
            if (!dic.RechDichoRecursif(mot))
            {
                Console.WriteLine("le mot n'est pas dans le dictionnaire");
                res = false;
            }
            if (colonne<0 || colonne>=15 || ligne<0 || ligne >= 15)
            {
                res = false;
                Console.WriteLine("le mot sort du jeu1");
            }
            if(direction=='D' && colonne + longueur-1 >= 15)
            {
                res = false;
                Console.WriteLine("le mot sort du jeu2");
            }
            if (direction=='B' && ligne + longueur-1 >= 15)
            {
                res = false;
                Console.WriteLine("Le mot sort du jeu3");
            }
            

            if(ligne+longueur<15 && colonne+longueur<15 && colonne>=0 && ligne >= 0)
            {
                if (direction == 'B')
                {
                    for(int i = 0; i < mot.Length; i++)
                    {
                        if (!this.MotFormeBas(ligne+i, colonne ,Convert.ToString(mot[i])))
                        {
                            res = false;
                            Console.WriteLine("un des mots qui se joint n'est plus un mot");
                            break;
                        }
                    }
                    
                    
                    

                }
                if (direction == 'D')
                {
                    for(int i = 0; i < mot.Length; i++)
                    {
                        if (!this.MotFormeDroit(ligne , colonne+i, Convert.ToString(mot[i])))
                        {
                            res = false;
                            Console.WriteLine("un des mots qui se joint n'est plus un mot");
                            break;

                        }

                    }
                    
                    if (!this.Check_total(mot,ligne,colonne,direction))
                    {
                        res = false;
                        
                        Console.WriteLine("Le mot Total n'est pas un mot");
                    }
                    
                }
                if (nombreDeCoup != 0)
                {
                    if (!Contact(mot, ligne, colonne, direction))
                    {
                        res = false;
                        Console.WriteLine("il n'y a pas de contact");
                    }
                }
                else//vérifier que ca passe par le centre
                {
                    if (direction == 'B')
                    {
                        bool milieu = false;
                        for(int i = 0; i < mot.Length; i++)
                        {
                            if(ligne+i==7 && colonne  == 7)
                            {
                                milieu = true;
                            }
                        }
                        if (!milieu)
                        {
                            Console.WriteLine("Le mot ne passe pas par le centre");
                            res = false;
                        }
                    }
                    if (direction == 'D')
                    {
                        bool milieu = false;
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if(ligne==7 && colonne + i == 7)
                            {
                                milieu = true;
                            }
                        }
                        if (!milieu)
                        {
                            Console.WriteLine("Le mot ne passe pas par le centre");
                            res = false;
                        }
                    }
                }
            }
            return res;
        }
        public bool Test_joueur(string mot, int ligne, int colonne, char direction, Joueur joueur,int indice=-1)// int indice=-1
        {
            bool res = true;
            int longueur = mot.Length;
            if (direction == 'B' && ligne + longueur < 15 && colonne + longueur < 15 && colonne >= 0 && ligne >= 0)
            {
                if (!this.Bas(ligne, colonne, mot, joueur,indice))
                {
                    res = false;
                }
            }
            if (direction == 'D' && ligne + longueur < 15 && colonne + longueur < 15 && colonne >= 0 && ligne >= 0)
            {
                if (!this.Droite(ligne, colonne, mot, joueur,indice))
                {
                    res = false;
                }
            }
            return res;

        }
        static public int somme(string mot,int ligne,int colonne,char direction)
        {
            mot = mot.ToUpper();
            int res = 0;
            if (direction == 'D')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (plateau[ligne, colonne + i] != "-1")
                    {
                        res = res + sac.Valeur_calcul(mot[i]);
                    }
                }
            }
            if (direction == 'B')
            {
                for(int i = 0; i < mot.Length; i++)
                {
                    if (plateau[ligne + i, colonne] != "-1")
                    {
                        res = res + sac.Valeur_calcul(mot[i]);
                    }
                }
            }
            return res;
        }
        public static int Score(string mot, int ligne, int colonne, char direction,int indice=-1)
        {
            int sum = 0;
            int facteur = 1;
            int sommepartielle = 0;
            if (direction == 'B')
            {
                for(int i = 0; i < mot.Length; i++)
                {
                    if (i != indice)
                    {
                        if (multiplicateur[ligne + i, colonne] == "T")
                        {
                            facteur = facteur * 3;
                            sum = sum + sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne + i, colonne] == "D")
                        {
                            facteur = facteur * 2;
                            sum = sum + sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne + i, colonne] == "_")
                        {

                            sum = sum + sac.Valeur_calcul(mot[i]);

                        }
                        if (multiplicateur[ligne + i, colonne] == "t")
                        {
                            sum = sum + 3 * sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne + i, colonne] == "d")
                        {
                            sum = sum + 2 * sac.Valeur_calcul(mot[i]);
                        }
                    }
                    else
                    {
                        
                        if (multiplicateur[ligne + i, colonne] == "T")
                        {
                            facteur = facteur * 3;

                        }
                        if (multiplicateur[ligne + i, colonne] == "D")
                        {
                            facteur = facteur * 2;
                        }
                    }


                }
                for (int i = 0; i < mot.Length; i++)
                {

                    string MotHorizontal = ChercherMotHorizontal(ligne + i, colonne, Convert.ToString(mot[i]));
                    if (MotHorizontal.Length > 1 && plateau[ligne+i,colonne]=="_")
                    {
                        if (i == indice)
                        {

                            sommepartielle = sommepartielle + somme(MotHorizontal,ligne,colonne,direction)-sac.Valeur_calcul(mot[indice]);//attention
                        }
                        else
                        {
                            sommepartielle = sommepartielle + somme(MotHorizontal,ligne,colonne,direction);
                        }
                    }

                    
                }
                int debut = colonne;
                while(colonne>0 && plateau[ligne, colonne - 1] != "_")
                {
                    sum = sum + sac.Valeur_calcul(Convert.ToChar(plateau[ligne, colonne - 1]));
                    colonne = colonne - 1;
                }
                colonne = debut;
                while(colonne<14 && plateau[ligne, colonne + 1] != "_")
                {
                    sum = sum + sac.Valeur_calcul(Convert.ToChar(plateau[ligne, colonne + 1]));
                    colonne = colonne + 1;
                }

            }
            if (direction == 'D')
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (i != indice)
                    {
                        if (multiplicateur[ligne, colonne + i] == "T")
                        {
                            facteur = facteur * 3;
                            sum = sum + sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne, colonne + i] == "D")
                        {
                            facteur = facteur * 2;
                            
                            sum = sum + sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne, colonne + i] == "_")
                        {

                            sum = sum + sac.Valeur_calcul(mot[i]);
                            
                        }
                        if (multiplicateur[ligne, colonne + i] == "t")
                        {
                            sum = sum + 3 * sac.Valeur_calcul(mot[i]);
                        }
                        if (multiplicateur[ligne, colonne + i] == "d")
                        {
                            sum = sum + 2 * sac.Valeur_calcul(mot[i]);
                        }
                    }
                    else
                    {
                        if (multiplicateur[ligne , colonne+i] == "T")
                        {
                            facteur = facteur * 3;

                        }
                        if (multiplicateur[ligne , colonne+i] == "D")
                        {
                            facteur = facteur * 2;
                        }
                    }
                    string MotVertical = ChercherMotVertical(ligne, colonne + i, Convert.ToString(mot[i]));
                    if (MotVertical.Length > 1 && plateau[ligne, colonne + i] == "_")
                    {
                        if (i == indice)
                        {
                            sommepartielle = sommepartielle + somme(MotVertical,ligne,colonne,direction)-sac.Valeur_calcul(mot[indice]);
                        }
                        else
                        {
                            sommepartielle = sommepartielle + somme(MotVertical, ligne, colonne, direction);
                        }
                    }


                }
                int debut = ligne;
                while (ligne > 0 && plateau[ligne-1, colonne ] != "_")
                {
                    sum = sum + sac.Valeur_calcul(Convert.ToChar(plateau[ligne-1, colonne ]));
                    ligne =  ligne - 1;
                }
                ligne = debut;
                while (ligne < 14 && plateau[ligne+1, colonne ] != "_")
                {
                    sum = sum + sac.Valeur_calcul(Convert.ToChar(plateau[ligne+1, colonne ]));
                    ligne = ligne + 1;
                }


            }
            
            

            
            return sum*facteur+sommepartielle;

        }
        public void EcrireMot(string mot, int ligne, int colonne, char direction,Joueur j,int indice=-1)
        {
            mot = mot.ToUpper();
            if (direction == 'D')
            {
                for(int i = 0; i < mot.Length; i++)
                {
                    if (plateau[ligne, colonne + i] == "_")
                    {
                        if (i != indice)
                        {
                            j.Remove_Main_Courante(sac.conversion_jeton(mot[i]));
                        }
                        else
                        {
                            j.Remove_Main_Courante(sac.conversion_jeton('*'));
                        }
                        
                    }
                    plateau[ligne, colonne+i] = Convert.ToString(mot[i]);
                    
                }
            }
            if (direction == 'B')
            {
                for(int i = 0; i < mot.Length; i++)
                {
                    Console.WriteLine(plateau[ligne + i, colonne]);
                    if (plateau[ligne + i, colonne] == "_")
                    {
                        if (i != indice)
                        {
                            j.Remove_Main_Courante(sac.conversion_jeton(mot[i])); // problème ici ca enlève pas le jeton de la main wtf
                            
                        }
                        else
                        {
                            j.Remove_Main_Courante(sac.conversion_jeton('*'));
                        }
                    }
                    plateau[ligne + i, colonne] = Convert.ToString(mot[i]);
                    
                }
            }
            if(indice>=0 && indice < mot.Length)
            {
                if (direction == 'B')
                {
                    multiplicateur[ligne + indice, colonne] = "-1";
                }
                if (direction == 'D')
                {
                    multiplicateur[ligne, colonne + indice] = "-1";
                }
            }
        }
        
        
    }
}
