using System;

namespace Elite
{
    static class Program
    {
        /*
        public static Quaternion asdf(Vector3 forward, Vector3 up)
        {
            forward = forward.Normalise();

            var vector = forward.Normalise();
            var vector2 = Utils.Cross(up, vector).Normalise();
            var vector3 = Utils.Cross(vector, vector2);
            var m00 = vector2.x;
            var m01 = vector2.y;
            var m02 = vector2.z;
            var m10 = vector3.x;
            var m11 = vector3.y;
            var m12 = vector3.z;
            var m20 = vector.x;
            var m21 = vector.y;
            var m22 = vector.z;

        var num8 = (m00 + m11) + m22;
        var quaternion = new Quaternion();
        if (num8 > 0.0)
        {
            var num = MathF.Sqrt(num8 + 1.0f);
            quaternion.w = num * 0.5f;
            num = 0.5f / num;
            quaternion.x = (m12 - m21) * num;
            quaternion.y = (m20 - m02) * num;
            quaternion.z = (m01 - m10) * num;
            return quaternion;
        }
        if ((m00 >= m11) && (m00 >= m22))
        {
            var num7 = MathF.Sqrt(((1.0f + m00) - m11) - m22);
            var num4 = 0.5f / num7;
            quaternion.x = 0.5f * num7;
            quaternion.y = (m01 + m10) * num4;
            quaternion.z = (m02 + m20) * num4;
            quaternion.w = (m12 - m21) * num4;
            return quaternion;
        }
        if (m11 > m22)
        {
            var num6 = MathF.Sqrt(((1.0f + m11) - m00) - m22);
            var num3 = 0.5f / num6;
            quaternion.x = (m10 + m01) * num3;
            quaternion.y = 0.5f * num6;
            quaternion.z = (m21 + m12) * num3;
            quaternion.w = (m20 - m02) * num3;
            return quaternion;
        }
        var num5 = MathF.Sqrt(((1.0f + m22) - m00) - m11);
        var num2 = 0.5f / num5;
        quaternion.x = (m20 + m02) * num2;
        quaternion.y = (m21 + m12) * num2;
        quaternion.z = 0.5f * num5;
        quaternion.w = (m01 - m10) * num2;
        return quaternion;
        }
        */
        /*
        public static Vector3 ToEulerAngles(Quaternion q)
        {
            Vector3 angles = new();

            // roll (x-axis rotation)
            float sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
            float cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
            angles.z = MathF.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            float sinp = 2 * (q.w * q.y - q.z * q.x);
            if (Math.Abs(sinp) >= 1)
            {
                angles.x = MathF.CopySign(MathF.PI / 2, sinp);
            }
            else
            {
                angles.x = MathF.Asin(sinp);
            }

            // yaw (z-axis rotation)
            float siny_cosp = 2 * (q.w * q.z + q.x * q.y);
            float cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
            angles.y = MathF.Atan2(siny_cosp, cosy_cosp);

            return angles;
        }    
    
    */

        public static Matrix4x4 makeRotationDir(Vector3 direction, Vector3 up)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.matrix = new float[4,4];
            Vector3 xaxis = Utils.Cross(up,direction);
            xaxis = xaxis.Normalise();
         
            Vector3 yaxis = Utils.Cross(direction, xaxis);
            yaxis = yaxis.Normalise();

            mat.matrix[0,0] = xaxis.x;
            mat.matrix[1,0] = yaxis.x;
            mat.matrix[2,0] = direction.x;

            mat.matrix[0,1] = xaxis.y;
            mat.matrix[1,1] = yaxis.y;
            mat.matrix[2,1] = direction.y;

            mat.matrix[0,2] = xaxis.z;
            mat.matrix[1,2] = yaxis.z;
            mat.matrix[2,2] = direction.z;


            return mat;


 



        }

        public static Vector3 RotationToFowardVector(Vector3 rotation)
        {
            Vector3 forward = new Vector3(0,0,1);
            //Z
            forward = new Vector3(
                forward.x * MathF.Cos(rotation.z) - forward.y * MathF.Sin(rotation.z),
                forward.x * MathF.Sin(rotation.z) + forward.y * MathF.Cos(rotation.z),
                forward.z
            );
            //Y
            forward = new Vector3(
                forward.x * MathF.Cos(rotation.y) + forward.z * MathF.Sin(rotation.y),
                forward.y,
                -forward.x * MathF.Sin(rotation.y) + forward.z * MathF.Cos(rotation.y)
            );
            // X
            forward = new Vector3(
                forward.x,
                forward.y * MathF.Cos(rotation.x) - forward.z * MathF.Sin(rotation.x),
                forward.y * MathF.Sin(rotation.x) + forward.z * MathF.Cos(rotation.x)
            );







           // forward.x = MathF.Cos(rotation.x);
            //forward.y = MathF.Cos(rotation.y);
            //forward.z = MathF.Cos(rotation.z);
            /*
            forward.x = MathF.Cos(rotation.x) * MathF.Sin(rotation.y);
            forward.y = -MathF.Sin(rotation.x);
            forward.z = MathF.Cos(rotation.x) * MathF.Cos(rotation.y);
            */
            return forward;
        }



