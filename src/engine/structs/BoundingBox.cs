using System;

namespace Elite
{
    public struct BoundingBox
    {
        public Vector3 start;
        public Vector3 end;


        public BoundingBox(Vector3 _start, Vector3 _end)
        {
            
            start = _start;
            end = _end;
           
        }


    }
}