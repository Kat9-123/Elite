// This renderer is based on OLC's C++ renderer.
// Any optimisation tips are appreciated!
using System;
using System.Collections.Generic;

namespace Elite
{
    public static class Renderer
    {


        private static Matrix4x4 projectionMatrix;



        

        public static void SetProjectionMatrix(float fov)
        {
            projectionMatrix = Matrix4x4.GenerateProjectionMatrix(fov);
        }


        public static void Initialise()
        {
            SetProjectionMatrix(Settings.FOV);
            ConsoleInterface.Initialise();
        }



        // I decided to not use operator overloads for these functions, because
        // they are slightly less efficient than function-calls (citation needed)
        private static Triangle MultiplyTriangleByMatrix(Triangle triangle, Matrix4x4 mat)
        {
            Triangle result;

            result.a = MultiplyVectorByMatrix(triangle.a,mat);
            result.b = MultiplyVectorByMatrix(triangle.b,mat);
            result.c = MultiplyVectorByMatrix(triangle.c,mat);

            return result;
        }

        public static Vector3 MultiplyVectorByMatrix(Vector3 vec, Matrix4x4 mat)
        {
            Vector3 result = new Vector3();


            result.x = vec.x * mat.matrix[0,0] + vec.y * mat.matrix[1,0] + vec.z * mat.matrix[2,0] + mat.matrix[3,0];
            result.y = vec.x * mat.matrix[0,1] + vec.y * mat.matrix[1,1] + vec.z * mat.matrix[2,1] + mat.matrix[3,1];
            result.z = vec.x * mat.matrix[0,2] + vec.y * mat.matrix[1,2] + vec.z * mat.matrix[2,2] + mat.matrix[3,2];
            float w =  vec.x * mat.matrix[0,3] + vec.y * mat.matrix[1,3] + vec.z * mat.matrix[2,3] + mat.matrix[3,3];

            if(w != 0f) result /= w;


            return result;
        }







        // Apply the translations of the current gameobject to the current triangle
        private static Triangle TranslateTriangle(Triangle triangle,Matrix4x4 rotationMatrix,GameObject obj)
        {
            
            // Apply the objects offset to centre it. Legacy
            triangle += obj.offset;


            // Scale
            triangle.a *= obj.scale;
            triangle.b *= obj.scale;
            triangle.c *= obj.scale;

            // Rotation
            triangle = MultiplyTriangleByMatrix(triangle, rotationMatrix);
        

            // Position
            triangle += obj.position;



            return triangle;
        }

        private static void RenderTriangle(
            Triangle triangle,
            Matrix4x4 rotationMatrix,Matrix4x4 cameraRotationMatrix,
            GameObject obj)
        {
           
            Triangle translatedTriangle = TranslateTriangle(triangle,rotationMatrix,obj);
        

            // movesWithCamera is a slightly hacky flag that decides if the camera
            // translations gets applied to the triangle. Used for some UI elements.
            if (!obj.movesWithCamera)
            {
                // Apply inverted cameraPosition
                translatedTriangle += Engine.cameraPosition * -1;
                
                // Apply inverted cameraRotation
                translatedTriangle = MultiplyTriangleByMatrix(translatedTriangle, cameraRotationMatrix);

            }


            // A triangle gets clipped if any of its points are behind the camera.
            // This will clip triangles that are visible, but just very close to the camera.
            // This is fine because it's very unlikely for objects to be that close to the camera
            // in this game
            if (obj.getsClipped && ((translatedTriangle.a.z < 0) || (translatedTriangle.b.z < 0) || (translatedTriangle.c.z < 0))) return;


    




            // Calculate normal
            Vector3 normal, line1, line2;

            normal = new Vector3();

            line1 = translatedTriangle.b - translatedTriangle.a;
            line2 = translatedTriangle.c - translatedTriangle.a;


            normal.x = line1.y * line2.z - line1.z * line2.y;
            normal.y = line1.z * line2.x - line1.x * line2.z;
            normal.z = line1.x * line2.y - line1.y * line2.x;

            normal = normal.Normalise();

            // Check if triangle is facing away from the camera
            if (obj.getsCulled) 
            {
                if(normal.Dot(translatedTriangle.a) >= 0) return;
            }

            // Calculate lighting. Default character is the objects character
            char character = obj.character;
            if(obj.getsLit)
            {

                // Apply cameraRotation to the lighting direction
                Vector3 lightDir = MultiplyVectorByMatrix(obj.lightingDirection, cameraRotationMatrix);
                lightDir = lightDir.Normalise();

                float intensity = normal.Dot(lightDir);

                if (intensity > 1) intensity = 1f;
                if (intensity < -1) intensity = -1f;
                
                // Scale the value from [-1 to 1] to [0 to 1]
                intensity += 1f;
                intensity /= 2f;


                character = obj.luminances[(int)(intensity*(obj.luminances.Length-1))];
                   
            }

            Triangle projectedTriangle;

            if(!obj.is2D)
            {
                // Project to 3D
                projectedTriangle = MultiplyTriangleByMatrix(translatedTriangle,projectionMatrix);



                // Scale everything to screenspace
                projectedTriangle += new Vector3(1,1,0);

                projectedTriangle.a.x *= Settings.SCREEN_SIZE_X/2;
                projectedTriangle.a.y *= Settings.SCREEN_SIZE_Y/2;

                projectedTriangle.b.x *= Settings.SCREEN_SIZE_X/2;
                projectedTriangle.b.y *= Settings.SCREEN_SIZE_Y/2;
                projectedTriangle.c.x *= Settings.SCREEN_SIZE_X/2;
                projectedTriangle.c.y *= Settings.SCREEN_SIZE_Y/2;

            }
            else //2D 
            {
                projectedTriangle = translatedTriangle;

            }



 
            if(obj.filled)
            {
                Rasteriser.DrawFilledTriangle(projectedTriangle,character,obj.colour);
                return;
            }

            Rasteriser.DrawTriangle(projectedTriangle,character,obj.colour);
          

        }

        

        public static void Render(List<GameObject> gameObjects)
        {


            Rasteriser.Reset();

            // Generate camera rotation matrix
            Matrix4x4 cameraRotationMatrix = Matrix4x4.DirectionToMatrix(Engine.cameraForward,Engine.cameraUp).MatrixQuickInverse();


            for (int gameObject = 0; gameObject < gameObjects.Count; gameObject++)
            {
            
                GameObject obj = gameObjects[gameObject];
                if (!obj.visible) continue;

                Triangle[] tris = obj.mesh.tris;

                Matrix4x4 rotationMatrix = Matrix4x4.DirectionToMatrix(obj.forward,obj.up);


                for (int i = 0; i < tris.Length; i++)
                {
                    RenderTriangle(tris[i],rotationMatrix,cameraRotationMatrix,obj);
                }                
            }


            Rasteriser.DrawBufferToScreen();

        }


    }

}