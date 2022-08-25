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

        public void DoExplosion(Enemy origin)
        {
            SoundManager.Play(Sounds.explosion);
            for (int i = 0; i < PARTICLE_COUNT; i++)
            {
                ExplosionParticle particle = particles[i];

                particle.position = origin.position;
                Vector3 momentum = Utils.RandomPositionExcludeCentre(100f,500f) + origin.momentum;



                float size = Utils.RandomFloat(0.4f,1.5f);


                particle.DoExplosion(momentum,new Vector3(size,size,size),Utils.RandomFloat(0.1f,1f),Utils.RandomFloat(150,250));
            }
        }


    }
}
