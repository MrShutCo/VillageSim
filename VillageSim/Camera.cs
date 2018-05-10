using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim {
    public class Camera {
        public static Matrix viewMatrix;

        private static Vector2 m_position;
        // Constant for where the center of the screen is, is hardcoded for now
        private static Vector2 m_halfViewSize = new Vector2(25 * 16, 15 * 16);

        // Controls where the camera is centered. This gets set by where the player is
        public static Vector2 Pos
        {
            get {
                return m_position;
            }

            set {
                m_position = value;
                UpdateViewMatrix();
            }
        }

        // UpdateViewMatrix():
        // Updates the camera to the proper position given by m_position.
        // Called every update from GameState
        private static void UpdateViewMatrix() {
            viewMatrix = Matrix.CreateTranslation(m_halfViewSize.X - m_position.X, m_halfViewSize.Y - m_position.Y, 0.0f);
        }
    }
}
