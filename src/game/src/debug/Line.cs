// Line renderer for debug purposes
using System;

namespace Elite
{
    public class Line : GameObject
    {
        private Vector3 start;
        private Vector3 end;

        public void SetLine(Vector3 _start, Vector3 _end)
        {

            start = _start; end = _end;


            position = start;
            forward = (end-start).Normalise();

        }
        public Line(Vector3 _start, Vector3 _end)
        {
            start = _start; end = _end;


            mesh = Models.line;
            colour = 9;
            getsClipped = true;
            getsCulled = false;

            SetLine(_start,_end);
        }

 


    }
}
