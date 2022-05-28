using System;

namespace Elite
{
    public class Line
    {
        
        public Vector3 start;
        public Vector3 end;
        public short colour;
        public bool visible;



        public Line(Vector3 _start, Vector3 _end, short _colour)
        {
            start = _start;
            end = _end;
            colour = _colour;

            Renderer.AddLine(this);
        }




    }
}
