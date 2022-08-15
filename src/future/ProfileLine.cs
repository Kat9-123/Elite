// ProfileLine
namespace Elite
{
    public class ProfileLine : GameObject
    {


        public Vector3 start;

        public Vector3 end;



        public void SetLine()
        {
            position = start;
            forward = (end-start).Normalise();
            scale = new Vector3(1,1,start.DistanceTo(end));
        }
        public ProfileLine(Vector3 _start, Vector3 _end)
        {
            start = _start;
            end = _end;



            mesh = Models.line;
            colour = 10;
            getsClipped = true;
            getsCulled = false;

           
            movesWithCamera = true;

            SetLine();
        }



 


    }
}
