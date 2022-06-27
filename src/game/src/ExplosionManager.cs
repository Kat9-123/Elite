using System;

namespace Elite
{
    public class ExplosionManager : GameObject
    {
        private const short PARTICLE_COUNT = 40;

        private ExplosionParticle[] particles = new ExplosionParticle[PARTICLE_COUNT];
    
        public override void Start()
        {
            visible = false;
            for (int i = 0; i < PARTICLE_COUNT; i++)
            {
                particles[i] = (ExplosionParticle) Engine.Instance(new ExplosionParticle());
            }
        }

        public override void Update(float deltaTime)
        {
            
        }


        public void DoExplosion(Enemy origin)
        {
            for (int i = 0; i < PARTICLE_COUNT; i++)
            {
                ExplosionParticle particle = particles[i];

                particle.position = origin.position;
                Vector3 momentum = new Vector3();
                momentum.x = Utils.RandomFloat(100f,500f) * Utils.RandomSign();
                momentum.y = Utils.RandomFloat(100f,500f)* Utils.RandomSign();
                momentum.z = Utils.RandomFloat(100f,500f)* Utils.RandomSign();


                float size = Utils.RandomFloat(0.4f,1.5f);


                particle.DoExplosion(momentum,new Vector3(size,size,size),Utils.RandomFloat(0.1f,1f),Utils.RandomFloat(150,250));
            }
        }


    }
}
