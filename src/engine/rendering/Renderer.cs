using System;
using System.Collections.Generic;

namespace Elite
{
    public static class Renderer
    {

        private static List<Line> lines = new List<Line>(4);



        public static void AddLine(Line line)
        {
            lines.Add(line);
        }

        public static void Initialise()
        {
            projectionMatrix = Matrix4x4.GenerateProjectionMatrix();
            ConsoleInterface.Initialise();
        }

        private static ConsoleInterface.CharInfo[] image;
        private static Matrix4x4 projectionMatrix;

        private static string ui = "";


        private const string LUMINACES = "#0OC*+/^,.  ";//"$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";



      //  private static Matrix4x4 cameraRotationMatrixZ;
        //private static Matrix4x4 cameraRotationMatrixY;
        //private static Matrix4x4 cameraRotationMatrixX;



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

            if(w != 0f)
            {
                result.x /= w;
                result.y /= w;
                result.z /= w;
            }


            return result;
        }

        public static ConsoleInterface.CharInfo[] GenerateEmptyBuffer()
        {
            ConsoleInterface.CharInfo[] result = new ConsoleInterface.CharInfo[Settings.SCREEN_SIZE_Y*Settings.SCREEN_SIZE_X];
            for (int y = 0; y < Settings.SCREEN_SIZE_Y; y++)
            {
                for (int x = 0; x < Settings.SCREEN_SIZE_X; x++)
                {
                    result[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = Convert.ToByte(' ');
                }
            }
            return result;
        }




        // Adds vector to all vectors in triangle
        private static Triangle AddVecToTriangle(Triangle tri, Vector3 vec)
        {
            Triangle result;
            result.a = tri.a + vec;
            result.b = tri.b + vec;
            result.c = tri.c + vec;
            return result;

        }


        private static Triangle TranslateTriangle(Triangle triangle,Matrix4x4 rotationMatrix,GameObject obj)
        {
            
            // Apply the objects offset to centre it
            triangle = AddVecToTriangle(triangle,obj.offset);


            // Scale
            triangle.a *= obj.scale;
            triangle.b *= obj.scale;
            triangle.c *= obj.scale;

            // Rotation
            triangle = MultiplyTriangleByMatrix(triangle, rotationMatrix);
        

            

            // Position
            triangle = AddVecToTriangle(triangle,obj.position);






            return triangle;
        }

        private static void RenderTriangle(Triangle triangle,Matrix4x4 rotationMatrix,Matrix4x4 cameraRotationMatrix,GameObject obj)
        {
            Triangle translatedTriangle = TranslateTriangle(triangle,rotationMatrix,obj);
        

            if (!obj.movesWithCamera)
            {
                // Apply inverted cameraposition
                translatedTriangle = AddVecToTriangle(translatedTriangle,Engine.cameraPosition * -1);
                
                // Apply inverted camerarotation
                translatedTriangle = MultiplyTriangleByMatrix(translatedTriangle, cameraRotationMatrix);

            }


            // A triangle gets clipped if any of its points are behind the camera.
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

            // Lighting
            
            char character = obj.character;
            if(obj.getsLit)
            {


               // lightDir += Engine.cameraPosition;
                
                    

                Vector3 lightDir = MultiplyVectorByMatrix(obj.lightingDirection, cameraRotationMatrix);
                lightDir = lightDir.Normalise();

                float dp = normal.Dot(lightDir);
                if (dp > 1) dp = 1f;
                if (dp < -1) dp = -1f;
                
                dp += 1f;
                dp /= 2f;

                character = LUMINACES[(int)(dp*(LUMINACES.Length-1))];
   


                
            }


            // Project
            Triangle projectedTriangle = MultiplyTriangleByMatrix(translatedTriangle,projectionMatrix);




            // Scale everything correctly back to screenspace
            projectedTriangle = AddVecToTriangle(projectedTriangle, new Vector3(1,1,0));

            projectedTriangle.a.x *= 0.5f * Settings.SCREEN_SIZE_X;
            projectedTriangle.a.y *= 0.5f * Settings.SCREEN_SIZE_Y;

            projectedTriangle.b.x *= 0.5f * Settings.SCREEN_SIZE_X;
            projectedTriangle.b.y *= 0.5f * Settings.SCREEN_SIZE_Y;

            projectedTriangle.c.x *= 0.5f * Settings.SCREEN_SIZE_X;
            projectedTriangle.c.y *= 0.5f * Settings.SCREEN_SIZE_Y;



            // Placing the projected triangle onto the buffer
            if(obj.filled)
            {
                DrawFilledTriangle(obj.colour,
                    new Vector2(projectedTriangle.a.x,projectedTriangle.a.y),
                    new Vector2(projectedTriangle.b.x,projectedTriangle.b.y),
                    new Vector2(projectedTriangle.c.x,projectedTriangle.c.y),character
                
                );
                return;
                
            }

       

            DrawLine(obj.colour,(int)projectedTriangle.a.x,(int)projectedTriangle.a.y,(int)projectedTriangle.b.x,(int)projectedTriangle.b.y,character);
            DrawLine(obj.colour,(int)projectedTriangle.b.x,(int)projectedTriangle.b.y,(int)projectedTriangle.c.x,(int)projectedTriangle.c.y,character);
            DrawLine(obj.colour,(int)projectedTriangle.c.x,(int)projectedTriangle.c.y,(int)projectedTriangle.a.x,(int)projectedTriangle.a.y,character);


                    
                    
                    

        }


        public static void Render(List<GameObject> gameObjects)
        {

            image = GenerateEmptyBuffer();
            // Generate camera rotation matrices

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

            


            /*
            Matrix4x4 rot = Matrix4x4.DirectionToMatrix(new Vector3(0,0,1),new Vector3(0,1,0));
            for (int i = 0; i < lines.Count; i++)
            {
                if(!lines[i].visible) continue;
                Triangle tri = new Triangle(lines[i].start,new Vector3(lines[i].start.x,lines[i].start.y,lines[i].start.z+0.001f),lines[i].end);
                RenderTriangle(tri,new Vector3(0,0,0),new Vector3(1,1,1),lines[i].colour,false,false,rot,new Vector3(0,0,0),false,'D',false,true,new Vector3(0,0,0));
            }

            */
            


            image = UI.AddUI(image,ui);
            ui = "";
            ConsoleInterface.Write(image);

        }



        public static void Write(string text)
        {
            ui += text;
        }
    
        public static void WriteLine(string text)
        {
            ui += text + "\n";
        }


        // Pick ya poison
        private static void DrawLine(short colour, int x,int y,int x2, int y2,char character) {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

            if (w<0) dx1 = -1; else if (w>0) dx1 = 1;
            if (h<0) dy1 = -1; else if (h>0) dy1 = 1;
            if (w<0) dx2 = -1; else if (w>0) dx2 = 1;

            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest>shortest)) {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;            
            }
            int numerator = longest >> 1 ;
            if(longest > 1000) return;
            for (int i=0;i<=longest;i++) {
                if (y >= 0 && y < Settings.SCREEN_SIZE_Y)
                {
                    if (x >= 0 && x < Settings.SCREEN_SIZE_X)
                    {
                        image[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = (byte)character;
                        image[y*Settings.SCREEN_SIZE_X + x].Attributes = colour;
                    }
                }
                
                numerator += shortest ;
                if (!(numerator<longest)) {
                    numerator -= longest ;
                    x += dx1;
                    y += dy1;
                } else {
                    x += dx2;
                    y += dy2;
                }
            }
        }


