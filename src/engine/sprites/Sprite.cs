namespace Elite
{

    public class Sprite
    {

        public Window.CharInfo[] pixels;
        public int height;
        public int width;

        public Sprite(string path="")
        {
            if(path != "")
            {
               SpriteLoader.SetSprite(this,path);
            }

        }
    }
}
