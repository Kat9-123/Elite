namespace Elite
{
    public class EnemyLaser : GameObject
    {
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

            player = Engine.gameManager.player;

        }

        public override void Start()
        {
            Engine.ChanageIndex(this,Engine.gameManager.enemyLayer); 
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



            if(Physics.CheckLineBox((player.boundingBox.start*accuracy) + player.position, (player.boundingBox.end*accuracy) + player.position, position, position+(forward*1_000_000)))
            {
                Engine.gameManager.player.Hit(damage);  
            }
        }
    }
}
