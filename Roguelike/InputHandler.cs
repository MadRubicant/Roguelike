using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Roguelike {
    static class InputHandler {
        static KeyboardState PreviousKeyboard;
        static KeyboardState CurrentKeyboard;
        static MouseState PreviousMouse;
        static MouseState CurrentMouse;

        static InputHandler() {
            PreviousKeyboard = new KeyboardState();
            CurrentKeyboard = new KeyboardState();
            PreviousMouse = new MouseState();
            CurrentMouse = new MouseState();
        }

        public static void GetInput() {
            PreviousKeyboard = CurrentKeyboard;
            PreviousMouse = CurrentMouse;
            CurrentKeyboard = Keyboard.GetState();
            CurrentMouse = Mouse.GetState();
        }

        /// <summary>
        /// Returns whether the given key was pressed this frame
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool ButtonPressed(Keys Key) {
            return CurrentKeyboard.IsKeyDown(Key) && PreviousKeyboard.IsKeyUp(Key);
        }

        /// <summary>
        /// Returns whether the given key is being held this frame
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool ButtonHeld(Keys Key) {
            return CurrentKeyboard.IsKeyDown(Key) && PreviousKeyboard.IsKeyDown(Key);
        }

        /// <summary>
        /// Returns whether the given key was released this frame
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool ButtonReleased(Keys Key) {
            return CurrentKeyboard.IsKeyUp(Key) && PreviousKeyboard.IsKeyDown(Key);
            
        }

        /// <summary>
        /// Returns whether the given mouse button was pressed this frame
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool MouseButtonPressed(MouseButton Button) {
            return MouseClickType(Button, ButtonPressed);
        }

        /// <summary>
        /// Returns whether the given mouse button is being held this frame
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool MouseButtonHeld(MouseButton Button) {
            return MouseClickType(Button, ButtonHeld);
        }

        /// <summary>
        /// Returns whether the given mouse button was released this frame
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool MouseButtonReleased(MouseButton Button) {
            return MouseClickType(Button, ButtonReleased);
        }

        private static bool MouseClickType(MouseButton Button, ButtonHandler ClickType) {
            switch (Button) {
                case MouseButton.Left:
                    return ClickType(CurrentMouse.LeftButton, PreviousMouse.LeftButton);
                case MouseButton.Middle:
                    return ClickType(CurrentMouse.MiddleButton, PreviousMouse.MiddleButton);
                case MouseButton.Right:
                    return ClickType(CurrentMouse.RightButton, PreviousMouse.RightButton);
                default:
                    throw new ArgumentException("Argument Button was passed an invalid value");
            }
        }
        private delegate bool ButtonHandler(ButtonState Current, ButtonState Previous);
        private static bool ButtonPressed(ButtonState Current, ButtonState Previous) {
            return Current == ButtonState.Pressed && Previous == ButtonState.Released;
        }

        private static bool ButtonHeld(ButtonState Current, ButtonState Previous) {
            return Current == ButtonState.Pressed && Previous == ButtonState.Released;
        }

        private static bool ButtonReleased(ButtonState Current, ButtonState Previous) {
            return Current == ButtonState.Released && Previous == ButtonState.Pressed;
        }
    }
}
