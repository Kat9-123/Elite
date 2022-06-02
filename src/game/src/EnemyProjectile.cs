using System;

namespace Elite
{
    public class EnemyProjectile : GameObject
    {

       // private const int SEGMENT_LENGTH = 5;

        private Timer fireTimer;
        private Timer laserVisibilityTimer;

        private Enemy owner;


        private float lifeTime = 3f;
        

        private bool shooting;

        private float accuracy;
        private float damage;
      //  public int length;

        private Vector3 pos;

        private Vector3 momentum;

        
        public EnemyProjectile(Enemy _owner,Vector3 _pos, Vector3 _momentum, float _damage, float _accuracy, float _lifeTime)
        {
        


            momentum = _momentum;
            damage = _damage;
            pos = _pos;
            accuracy = _accuracy;
            mesh = ModelHandler.LoadModel("Cube.obj");
            scale = new Vector3(0.1f,0.1f,2f);
            owner = _owner;


           // getsCulled = false;



            up = owner.up;
            position = owner.position + Utils.RelativeToRotation(pos,owner.forward,up);
            forward = (Engine.cameraPosition-position).Normalise();//owner.forward;

        }


        public override void Update(float deltaTime)
        {
            position += momentum;
            position += forward*deltaTime*500f;

            lifeTime -= deltaTime;

            if(lifeTime <= 0)
            {
                Engine.QueueDestruction(this);
            }
        }


 









    }
}
