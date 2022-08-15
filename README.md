# Elite not very Dangerous | By: Kat9_123
#### A custom 3D Engine that draws it's graphics to the Windows console and a game loosely based on Elite. Fully written in C#.
#### Only for Windows!
## Tabel of contents
- [What is it?](#what-is-it)
- [Screenshots](#screenshots)
- [Why?](#why)
- [How to play](#how-to-play)
- [Controls](#controls)
- [Engine](#engine)
- [Miscellaneous](#miscellaneous)


## What is it?
Elite not very Dangerous is a simple game made in my custom 3D Engine that draws it's graphics to the console.
The game is very loosely based on the original Elite. 
Everything was made by me except for the really low level console interactions. Those were all adapted from Stackoverflow

All of the assets were also created by me. The sprites were made in Slate, the models
in Blender, the sound effects in SFXR and the music in MuseScore.

It isn't really a game, it's more like a tech-demo.



### Features
- Uses the Windows command prompt
- 2 Enemies and a Boss with detailed models
- Mouse and Keyboard controls
- Short distance warp ability
- Pew pew!
- Planets
- It looks relatively good
- Space!
- Full HUD
- Difficulty scaling
- Soundeffects and music!
- Score saving


## Screenshots
### HUD Explanation
![image](/screenshots/HUD.png)

### Planets
![image](/screenshots/Planets.png)

### Enemy
![image](/screenshots/Enemy.png)

### Boss
![image](/screenshots/Boss.png)

### The Boss has a massive laser attack
![image](/screenshots/BossLaser.png)

### Short distance warp ability
![image](/screenshots/Warp.png)

## Why?
After I finished making <a href="https://github.com/Kat9-123/Asteroids"> an Asteroids clone</a> for the Windows console
I thought "What if I now made Elite in the console". First I dismissed it as I thought
that I was far to incapable to do something like that. After a while of thinking about it I decided
to do some research. I quickly stumbled upon 
<a href="https://www.youtube.com/watch?v=ih20l3pJoeU">OLC's 3D Rendering Engine</a> tutorial. Though
it was a tutorial for C++ it was pretty easy to convert the core concepts to C#. After a lot of 
tinkering I finally had a functioning renderer (rotation did take some time to get fully functioning).
After that finishing the rest of the engine and game was relatively simple.

It was a very fun, challenging and educational project and I'm very happy with the result.



## How to play
#### For the best experience use the <a href="https://strlen.com/square/">Square</a> font
<i>If you want to use a different font, modify the Font option in assets/Settings.txt</i>


You can either compile it from source yourself, or download it from the <a href=https://github.com/Kat9-123/Elite/releases>Releases tab</a> just make sure that the assets folder is located in the same directory as where you launch the executable from.

I  suggest you take a look at assets/Settings.txt for things like graphics
or mouse settings before playing.



## Controls
- Space / LeftMouseButton - Shoot


### Movement
- W - Forward
- S - Backwards
- A - Left
- D - Right
####
- LShift - Up
- LControl - Down
####
- X - Break
####
- C / RightMouseButton - Short distance warp



### Rotation
- Q - Roll left
- E - Roll right
####
####
- LeftArrow - Yaw Left
- RightArrow - Yaw Right
- UpArrow - Pitch Up
- DownArrow - Pitch Down
#### Or
- MouseMovementLeft - Yaw Left
- MouseMovementRight - Yaw Right
- MouseMovementUp - Pitch Up
- MouseMovementDown - Pitch Down

### Misc
- M - Free mouse
- Escape - Exit
- R - Restart




## Engine
The Engine uses a basic object oriented approach.

Though the Engine and the game are pretty closely integrated, you could definitely extract the engine
(if you really wanted to for some reason). 

The Engine itself lacks some pretty important features,
mainly true object layering and occlusion. I decided not to implement these feautures because
<s>I was too lazy</s> it wasn't necessary for this project and because it would cause unnecessary lag.
Actual frustum culling would be nice though...


Features:
- Fully written in C#
- Runs in the Windows command prompt
- Keyboard and Mouse support
- 16 colours
- Object oriented architecture
- PNG and OBJ support
- Custom font support
- Basic line box collision detection
- Easily accesible settings
- Basic lighting
- Sound support
- Basic LOD support

Quirks:
- Rotation based on forward and up vectors



## Miscellaneous
If you have any tips, optimisations, balance changes, etc. Please either message me or make a pull request.

The code is still relatively messy so any help with that is also greatly appreciated. I got 
kind of burned out so the newer features, like audio and LOD, are especially messy.

### Perfomance
Sadly the performance really isn't great. It will probably run on most computers
but you will have to tinker a bit with the settings. The biggest cause for the
performance problem is the fact that all graphics get drawn to the console, which is
kinda sad.
