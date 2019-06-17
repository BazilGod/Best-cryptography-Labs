using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompressionPlugin;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace ConsoleApplication {
    class Program {
        static void Main(string[] args) {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            
            // Test Frequency
            checkFrequency1();
            checkFrequency2();
            checkFrequency3(@"E:\Text.txt");

            // Test Minimum
            checkMinimum1();
            checkMinimum2();
            checkMinimum3();
            checkMinimum4();

            // test Tree
            checkTree1();
            checkTree2();
            checkTree3();

            // test Dictionnary
            checkDictionary1();
            checkDictionary2();

            // test storeContentToByteArray
            checkstoreContentToByteArray();

            // test compress
            checkDecompress1();
            checkDecompress2();
            checkCompress(@"E:\Text.txt");
        
            // Benchmark
            benchmark(@"E:\Text.txt");
            //benchmark(@"E:\amd-catalyst-14-9-win7-win8.1-64bit-dd-ccc-whql.exe");
        
        }

        /***************************************************************************************************************/

        /*
         * Convert String to byte[]
         */
        static public byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /***************************************************************************************************************/

        static private void checkFrequency1() {
            byte[] data = GetBytes("abbcccdddd"); // One a, two b, three c and 4 d
            List<KeyValuePair<byte, int>> frequencyTable = CompressionPlugin.CompressionPlugin.frequency(data);

            KeyValuePair<byte, int> pair = frequencyTable.Find(x => x.Key == 97); //Check a frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 98); //Check b frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 99); //Check c frequency
            Debug.Assert(pair.Value == 3, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 100); //Check d frequency
            Debug.Assert(pair.Value == 4, "Echec de la méthode frequency");
        }

        static private void checkFrequency2() {
            byte[] data = GetBytes("hey thibault!");
            List<KeyValuePair<byte, int>> frequencyTable = CompressionPlugin.CompressionPlugin.frequency(data);

            KeyValuePair<byte, int> pair = frequencyTable.Find(x => x.Key == 104); //Check h frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency : h");

            pair = frequencyTable.Find(x => x.Key == 101); //Check e frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : e");

            pair = frequencyTable.Find(x => x.Key == 121); //Check y frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : y");

            pair = frequencyTable.Find(x => x.Key == 32); //Check space frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : space");

            pair = frequencyTable.Find(x => x.Key == 116); //Check t frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency :t");

            pair = frequencyTable.Find(x => x.Key == 105); //Check i frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :i");

            pair = frequencyTable.Find(x => x.Key == 98); //Check b frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :b");

            pair = frequencyTable.Find(x => x.Key == 97); //Check a frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :a");

            pair = frequencyTable.Find(x => x.Key == 117); //Check u frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :u");

            pair = frequencyTable.Find(x => x.Key == 108); //Check l frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :l");
        }

        static private void checkFrequency3(String filename) {
            byte[] data = File.ReadAllBytes(filename);
            int lengh = data.Count();
            int totalFrequency = 0;
            List<KeyValuePair<byte, int>> frequencyTable = CompressionPlugin.CompressionPlugin.frequency(data);
            foreach (KeyValuePair<byte, int> pair in frequencyTable) {
                totalFrequency += pair.Value;
            }
            Debug.Assert(lengh == totalFrequency, "Echec de la methode frequency");
        }

        /***************************************************************************************************************/

        static private void checkMinimum1() {

            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 5 });
            listNode.Add(new Node { Key = 1, Value = 8 });
            listNode.Add(new Node { Key = 2, Value = 15 });
            listNode.Add(new Node { Key = 3, Value = 2 });
            listNode.Add(new Node { Key = 4, Value = 7 });
            listNode.Add(new Node { Key = 5, Value = 2 });
            listNode.Add(new Node { Key = 6, Value = 3 });

            Node left = listNode[0];
            Node right = listNode[1];
            CompressionPlugin.CompressionPlugin.findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 2 && right.Value == 2, "Echec méthode findMinimum - checkMinimum1");
        }

        static private void checkMinimum2() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 1 });
            listNode.Add(new Node { Key = 1, Value = 2 });
            listNode.Add(new Node { Key = 2, Value = 4 });
            listNode.Add(new Node { Key = 3, Value = 3 });
            listNode.Add(new Node { Key = 4, Value = 4 });
            listNode.Add(new Node { Key = 5, Value = 6 });
            listNode.Add(new Node { Key = 6, Value = 8 });

            Node left = listNode[0];
            Node right = listNode[1];
            CompressionPlugin.CompressionPlugin.findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 2, "Echec méthode findMinimum - checkMinimum2");
        }

        static private void checkMinimum3() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 2 });
            listNode.Add(new Node { Key = 1, Value = 1 });
            listNode.Add(new Node { Key = 2, Value = 1 });
            listNode.Add(new Node { Key = 3, Value = 4 });
            listNode.Add(new Node { Key = 4, Value = 4 });
            listNode.Add(new Node { Key = 5, Value = 6 });
            listNode.Add(new Node { Key = 6, Value = 8 });

            Node left = listNode[0];
            Node right = listNode[1];
            CompressionPlugin.CompressionPlugin.findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 1, "Echec méthode findMinimum  - checkMinimum3");
        }

        static private void checkMinimum4() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 5 });
            listNode.Add(new Node { Key = 1, Value = 5 });
            listNode.Add(new Node { Key = 2, Value = 5 });
            listNode.Add(new Node { Key = 3, Value = 5 });
            listNode.Add(new Node { Key = 4, Value = 5 });
            listNode.Add(new Node { Key = 5, Value = 1 });
            listNode.Add(new Node { Key = 6, Value = 1 });

            Node left = listNode[0];
            Node right = listNode[1];
            CompressionPlugin.CompressionPlugin.findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 1, "Echec méthode findMinimum  - checkMinimum4");
        }

        /***************************************************************************************************************/

        static private void checkTree1() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(1, 9));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 15));
            listPair.Add(new KeyValuePair<byte, int>(4, 3));
            listPair.Add(new KeyValuePair<byte, int>(5, 5));
            listPair.Add(new KeyValuePair<byte, int>(6, 4));
            listPair.Add(new KeyValuePair<byte, int>(7, 13));
            listPair.Add(new KeyValuePair<byte, int>(8, 11));
            listPair.Add(new KeyValuePair<byte, int>(9, 1));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);
            int valueTotal = 0;
            for (int i = 0; i < listPair.Count; i++) {
                valueTotal += listPair[i].Value;
            }

            Debug.Assert(valueTotal == treeTop.Value, "Erreur dans la méthode createBinaryTree - checkTree1");
        }

        static private void checkTree2() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);
            Debug.Assert(treeTop.Left.Left.Key == 4 && treeTop.Left.Right.Key == 3 && treeTop.Right.Right.Left.Key == 2 && treeTop.Right.Right.Right.Key == 1 && treeTop.Right.Left.Key == 0, "Erreur dans la méthode createBinaryTree");
        }

        static private void checkTree3() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 1));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);
            Debug.Assert(treeTop.Value == 1 && treeTop.Right.Value == 1 && treeTop.Right.Key == 0, "Erreur dans la méthode createBinaryTree");

        }


        /***************************************************************************************************************/

        static private void checkDictionary1() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);

            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            classe.createDictionary(treeTop, new List<bool>()); // Something wrong with this

            List<bool>[] dictionary = classe.dictionary;

            Debug.Assert(dictionary[0][0] && !dictionary[0][1], "Erreur dans la création du dico");
            Debug.Assert(dictionary[1][0] && dictionary[1][1] && dictionary[1][2], "Erreur dans la création du dico"); // Check for 0         
            Debug.Assert(dictionary[2][0]  && dictionary[2][1] && !dictionary[2][2], "Erreur dans la création du dico");
            Debug.Assert(!dictionary[3][0] && dictionary[3][1], "Erreur dans la création du dico");
            Debug.Assert(!dictionary[4][0] && !dictionary[4][1] , "Erreur dans la création du dico");
            
        }

        static private void checkDictionary2() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);

            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            classe.createDictionary(treeTop, new List<bool>()); // Something wrong with this

            List<bool>[] dictionary = classe.dictionary;

            int k = 0;

            for (int i = 0; i < 256; i++) {
                if (dictionary[i] != null) k++;             
            }

            Debug.Assert(k == listPair.Count(), "Erreur dans la création du dico");
            
        }


        /***************************************************************************************************************/

        static private void checkstoreContentToByteArray(){
            byte[] a = {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3,4,4,4,4,4 };

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(listPair);

            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            classe.createDictionary(treeTop, new List<bool>());

            List<bool>[] dictionary = classe.dictionary;

            byte[] res = classe.storeContentToByteArray(a);

            Debug.Assert(res.Count() == 8, "Erreur dans sotreContentToByteArray"); // Taille de la bitArray = 60, 60/8 = 7.5 donc 60%8 différent de 0 donc 60/8 = 7 + 1 = 8 !
        }


        /***************************************************************************************************************/

        static private void checkDecompress1() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };

            List<KeyValuePair<byte, int>> frequency = CompressionPlugin.CompressionPlugin.frequency(data);
            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop, new List<bool>());
            byte[] compressedData = classe.storeContentToByteArray(data);
            int sizeofUnCompressData = data.Count();

            // Clear dictionary
            for (int i = 0; i < 256; i++) {
                classe.dictionary[i] = null;
            }


            Node treeTop2 = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop2, new List<bool>());
            byte[] dataDecompressed = CompressionPlugin.CompressionPlugin.decodeBitArray(new BitArray(compressedData),ref treeTop2, ref sizeofUnCompressData);

            Debug.Assert(data.Count() == dataDecompressed.Count(), "La taille de l'entrée ne correspond pas à celle de la sortie");
            for (int i = 0; i < data.Count(); i++) {
                Debug.Assert(data[i] == dataDecompressed[i], "Erreur dans la tout le programme - checkCompress");
            }      
        }


        static private void checkDecompress2() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };

            List<KeyValuePair<byte, int>> frequency = CompressionPlugin.CompressionPlugin.frequency(data);
            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop, new List<bool>());
            byte[] compressedData = classe.storeContentToByteArray(data);
            int sizeofUnCompressData = data.Count();

            // Clear dictionary
            for (int i = 0; i < 256; i++) {
                classe.dictionary[i] = null;
            }


            Node treeTop2 = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop2, new List<bool>());
            byte[] dataDecompressed = CompressionPlugin.CompressionPlugin.decodeBitArray(new BitArray(compressedData), ref treeTop2, ref sizeofUnCompressData);

            Debug.Assert(data.Count() == dataDecompressed.Count(), "La taille de l'entrée ne correspond pas à celle de la sortie");
            for (int i = 0; i < data.Count(); i++) {
                Debug.Assert(data[i] == dataDecompressed[i], "Erreur dans la tout le programme! - checkCompress");
            }
        }



        static private void checkCompress(String filename) {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = File.ReadAllBytes(filename);

            List<KeyValuePair<byte, int>> frequency = CompressionPlugin.CompressionPlugin.frequency(data);
            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop, new List<bool>());
            byte[] compressedData = classe.storeContentToByteArray(data);
            int sizeofUnCompressData = data.Count();
            
            // Clear dictionary
            for (int i = 0; i < 256; i++) {
                classe.dictionary[i]= null;
            }
                

            Node treeTop2 = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop2, new List<bool>());
            byte[] dataDecompressed = CompressionPlugin.CompressionPlugin.decodeBitArray(new BitArray(compressedData),ref treeTop2, ref sizeofUnCompressData);

            Debug.Assert(data.Count() == dataDecompressed.Count(), "La taille de l'entrée ne correspond pas à celle de la sortie");
            for (int i = 0; i < data.Count();i++) {
                Debug.Assert(data[i] == dataDecompressed[i], "Erreur dans la tout le programme - checkCompress");
            }

            File.WriteAllBytes(@"E:\TestfrequencyResult.txt", dataDecompressed);
        }

        /***************************************************************************************************************/

        static private void benchmark(String filename) {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = File.ReadAllBytes(filename);

            // Begin benchmark Compress
            Stopwatch s1 = Stopwatch.StartNew();

            List<KeyValuePair<byte, int>> frequency = CompressionPlugin.CompressionPlugin.frequency(data);
            Node treeTop = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop, new List<bool>());
            byte[] compressedData = classe.storeContentToByteArray(data);
            int sizeofUncompressData = data.Count();

            s1.Stop(); // end benchmark

            // Clear dictionary
            for (int i = 0; i < 256; i++) {
                classe.dictionary[i] = null;
            }

            // Begin benchmark Decompress
            Stopwatch s2 = Stopwatch.StartNew();

            Node treeTop2 = CompressionPlugin.CompressionPlugin.createBinaryTree(frequency);
            classe.createDictionary(treeTop2, new List<bool>());
            byte[] dataDecompressed = CompressionPlugin.CompressionPlugin.decodeBitArray(new BitArray(compressedData),ref treeTop2, ref sizeofUncompressData);

            s2.Stop(); // End benchmark

            Console.WriteLine("Compression :");
            Console.Write("     Durée du test :");
            Console.Write(s1.ElapsedMilliseconds);
            Console.WriteLine(" ms");

            Console.WriteLine("Décompression :");
            Console.Write("     Durée du test :");
            Console.Write(s2.ElapsedMilliseconds);
            Console.WriteLine(" ms");

            Console.Write("     Durée total du test :");
            Console.Write(s2.ElapsedMilliseconds + s1.ElapsedMilliseconds);
            Console.WriteLine(" ms");

            Console.Write("     Pourcentage de compression :");
            Console.WriteLine(1 - (float)compressedData.Count() / (float)data.Count());
        }
    }
}
