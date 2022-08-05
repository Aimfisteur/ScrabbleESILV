using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;//to check if in the array
using System.IO;

namespace ScrabbleESILV
{
    class Jeu
    {
        List<Joueur> joueurs= new List<Joueur>();
        Dictionnaire mondico;
        Plateau monplateau;
        Sac_Jetons monsac_jetons;
        int minutes;
        TimeSpan end;
        int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        public Jeu()
        {
            Console.WriteLine("veuillez déclarer tout d'abord le nom des participants en faisant enter jusqu'à écrire FIN ce qui va finir l'entrée des joueurs");
            string LectureConsole = Console.ReadLine();
            while (LectureConsole == "FIN")
            {
                Console.WriteLine("le nombre de joueur doit etre supérieur à  0");
                LectureConsole = Console.ReadLine();
            }
            
            while (LectureConsole != "FIN" )
            {
                
                Joueur j = new Joueur(LectureConsole);
                joueurs.Add(j);
                
                LectureConsole = Console.ReadLine();
            }
            Console.WriteLine("veuillez saisir maintenant la langue du dictionnaire( Francais) si c'est en francais");
            LectureConsole = Console.ReadLine().ToUpper();
            bool test = true;

            while (test)
            {
                try
                {
                    
                    if (LectureConsole == "FRANCAIS")
                    {
                        this.mondico = new Dictionnaire("Francais.txt", LectureConsole);
                    }
                    else
                    {
                        string langue = LectureConsole;
                        Console.WriteLine("veuillez saisir l'addresse du dictionaire non francais");
                        LectureConsole = Console.ReadLine();
                        this.mondico = new Dictionnaire(LectureConsole, langue);

                    }
                    test = false;

                }
                catch
                {
                    Console.WriteLine("l'adresse saisie n'est pas valide ");
                }
            }
            
            
            Console.WriteLine("Souhaitez vous reprendre votre partie ?NON si c'est pas le cas");
            LectureConsole = Console.ReadLine().ToUpper();
            if (LectureConsole == "NON")
            {
                this.monplateau = new Plateau("PlateauVide.txt", mondico);
            }
            else
            {
                Console.WriteLine("veuillez saisir l'adresse svp pls.");
                
                bool test2 = true;
                while (test2)
                {
                    try
                    {
                        LectureConsole = Console.ReadLine();
                        this.monplateau = new Plateau(LectureConsole, mondico);
                        test2 = false;
                    }
                    catch
                    {
                        Console.WriteLine("l'adresse saisie est inccorecte");
                    }

                }
                
            }
            Console.WriteLine("Souhaitez vous choisir le sac de jeton de base? OUI ou NON");
            LectureConsole = Console.ReadLine().ToUpper();
            while(LectureConsole!="OUI" && LectureConsole != "NON")
            {
                Console.WriteLine("écrivez OUI OU NON");
                LectureConsole = Console.ReadLine().ToUpper();
            } 
            if (LectureConsole == "OUI")
            {
                this.monsac_jetons = new Sac_Jetons("Jetons.txt");
            }
            else
            {
                bool test3 = true;
                Console.WriteLine("veuillez saisir le path du sac de jetons souhaité");
                while (test3)
                {
                    try
                    {
                        LectureConsole = Console.ReadLine();
                        this.monsac_jetons = new Sac_Jetons(LectureConsole);
                        test3 = false;
                    }
                    catch
                    {
                        Console.WriteLine("le path choisi n'est pas valide");
                    }
                }
                
            }
            Console.WriteLine("veuillez saisir le nombre de minutes par tours");
            bool test4 = true;
            while (test4)
            {
                try
                {
                    this.minutes = Convert.ToInt32(Console.ReadLine());
                    end = new TimeSpan(0, minutes, 0);
                    test4 = false;
                }
                catch
                {
                    Console.WriteLine("le nombre saisi est incorrecte");
                }
            }

            

            
        }
        public void remplissage(Joueur j)
        {
            var r= new Random();
            
            while(j.Main.Count<7 && monsac_jetons.Sac_restant() != 0)
            {
                Jeton jet = monsac_jetons.Retire_Jeton(r);
                
                j.Add_Main_Courante(jet);
                
            }
        }
        

