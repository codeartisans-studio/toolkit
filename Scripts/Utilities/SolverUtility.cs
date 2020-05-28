using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    //https://ww2.mathworks.cn/help/simulink/ug/choose-a-solver.html
    public static class SolverUtility
    {
        // Fixed-step
        // ode4(Runge-Kutta)
        // Runge-Kutta法（Runge-Kutta methods）
        // https://en.wikipedia.org/wiki/Runge%E2%80%93Kutta_methods
        // 例1（百度百科中例子）：
        // https://baike.baidu.com/item/%E9%BE%99%E6%A0%BC%E5%BA%93%E5%A1%94%E6%B3%95/3016350?fr=aladdin
        // yo += dt * (-1 * xn * yo * yo);// 表示正常运算
        // yn = IntegrationUtility.RK4(xn, yn, dt, (t, y) => (-1 * t * y * y));
        // xn += dt;
        // 例2（游戏开发物理学中第7章例子）：
        // yo += dt * ((T - (C * yn)) / M);// 表示正常运算
        // yn = IntegrationUtility.RK4(xn, yn, dt, (t, y) => (((T - (C * y)) / M)));
        // xn += dt
        public static double Ode4(double t, double y, double h, Func<double, double, double> f)
        {
            double k1, k2, k3, k4;

            k1 = f(t, y);
            k2 = f(t + h / 2, y + k1 * h / 2);
            k3 = f(t + h / 2, y + k2 * h / 2);
            k4 = f(t + h, y + h * k3);

            return y + h * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }

        public static double Ode4(float t, float y, float h, Func<float, float, float> f)
        {
            float k1, k2, k3, k4;

            k1 = f(t, y);
            k2 = f(t + h / 2, y + k1 * h / 2);
            k3 = f(t + h / 2, y + k2 * h / 2);
            k4 = f(t + h, y + h * k3);

            return y + h * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }

        public static Vector3 Ode4(float t, Vector3 y, float h, Func<float, Vector3, Vector3> f)
        {
            Vector3 k1, k2, k3, k4;

            k1 = f(t, y);
            k2 = f(t + h / 2, y + k1 * h / 2);
            k3 = f(t + h / 2, y + k2 * h / 2);
            k4 = f(t + h, y + h * k3);

            return y + h * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }

        public static Vector2 Ode4(float t, Vector2 y, float h, Func<float, Vector2, Vector2> f)
        {
            Vector2 k1, k2, k3, k4;

            k1 = f(t, y);
            k2 = f(t + h / 2, y + k1 * h / 2);
            k3 = f(t + h / 2, y + k2 * h / 2);
            k4 = f(t + h, y + h * k3);

            return y + h * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }

        // Fixxed-step
        // ode1(Euler)
        // Euler法(Euler method)
        // https://en.wikipedia.org/wiki/Euler_method
        public static double Ode1(double t, double y, double h, Func<double, double, double> f)
        {
            double k;

            k = f(t, y);

            return y + h * k;
        }

        public static double Ode1(float t, float y, float h, Func<float, float, float> f)
        {
            float k;

            k = f(t, y);

            return y + h * k;
        }

        public static Vector3 Ode1(float t, Vector3 y, float h, Func<float, Vector3, Vector3> f)
        {
            Vector3 k;

            k = f(t, y);

            return y + h * k;
        }

        public static Vector2 Ode1(float t, Vector2 y, float h, Func<float, Vector2, Vector2> f)
        {
            Vector2 k;

            k = f(t, y);

            return y + h * k;
        }
    }
}