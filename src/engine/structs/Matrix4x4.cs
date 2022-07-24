using System;

namespace Elite
{
    public struct Matrix4x4
    {
        public float[,] matrix;

        /*
        public Matrix4x4(bool _ = false)
        {
            matrix = new float[4,4];
        }
        */

        public static Matrix4x4 GenerateProjectionMatrix(float fov)
        {
            Matrix4x4 matrix;
            matrix.matrix = new float[4,4];

            float near = 0.05f;
            float far = 1000f;
            float aspectRatio = (float) Settings.SCREEN_SIZE_Y / (float) (Settings.SCREEN_SIZE_X);

            float fovRad = 1.0f/MathF.Tan(fov * 0.5f / 180f * MathF.PI);

            matrix.matrix[0,0] = aspectRatio * fovRad;
            matrix.matrix[1,1] = fovRad;
            matrix.matrix[2,2] = far / (far - near);
            matrix.matrix[3,2] = (-far * near) / (far - near);
            matrix.matrix[2,3] = 1f;
            matrix.matrix[3,3] = 0f;

            return matrix;
        }
        public static Matrix4x4 GenerateZRotationMatrix(float theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.matrix = new float[4,4];
            mat.matrix[0,0] = (float)  Math.Cos(theta);
            mat.matrix[0,1] = (float)  Math.Sin(theta);
            mat.matrix[1,0] = (float) -Math.Sin(theta);
            mat.matrix[1,1] = (float)  Math.Cos(theta);
            mat.matrix[2,2] = 1f;
            mat.matrix[3,3] = 1f;

            return mat;
        }

        public static Matrix4x4 GenerateYRotationMatrix(float theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.matrix = new float[4,4];
            mat.matrix[0,0] =  (float)  Math.Cos(theta);
            mat.matrix[2,0] =  (float)  Math.Sin(theta);
            mat.matrix[0,2] =  (float) -Math.Sin(theta);
            mat.matrix[1,1] =  1f;
            mat.matrix[2,2] =  (float)  Math.Cos(theta);
            mat.matrix[3,3] =  1f;
            return mat;            
        }

        public static Matrix4x4 GenerateXRotationMatrix(float theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.matrix = new float[4,4];
            mat.matrix[0,0] = 1f;
            mat.matrix[1,1] =  MathF.Cos(theta);
            mat.matrix[1,2] =  MathF.Sin(theta);
            mat.matrix[2,1] = -MathF.Sin(theta);
            mat.matrix[2,2] =  (float) Math.Cos(theta);
            mat.matrix[3,3] =  1f;
            return mat;            
        }

        public static Matrix4x4 DirectionToMatrix(Vector3 direction, Vector3 up)
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

        public Matrix4x4 MatrixQuickInverse() // Only for Rotation/Translation Matrices
        {
            Matrix4x4 m = this;
            Matrix4x4 matrix = this;
            matrix.matrix = new float[4,4];
            matrix.matrix[0,0] = m.matrix[0,0]; matrix.matrix[0,1] = m.matrix[1,0]; matrix.matrix[0,2] = m.matrix[2,0]; matrix.matrix[0,3] = 0.0f;
            matrix.matrix[1,0] = m.matrix[0,1]; matrix.matrix[1,1] = m.matrix[1,1]; matrix.matrix[1,2] = m.matrix[2,1]; matrix.matrix[1,3] = 0.0f;
            matrix.matrix[2,0] = m.matrix[0,2]; matrix.matrix[2,1] = m.matrix[1,2]; matrix.matrix[2,2] = m.matrix[2,2]; matrix.matrix[2,3] = 0.0f;
            matrix.matrix[3,0] = -(m.matrix[3,0] * matrix.matrix[0,0] + m.matrix[3,1] * matrix.matrix[1,0] + m.matrix[3,2] * matrix.matrix[2,0]);
            matrix.matrix[3,1] = -(m.matrix[3,0] * matrix.matrix[0,1] + m.matrix[3,1] * matrix.matrix[1,1] + m.matrix[3,2] * matrix.matrix[2,1]);
            matrix.matrix[3,2] = -(m.matrix[3,0] * matrix.matrix[0,2] + m.matrix[3,1] * matrix.matrix[1,2] + m.matrix[3,2] * matrix.matrix[2,2]);
            matrix.matrix[3,3] = 1.0f;
            
            return matrix;
        }
    }
}