        public void Lancer_Jeu()
        {
            while (monsac_jetons.Sac_restant() != 0)
            {
                for(int i = 0; i < joueurs.Count; i++)
                {

                    TimeSpan myDateResult = DateTime.Now.TimeOfDay;
                    bool joue = false;

                    while ((DateTime.Now.TimeOfDay - myDateResult < end) && !joue)
                    {


                        remplissage(joueurs[i]);//enlever le console.WriteLine

                        
                       
                        
                        string mot="bbvozdzdzbvuez";
                        int ligne=0;
                        int colonne=0;
                        char direction='A';
                        int indice=0;

                        Console.WriteLine();

                        while ((!monplateau.Test_Plateau(mot, ligne, colonne, direction) || (!monplateau.Test_joueur(mot, ligne, colonne, direction, joueurs[i],indice))))
                        {
                            Console.WriteLine("c'est au tour de :\n" + joueurs[i].toString() + "\n");
                            monplateau.toString();
                            Console.WriteLine();
                            Console.WriteLine("veuillez saisir le mot que vous souhaitez  placer ( si vous voulez stopper le jeu veuillez saisir:STOP_SVP");

                            Console.WriteLine();
                            mot = Console.ReadLine().ToUpper();
                            if(mot== "STOP_SVP")
                            {
                                //écriture des fichiers de joueurs:
                                for(int c = 0; c < joueurs.Count; c++)
                                {
                                    string nomfichier = joueurs[c].Nom + ".txt";
                                    StreamWriter sr = new StreamWriter(nomfichier);
                                    string ecrit = "";
                                    ecrit = ecrit + joueurs[c].Nom + ";" + joueurs[c].Score + ";";
                                    sr.WriteLine(ecrit);
                                    ecrit = "";
                                    for(int compteurmot = 0; compteurmot < joueurs[c].Mots.Count; compteurmot++)
                                    {
                                        ecrit = ecrit + joueurs[c].Mots[compteurmot] + ";";
                                    }
                                    sr.WriteLine(ecrit);
                                    ecrit = "";
                                    for(int compteurmain = 0; compteurmain < joueurs[c].Main.Count; compteurmain++)
                                    {
                                        ecrit = ecrit + joueurs[c].Main[compteurmain].Lettre + ";";
                                    }
                                    sr.Close();


                                }
                                //écriture du plateau
                                StreamWriter plato = new StreamWriter("plateausauvegarde.txt");
                                for(int k = 0; k < 15; k++)
                                {
                                    string lignetableau = "";
                                    for(int j = 0; j < 15; j++)
                                    {
                                        lignetableau = lignetableau + monplateau.PPlateau[k, j] + ";";
                                    }
                                    plato.WriteLine(lignetableau);
                                }
                                plato.Close();

                                throw new ArgumentNullException("fin du prog");
                            }
                            bool test = true;
                            while (test)
                            {
                                try
                                {
                                    Console.WriteLine("veuillez saisir la ligne");
                                    Console.WriteLine();


                                    ligne = (int)(Convert.ToChar(Console.ReadLine().ToUpper())) - 65;
                                    test = false;
                                }
                                catch
                                {
                                    Console.WriteLine("veuillez saisir une ligne correcte!!");
                                }
                            }
                            bool test1 = true;
                            while (test1)
                            {
                                try
                                {
                                    Console.WriteLine("veuillez saisir la colonne");
                                    Console.WriteLine();
                                    colonne = (int)(Convert.ToChar(Console.ReadLine().ToUpper())) - 65;
                                    test1 = false;
                                }
                                catch
                                {
                                    Console.WriteLine("veuillez saisir une colonne correcte!!");
                                }
                            }
                            bool test2 = true;
                            while (test2)
                            {
                                try
                                {
                                    Console.WriteLine("veuillez saisir la direction ( D pour droite et B pour bas)");
                                    Console.WriteLine();
                                    direction = char.ToUpper(Convert.ToChar(Console.ReadLine()));
                                    test2 = false;
                                }
                                catch
                                {
                                    Console.WriteLine("veuillez saisir une bonne direction!!");
                                }
                            }
                            bool test3 = true;
                            while (test3)
                            {
                                try
                                {
                                    Console.WriteLine("veuillez saisir l'indice du caractère avec le mot étoile(-1 si il n'y a pas *)");
                                    Console.WriteLine();
                                    indice = Convert.ToInt32(Console.ReadLine());
                                    while (indice < -1 || indice >= mot.Length)
                                    {
                                        Console.WriteLine("veuillez saisir l'indice du caractère avec le mot étoile");
                                        Console.WriteLine();
                                        indice = Convert.ToInt32(Console.ReadLine());
                                    }
                                    
                                    test3 = false;
                                }
                                catch
                                {
                                    Console.WriteLine("veuillez saisir un bon indice!!");
                                }
                            }

                            
                            
                            
                        }
                        int score;
                        score = Plateau.Score(mot, ligne, colonne, direction, indice);

                        Console.WriteLine("\nvous avez marqué un score de : " + score);
                        Console.WriteLine();

                        monplateau.EcrireMot(mot, ligne, colonne, direction, joueurs[i], indice);
                        joueurs[i].Score += score;
                        joueurs[i].Add_Mot(mot);
                        //Console.WriteLine("il reste" + monsac_jetons.Sac_restant() + "lettres dans le dico");
                        Console.WriteLine();

                        joue = true;



                        monplateau.NombreDeCoup += 1;
                    }

                }
            }
            //à finir
            Console.WriteLine("FIN DU JEU: Les scores sont donc:");
            string gagants = "";
            int maxscore = 0;
            for(int i = 0; i < joueurs.Count; i++)
            {
                Console.WriteLine("Le joueurs :" + joueurs[i].Nom + " a un score de : " + joueurs[i].Score);
                if(joueurs[i].Score > maxscore)
                {
                    maxscore = joueurs[i].Score;
                }

            }
            //affichage des gagnants
            for (int i = 0; i < joueurs.Count; i++)
            {
                
                if (joueurs[i].Score == maxscore)
                {
                    gagants = gagants +" "+ joueurs[i].Nom;
                }

            }
            Console.WriteLine("et les gagant sont : " + gagants);

        }
        

    }

}
