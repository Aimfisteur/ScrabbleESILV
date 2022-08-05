using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScrabbleESILV
{
    class Dictionnaire
    {
        Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
        string langue;


        public Dictionnaire(string path, string langue)
        {


            this.langue = langue;
            string ligne;
            StreamReader sr = new StreamReader(path);
            int key = 2;

            while ((ligne = sr.ReadLine()) != null)
            {
                string[] car = ligne.Split(" ");
                if (char.IsNumber(car[0][0]))
                {
                    List<string> newlist = new List<string>();
                    key = Convert.ToInt32(car[0]);
                    dic.Add(key, newlist);
                }
                else
                {
                    for (int i = 0; i < car.Length; i++)
                    {
                        dic[key].Add(car[i]);
                    }
                }


            }
        }
        public Dictionary<int,List<string>> Dic
        {
            get { return this.dic; }
           

        }
        public string toString()
        {
            string res = "Langue : "+langue+" et il y a :\n";
            int sum = 0;
            for(int i = dic.Count - 1; i >= 0; i--)
            {
                var item = dic.ElementAt(i);
                res = res + "un nombre de mot de " + item.Key + " lettres qui est de" + item.Value.Count+"\n";
                sum = sum + item.Value.Count;
            }
            return res+"\npour un total de : "+sum;

        }
        private bool rec(int gauche,int droite,List<string> L,string mot)
        {
            if (droite >= gauche)
            {
                int mid = (gauche + droite) / 2;
                
                
                if (L[mid] == mot)
                {
                    return true;
                }
                if (string.Compare(mot, L[mid]) < 0)
                {
                    return rec(gauche, mid - 1, L, mot);


                }
                else
                {
                    return rec(mid + 1, droite, L, mot);
                }
            }
            return false;
        }
        public bool RechDichoRecursif(string mot)
        {
            int longeur = mot.Length;
            bool test = false;
            for (int i = dic.Count - 1; i >= 0; i--)
            {
                var item = dic.ElementAt(i);
                if (item.Key == longeur)
                {
                    test = true;
                }
            }
            if (!test)
            {
                return false;
            }

                return rec(0, dic[longeur].Count-1, dic[longeur], mot.ToUpper());


        }
    }
}
