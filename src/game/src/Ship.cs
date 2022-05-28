using System;


namespace Elite
{   

    public class Ship : GameObject
    {

        public float currentShield = 200f;
        public float currentHull;


        protected float maxSpeed = 7.5f;

        protected float speed = 0f;


        public void ApplyMovement(Vector3 forward, float deltaTime)
        {

            position += forward * speed * deltaTime;
 
            //momentum -= movement/200f;
    
            /*
            if(maxSpeed != -1)
            {
                momentum = momentum.Clamp(maxSpeed);
            }
            */

            
        }
    }
}