        private static void DrawFilledTriangle(short colour, Vector2 a, Vector2 b, Vector2 c,char character)
        {
            Vector2 pos = new Vector2();
            pos.x = MathF.Min(a.x,MathF.Min(b.x,c.x));
            pos.y = MathF.Min(a.y,MathF.Min(b.y,c.y));


            Vector2 largestSize = new Vector2();

            largestSize.x = MathF.Max(a.x,MathF.Max(b.x,c.x));
            largestSize.y = MathF.Max(a.y,MathF.Max(b.y,c.y));


            Vector2 posEnd = new Vector2();

            pos.x = MathF.Max(0,pos.x);
            pos.y = MathF.Max(0,pos.y);

            posEnd.x = MathF.Min(largestSize.x+pos.x,Settings.SCREEN_SIZE_X);
            posEnd.y = MathF.Min(largestSize.y+pos.y,Settings.SCREEN_SIZE_Y);

            for (int y = (int)pos.y; y < posEnd.y; y++)
            {
                for (int x = (int)pos.x; x < posEnd.x; x++)
                {

                    

                    if(LiesPointWithinTriangle(new Vector2(x,y), a,b,c))
                    {
                        image[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = (byte)character;
                        image[y*Settings.SCREEN_SIZE_X + x].Attributes = colour;
                    }
                }
            }

        }

        
        private static bool LiesPointWithinTriangle(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
        {

            float as_x = point.x-a.x;
            float as_y = point.y-a.y;

            bool s_ab = (b.x-a.x)*as_y-(b.y-a.y)*as_x > 0;

            if((c.x-a.x)*as_y-(c.y-a.y)*as_x > 0 == s_ab) return false;

            if((c.x-b.x)*(point.y-b.y)-(c.y-b.y)*(point.x-b.x) > 0 != s_ab) return false;

            return true;

        }










        /*
        public static Vector3 RotateAroundPoint(Vector3 vec, Vector3 axis, float theta)
        {
            Vector3 result;
            result = vec * (float)Math.Cos(theta) + (axis*vec) * (float)Math.Sin(theta) + axis *(axis*vec)*(1-(float)Math.Cos(theta));
            return result;
        }
        private static Triangle RotateTriangleAroundPoint(Triangle triangle, Vector3 axis, float theta)
        {
            Triangle result = new Triangle();

            result.a = RotateAroundPoint(triangle.a,axis,theta);
            result.b = RotateAroundPoint(triangle.b,axis,theta);
            result.c = RotateAroundPoint(triangle.c,axis,theta);
            return result;
        }
        */





    }


}