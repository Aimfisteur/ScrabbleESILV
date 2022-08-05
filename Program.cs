using System;
using System.Collections.Generic;
using System.Linq;//to check if in the array
using System.IO;

namespace ScrabbleESILV
{
    class Program
    {
        public static List<List<string>> Powerset(List<string> array)
        {
            List<List<string>> subset = new List<List<string>>();
            subset.Add(new List<string>());
            foreach (string ele in array)
            {
                int length = subset.Count;
                for (int i = 0; i < length; i++)
                {
                    List<string> current = new List<string>(subset[i]);
                    current.Add(ele);
                    subset.Add(current);
                }
            }
            return subset;
        }
        
        public static void afficherListList(List<List<string>> list)
        {
            foreach (List<string> element in list)
            {
                string res = "";
                for(int i = 0; i < element.Count; i++)
                {
                    res = res + element[i];
                }
                Console.WriteLine(res);
            }
        }
        public static List<List<string>> GetPermutations(List<string> array)
        {
            List<List<string>> permutations = new List<List<string>>();
            GetPermutations(array, new List<string>(), permutations);
            return permutations;
        }

        public static void GetPermutations(List<string> array, List<string> currentPermutation,List<List<string>> permutations)
        {
            if(array.Count==0 && currentPermutation.Count > 0)
            {
                permutations.Add(currentPermutation);

            }
            else
            {
                for(int i=0;i< array.Count; i++)
                {
                    List<string> newArray = new List<string>(array);
                    newArray.RemoveAt(i);
                    List<string> newPermutation = new List<string>(currentPermutation);
                    newPermutation.Add(array[i]);
                    GetPermutations(newArray, newPermutation, permutations);
                }
            }
        }
        static void Main(string[] args)
        {



            
            Jeu jeu = new Jeu();
            jeu.Lancer_Jeu();
           


            Console.ReadKey();
        }
    }
}