        public static Vector3 ForwardVectorToRotation(Vector3 forward)
        {
            Vector3 rotation = new Vector3(0,0,0);

           // rotation.z = 2 * MathF.Atan((MathF.Sqrt(-(forward.x*forward.x) + forward.x*forward.x + forward.y*forward.y) - forward.y)/(forward.x + forward.x));
            //forward.x = forward.x * MathF.Cos(rotation.z) - forward.y * MathF.Sin(rotation.z);
            //forward.y = forward.x * MathF.Sin(rotation.z) + forward.y * MathF.Cos(rotation.z);
            //forward.z = forward.z;
            /*
            rotation.x = MathF.Asin(-forward.y);
            rotation.y = MathF.Asin(forward.x/MathF.Cos(rotation.x));
            //rotation.z = MathF.Asin(forward.Length());
            */
            return rotation;
        }

        public static void PrintVec(string name, Vector3 vec)
        {
            Console.Write(name + ": (");
            Console.Write(vec.x);
            Console.Write(", ");
            Console.Write(vec.y);
            Console.Write(", ");
            Console.Write(vec.z);
            Console.Write(")\n");
        }

        static Matrix4x4 MakeMatrix(Vector3 X, Vector3 Y )  
        {  
            // make sure that we actually have two unique vectors.


            Matrix4x4 M;  
            M.matrix = new float[4,4];
            Vector3 one = X.Normalise();
            Vector3 two = Utils.Cross(X,Y).Normalise();
            Vector3 three = Utils.Cross(two,X).Normalise();

            /*
            M.matrix[0,0] = one.x; 
            M.matrix[1,0] = one.y;
            M.matrix[2,0] = one.z;

            M.matrix[0,2] = two.x;
            M.matrix[1,2] = two.y;
            M.matrix[2,2] = two.z;

            M.matrix[0,1] = three.x;
            M.matrix[1,1] = three.y;
            M.matrix[2,1] = three.z;
            */




            M.matrix[0,0] = one.x; 
            M.matrix[0,1] = one.y;
            M.matrix[0,2] = one.z;

            M.matrix[2,0] = two.x;
            M.matrix[2,1] = two.y;
            M.matrix[2,2] = two.z;

            M.matrix[1,0] = three.x;
            M.matrix[1,1] = three.y;
            M.matrix[1,2] = three.z;

    

            M.matrix[3,3] = 1f;
            
            return M;
        }


        private static void Main(string[] args)
        {

            FileHandler.Setup();
            FontHandler.LoadFont();
           // Console.ReadLine();
            
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Console.BackgroundColor = (System.ConsoleColor) ((x+y)%16);
                    Console.Write(' ');            
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine();

            Console.ReadLine();
        
            /*
            AudioHandler.Play();

           // Console.Beep(440,5000);
            Console.ReadKey();
            
            Vector3 rotation = new Vector3(0,0.25f*MathF.PI,0);

            Vector3 forward = new Vector3(0.5f,0,0.5f).Normalise();
            Vector3 up = new Vector3(0,1,0);

            Vector3 test = new Vector3(2,48,3);
            Vector3 test2 = new Vector3(2,48,3);


            Matrix4x4 rotZ = Renderer.GenerateZRotationMatrix(rotation.z);
            Matrix4x4 rotY = Renderer.GenerateYRotationMatrix(rotation.y);
            Matrix4x4 rotX = Renderer.GenerateXRotationMatrix(rotation.x);

            test = Renderer.MultiplyVectorByMatrix(test,rotZ);
            test = Renderer.MultiplyVectorByMatrix(test,rotY);
            test = Renderer.MultiplyVectorByMatrix(test,rotX);

            PrintVec("Matrix",test);

            Matrix4x4 ab = makeRotationDir(forward,up);
            test2 = Renderer.MultiplyVectorByMatrix(test2,ab);


            PrintVec("test",test2);
            
            ///Console.ReadKey();

            */

            Engine.Setup();

            Engine.Run();

            Console.ReadKey();
        }
    }
}
