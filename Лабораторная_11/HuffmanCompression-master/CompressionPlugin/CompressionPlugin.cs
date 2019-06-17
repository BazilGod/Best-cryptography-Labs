using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman;
using System.Collections;

namespace CompressionPlugin
{
    public class CompressionPlugin : MarshalByRefObject, IPlugin{
        public List<bool>[] dictionary = new List<bool>[256];

        private String namePlugin = "CompressionPlugin";
        public String PluginName {
            get{ return this.namePlugin; }
        }

        public bool Compress(ref Huffman.HuffmanData data) {
            byte[] inValue = data.uncompressedData;
            data.frequency = frequency(inValue);

            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());

            data.compressedData = storeContentToByteArray(inValue);
            data.sizeOfUncompressedData = inValue.Count();

            return true;
        }

        public bool Decompress(ref Huffman.HuffmanData data) {
            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());
            data.uncompressedData = decodeBitArray(new BitArray(data.compressedData),ref treeTop, ref data.sizeOfUncompressedData);
            return true;
        }

        /*
         * Calculate to frequency of each character
         */
        static public List<KeyValuePair<byte,int>> frequency(byte[] data) {
            int[] frequenceTable = new int[256];
            int i, length = data.Length;

            for (i = 0; i < length; i++) {
                frequenceTable[data[i]]++; 
            }

            List<KeyValuePair<byte, int>> list = new List<KeyValuePair<byte, int>>();

            for (i = 0; i < 256; i++) {
                if (frequenceTable[i] != 0)
                    list.Add(new KeyValuePair<byte, int>((byte)i, frequenceTable[i]));
            }

            return list;
        }

        /*
         * Find the 2 nodes which have the 2 minimum values
         * Left is the minimum
         * Right is the second minimum
         */
        static public void findMinimum(List<Node> list, ref Node left,ref Node right) {
            int i, length = list.Count;

            for (i = 1; i < length; i++){
                if (list[i].Value <= right.Value){
                    if (list[i].Value <= left.Value){
                        right = left;
                        left = list[i];
                    }else{
                        right = list[i];
                    }
                }     
            }
        }

        /*
         * Create the Huffman tree from the frequency table
         */
        static public Node createBinaryTree(List<KeyValuePair<byte, int>> data) {
            List<Node> listNode = new List<Node>();
            int i, length = data.Count();
            Node left, right;

            for (i = 0; i < length; i++) {
                listNode.Add(new Node { Key = data[i].Key, Value = data[i].Value });
            }

            if (length == 1) {
                right = listNode[0];
                listNode.Add(new Node { Value = right.Value, Right = right, Left =  new Node(), isParent = 1 });
                listNode.Remove(right);
            }

            while (listNode.Count > 1) {
                left = listNode[0];
                right = listNode[1];
                findMinimum(listNode, ref left, ref right);

                listNode.Add(new Node { Value = left.Value + right.Value, Left = left, Right = right, isParent = 1 });
                listNode.Remove(left);
                listNode.Remove(right);
            }

            return listNode[0];
        }

        /*
         * Get through the Huffman tree to establish the binary code for each letter
         */
        public void createDictionary(Node node, List<bool> bools) {
            if (node.isParent != 1)
                dictionary[node.Key] =  bools;

            else {
                List<bool> bools_copy = new List<bool>(bools);
                bools_copy.Add(true);
                bools.Add(false);

                createDictionary(node.Right, bools_copy);
                createDictionary(node.Left, bools);
            }
        }

        /*
         * Store the original content into a ByteArray
         */
        public byte[] storeContentToByteArray(byte[] data) {
            List<bool> encoded = new List<bool>();
            int i, j, length = data.Length, count;
            List<bool> bools;

            for (i = 0; i < length; i++) {
                bools = dictionary[data[i]];
                count = bools.Count;
                for (j = 0; j < count; j++)
                    encoded.Add(bools[j]);
            }

            BitArray bits = new BitArray(encoded.ToArray());
            length = bits.Length;

            i = (length % 8 == 0) ? length / 8 : length / 8 + 1;

            byte[] res = new byte[i];
            bits.CopyTo(res, 0);

            return res;
        }

        /*
         * Decode the encoded BitArray to byte[]
         */
        static public byte[] decodeBitArray(BitArray bits,ref Node treeTop, ref int length) {
            byte[] res = new byte[length];
            int j=0,i;
            Node node = treeTop;

            for (i = 0; j < length; i++) {

                node = bits[i] ? node.Right : node.Left;

                if (node.isParent != 1) {
                    res[j] = node.Key;
                    node = treeTop;
                    j++;
                }
            }

            return res;
        }
    }
}
