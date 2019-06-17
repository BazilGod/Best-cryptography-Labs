using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CompressionPlugin {
    public class Node {
        public Node Right{ get; set;}
        public Node Left{ get; set;}
        public byte Key { get; set; }
        public int Value { get; set; }

        public byte isParent { get; set; }
    }
}
