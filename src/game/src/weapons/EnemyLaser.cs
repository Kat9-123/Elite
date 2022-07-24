using System;

namespace Elite
{
    public class EnemyLaser : GameObject
    {

       // private const int SEGMENT_LENGTH = 5;

        private Timer fireTimer;
        private Timer laserVisibilityTimer;

        private Enemy owner;
        

        private Player player;
        private bool shooting;

        private float accuracy;
        private float damage;
      //  public int length;

        private Vector3 pos;

        
        public EnemyLaser(Enemy _owner, Mesh _mesh,Vector3 _pos, float _damage, float _accuracy,float fireTime, float laserVisibilityTime)
        {

            damage = _damage;
            pos = _pos;
            accuracy = _accuracy;
            visible = false;
            owner = _owner;

            mesh = _mesh;
            getsCulled = false;
            fireTimer = new Timer(fireTime);
            laserVisibilityTimer = new Timer(laserVisibilityTime);

            player = Engine.main.player;

        }

        public override void Start()
        {
            Engine.MoveLayer(this,Engine.main.enemyLayer); 
        }



        public void Shoot(float deltaTime)
        {
            forward = owner.forward;
            up = owner.up;
            position = owner.position + Utils.RelativeToRotation(pos,forward,up);


            if(shooting)
            {

                if(laserVisibilityTimer.Accumulate(deltaTime))
                {
                    shooting = false;
                    visible = false;
                    laserVisibilityTimer.Reset();
                }
            }
    

            if(!fireTimer.Accumulate(deltaTime))
            {

                return;
            }
            fireTimer.Reset();
            shooting = true;
            visible = true;

            // && (position.SquaredDistanceTo(Engine.cameraPosition) < 20000)
           // Renderer.WriteLine(forward.Dot((Engine.cameraPosition-position).Normalise()).ToString());
            //if((forward.Dot((Engine.cameraPosition-position).Normalise()) > accuracy))
            //{
              //  Engine.main.player.Hit(damage);   
            //}


            if(Physics.CheckLineBox((player.boundingBox.start*accuracy) + player.position, (player.boundingBox.end*accuracy) + player.position, position, position+(forward*1_000_000)))
            {
                Engine.main.player.Hit(damage);  
            }
        }









    }
}
