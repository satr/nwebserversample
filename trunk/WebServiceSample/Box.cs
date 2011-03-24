using System;

namespace WebServiceSample {
    [Serializable]
    public class Box {
        public Box(): this(0,0,0){
        }

        public Box(int x, int y, int z){
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Volume{
            get{
                return X*Y*Z;
            }
            set { 
                //to nothing
            }
        }
    }
}
