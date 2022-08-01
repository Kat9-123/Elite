namespace Elite
{
    public enum InputMap
    {
        MOVE_LEFT = 0x41, // A //
        MOVE_RIGHT = 0x44, // D
        MOVE_FORWARD = 0x57, // W
        MOVE_BACK = 0x53, // S

        MOVE_UP = 0x10, // SPACE
        MOVE_DOWN = 0x11, // CTRL

        YAW_LEFT = 0x25, // LEFT ARROW
        YAW_RIGHT = 0x27, // RIGHT ARROW

        PITH_UP = 0x26, // UP ARROW
        PITCH_DOWN = 0x28, // DOWN ARROW
        
        ROLL_LEFT = 0x51, // Q
        ROLL_RIGHT = 0x45, // E

        STOP = 0x58, // X

        SHOOT = 0x20, // SPACE


        TARGET = 0x54, //T
        TARGET_MOUSE = 0x04, // MIDDLE MOUSE BUTTON


        PAUSE = 0x1B, // ESC

        RESTART = 0x52, //R


        ZOOM = 0x46, //F

        SHOOT_MOUSE = 0x1, // LEFT MOUSE BUTTON
        BLINK = 0x43, // C
        BLINK_MOUSE = 0x2, // RIGHT MOUSE BUTTON


        FREE_MOUSE = 0x4D // M



    }


}