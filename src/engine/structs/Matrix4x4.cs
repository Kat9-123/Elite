using System;

namespace Elite
{
    public struct Matrix4x4
    {
        public float[,] matrix;


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

        public static Matrix4x4 DirectionToMatrix(Vector3 direction, Vector3 up)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.matrix = new float[4,4];
            Vector3 xAxis = Utils.Cross(up,direction);
            xAxis = xAxis.Normalise();
         
            Vector3 yAxis = Utils.Cross(direction, xAxis);
            yAxis = yAxis.Normalise();

            mat.matrix[0,0] = xAxis.x;
            mat.matrix[1,0] = yAxis.x;
            mat.matrix[2,0] = direction.x;

            mat.matrix[0,1] = xAxis.y;
            mat.matrix[1,1] = yAxis.y;
            mat.matrix[2,1] = direction.y;

            mat.matrix[0,2] = xAxis.z;
            mat.matrix[1,2] = yAxis.z;
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