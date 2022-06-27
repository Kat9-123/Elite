using System;

namespace Elite
{
    public struct BoundingBox
    {
        public Vector3 start;
        public Vector3 end;


        public BoundingBox(Vector3 _start, Vector3 _end, Enemy? enemy=null)
        {
            
            start = _start;
            end = _end;
            if(enemy != null)
            {
                Engine.Instance(new BoundingBoxDisplay((Enemy)enemy, start,end));

            }



           
        }


    }
}