using System;
using System.Collections;
using System.Text;
using Tao.OpenGl;

namespace LabDarbas_4
{
    class Pradzia
    {
        static float normalX = 0;
        static float normalY = 0;
        static float normalZ = 0;

        static float lightX = 1;
        static float lightY = 1;
        static float lightZ = 0;

        static float angleX = 45;
        static float angleY = 0;
        static float angleZ = 0;

        static int lightModel;


        static void Main(string[] args)
        {
            int w = 400;
            int h = 400;

            Glut.glutInit();
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Glut.glutInitDisplayMode(Glut.GLUT_DEPTH | Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(w, h);
            Glut.glutInitWindowPosition(400, 300);
            Glut.glutCreateWindow("Pradzia");
            Init();
            Glut.glutDisplayFunc(new Glut.DisplayCallback(Vaizdavimas));
            Glut.glutSpecialFunc(new Glut.SpecialCallback(Klaviatura));
            Glut.glutCreateMenu(new Glut.CreateMenuCallback(Menu));
            Glut.glutReshapeFunc(new Glut.ReshapeCallback(Perpiesti));
            Glut.glutAddMenuEntry("Difuse", 1);
            Glut.glutAddMenuEntry("Specular", 2);
            Glut.glutAddMenuEntry("Ambient", 3);
            Glut.glutAddMenuEntry("Du sviesos saltiniai", 4);
            Glut.glutAddMenuEntry("+45 laipsniai X", 5);
            Glut.glutAddMenuEntry("-45 laipsniai X", 6);
            Glut.glutAddMenuEntry("Pabaiga", 9);
            Glut.glutAttachMenu(Glut.GLUT_RIGHT_BUTTON);
            Glut.glutMainLoop();
        }

        static void Init()
        {
            Gl.glClearColor(0.6f, 0.6f, 1.0f, 1.0f);
            Gl.glColor3f(102 / 256f, 100 / 256f, 256 / 256f);
            Gl.glEnable(Gl.GL_LIGHTING);
            float[] light_position = { lightX, lightY, lightZ, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_position);
            float[] specularColor = { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, specularColor);
            Gl.glEnable(Gl.GL_LIGHT0);
            float[] colorBlue = { 0.0f, 1f, 1f, 1.0f };
            float[] colorBlue1 = { 0.0f, 1f, 1f, 1.0f };
        }

        static void Perpiesti(int w, int h)
        {
        }

        static void Vaizdavimas()
        {
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Glut.glutInitDisplayMode(Glut.GLUT_DEPTH | Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            float[] light_position = { lightX, lightY, lightZ, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_position);
            float[] color = { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, lightModel, color);
            float[] light_position1 = { -0.85f, 0.65f, -1.35f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, light_position1);
            float[] specularColor1 = { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glLightfv(Gl.GL_LIGHT2, lightModel, specularColor1);
            float[] colorBlue = { 0.0f, 0.1f, 1f, 1.0f };
            Gl.glMaterialfv(Gl.GL_FRONT, lightModel, colorBlue);
            Gl.glPointSize(4.0f);
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex3f(lightX, lightY, lightZ);
            Gl.glEnd();
            Gl.glLoadIdentity();
            Gl.glRotated(angleX, 1.0, 0.0, 0.0);
            Gl.glRotated(angleY, 0.0, 1.0, 0.0);
            Gl.glRotated(angleZ, 0.0, 0.0, 1.0);
            Piesk();
            Gl.glFlush();
        }

        static void Klaviatura(int ch, int x, int y)
        {
            switch (ch)
            {
                case Glut.GLUT_KEY_RIGHT:
                    lightX += 0.05f;
                    break;
                case Glut.GLUT_KEY_LEFT:
                    lightX -= 0.05f;
                    break;
                case Glut.GLUT_KEY_UP:
                    lightY += 0.05f;
                    break;
                case Glut.GLUT_KEY_DOWN:
                    lightY -= 0.05f;
                    break;
                case Glut.GLUT_KEY_PAGE_UP:
                    lightZ += 0.05f;
                    break;
                case Glut.GLUT_KEY_PAGE_DOWN:
                    lightZ -= 0.05f;
                    break;
            }
            Glut.glutPostRedisplay();
        }

        static void Menu(int i)
        {
            switch (i)
            {
                case 2:
                    //Specular light  
                    lightModel = Gl.GL_SPECULAR;
                    Glut.glutPostRedisplay();
                    break;
                case 1:
                    lightModel = Gl.GL_DIFFUSE;
                    Glut.glutPostRedisplay();
                    break;
                case 3:
                    //Ambient light
                    lightModel = Gl.GL_AMBIENT;
                    Glut.glutPostRedisplay();
                    break;
                case 4:

                    //Du sviesos saltiniai
                    Gl.glEnable(Gl.GL_LIGHT2);
                    Glut.glutPostRedisplay();
                    break;
                case 5:
                    //+45 laipsniai X
                    angleX += 45f;
                    break;

                case 6:
                    //+45 laipsniai X
                    angleX -= 45f;
                    break;
                case 9:
                    //EXIT  
                    Environment.Exit(0);
                    break;
            }
            Glut.glutPostRedisplay();
        }

        static void CalculateNormal(float[] mas)
        {
            int virsSk = mas.Length / 3;
            normalX = 0;
            normalY = 0;
            normalZ = 0;
            for (int i = 1; i <= virsSk - 1; i++)
            {
                //normalX=============================
                normalX = normalX + (mas[i * 3 - 2] - mas[i * 3 - 2 + 3]) * (mas[i * 3 - 1] + mas[i * 3 - 1 + 3]);
                //normalY=============================
                normalY = normalY + (mas[i * 3 - 1] - mas[i * 3 - 1 + 3]) * (mas[i * 3 - 3] + mas[i * 3 - 3 + 3]);
                //normalZ==============================
                normalZ = normalZ + (mas[i * 3 - 3] - mas[i * 3 - 3 + 3]) * (mas[i * 3 - 2] + mas[i * 3 - 2 + 3]);
            }

            normalX = normalX + (mas[virsSk * 3 - 2] - mas[2 - 1]) * (mas[virsSk * 3 - 1] + mas[3 - 1]);

            normalY = normalY + (mas[virsSk * 3 - 1] - mas[3 - 1]) * (mas[virsSk * 3 - 3] + mas[3 - 3]);

            normalZ = normalZ + (mas[virsSk * 3 - 3] - mas[3 - 3]) * (mas[virsSk * 3 - 2] + mas[2 - 1]);

            float d = (float)Math.Sqrt(normalX * normalX + normalY * normalY + normalZ * normalZ);
            normalX /= d;
            normalY /= d;
            normalZ /= d;
        }

        static void Piesk()//iškvieciama iš vaizdavimo i ekrana metodo
        {
            Gl.glColor3f(1f, 0f, 0f);
            KetvirtisS();
            Gl.glRotated(90, 0.0, 0.0, 1.0);
            Gl.glColor3f(1f, 0f, 0f);
            KetvirtisS();
            Gl.glRotated(90, 0.0, 0.0, 1.0);
            Gl.glColor3f(1f, 0f, 0f);
            KetvirtisS();
            Gl.glRotated(90, 0.0, 0.0, 1.0);
            Gl.glColor3f(1f, 0f, 0f);
            KetvirtisS();
            Gl.glRotated(90, 0.0, 0.0, 1.0);
        }

        static void KetvirtisS()
        {
            float[] Ib = {        0.0f, -0.4f, 0.0f,
                        0.0f, -0.2f, -0.1f,
                                  0.2f, -0.5f, 0.0f,
                         };
            float[] IIb = {       0.2f, -0.5f, 0.0f,
                        0.0f, -0.2f, -0.1f,
                                  0.2f, -0.3f, 0.0f,
                          };
            float[] IIIb = {      0.0f, -0.2f, -0.1f,
                        0.2f, 0.0f, -0.1f,
                             0.3f, -0.2f, 0.0f,
                        0.2f, -0.3f, 0.0f,
                           };
            float[] IVb = {        0.2f, 0.0f, -0.1f,
                         0.5f, -0.2f, 0.0f,
                                   0.3f, -0.2f, 0.0f,
                          };
            float[] Vb = {         0.2f, 0.0f, -0.1f,
                         0.4f, 0.0f, 0.0f,
                                   0.5f, -0.2f, 0.0f,
                         };
            float[] It = {        0.0f, -0.4f, 0.3f,
                        0.2f, -0.5f, 0.3f,
                        0.0f, -0.2f, 0.4f,
                         };
            float[] IIt = {       0.2f, -0.5f, 0.3f,
                        0.2f, -0.3f, 0.3f,
                        0.0f, -0.2f, 0.4f,
                          };
            float[] IIIt = {      0.0f, -0.2f, 0.4f,
                        0.2f, -0.3f, 0.3f,
                        0.3f, -0.2f, 0.3f,
                        0.2f, 0.0f, 0.4f
                           };
            float[] IVt = {       0.2f, 0.0f, 0.4f,
                        0.3f, -0.2f, 0.3f,
                        0.5f, -0.2f, 0.3f,
                          };
            float[] Vt = {        0.2f, 0.0f, 0.4f,
                        0.5f, -0.2f, 0.3f,
                        0.4f, 0.0f, 0.3f,

                         };
            float[] Is = {        0.0f, -0.4f, 0.0f,
                                  0.2f, -0.5f, 0.0f,
                                  0.2f, -0.5f, 0.3f,
                                  0.0f, -0.4f, 0.3f,
                         };
            float[] IIs = {       0.2f, -0.5f, 0.0f,
                                  0.2f, -0.3f, 0.0f,
                                  0.2f, -0.3f, 0.3f,
                                  0.2f, -0.5f, 0.3f,
                          };
            float[] IIIs = {      0.2f, -0.3f, 0.0f,
                                  0.3f, -0.2f, 0.0f,
                                  0.3f, -0.2f, 0.3f,
                                  0.2f, -0.3f, 0.3f,
                          };
            float[] IVs = {       0.3f, -0.2f, 0.0f,
                                  0.5f, -0.2f, 0.0f,
                                  0.5f, -0.2f, 0.3f,
                                  0.3f, -0.2f, 0.3f,
                           };
            float[] Vs = {        0.5f, -0.2f, 0.0f,
                                  0.4f, 0.0f, 0.0f,
                                  0.4f, 0.0f, 0.3f,
                                  0.5f, -0.2f, 0.3f,
                           };
            float[] VIs = {       0.2f, 0.0f, -0.1f,
                                  0.0f, -0.2f, -0.1f,
                                  0.0f, -0.2f, 0.4f,
                                  0.2f, 0.0f, 0.4f,
                           };

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glLineWidth(2);

            CalculateNormal(Ib);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Ib);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIb);
            CalculateNormal(IIb);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIIb);
            CalculateNormal(IIIb);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IVb);
            CalculateNormal(IVb);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Vb);
            CalculateNormal(Vb);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, It);
            CalculateNormal(It);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIt);
            CalculateNormal(IIt);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);


            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIIt);
            CalculateNormal(IIIt);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IVt);
            CalculateNormal(IVt);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Vt);
            CalculateNormal(Vt);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Is);
            CalculateNormal(Is);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIs);
            CalculateNormal(IIs);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IIIs);
            CalculateNormal(IIIs);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IVs);
            CalculateNormal(IVs);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, Vs);
            CalculateNormal(Vs);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, VIs);
            CalculateNormal(VIs);
            Gl.glNormal3f(normalX, normalY, normalZ);
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glFlush();
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY | Gl.GL_COLOR_ARRAY);
        }
    }
}

