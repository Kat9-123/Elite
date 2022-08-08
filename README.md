# Elite not very Dangerous | By: Kat9_123
#### A custom 3D Engine and game based on Elite that runs in the console. Written in C#
#### Only for Windows!
## Tabel of contents
- [What is it?](#what-is-it)
- [Screenshots](#screenshots)
- [Why?](#why)
- [How to play it](#how-to-play-it)
- [Controls](#controls)
- [Engine](#engine)
- [Miscellaneous](#miscellaneous)


## What is it?
This is a simple game made in my custom 3D Engine that draws it's graphics to the console.
The game is very loosely based on the original Elite. 
Everything was made by me except for the really low level console interactions.

The game isn't really a complete game. It's more of a techdemo.

### Features
- Uses the Windows command prompt
- 2 Enemies and a Boss with detailed models
- Mouse and Keyboard controls
- Short distance warp ability
- Pew pew!
- It looks relatively good
- Space!
- Full HUD
- Difficulty scaling


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
to do some research. I stumbled upon 
<a href="https://www.youtube.com/watch?v=ih20l3pJoeU">OLC's 3D Rendering Engine</a> tutorial. Though
it was a tutorial for C++ it was pretty easy to convert the core concepts to C#. After a lot of 
tinkering I finally had a functioning renderer (rotation did take some time to get fully functioning).
After that finishing the rest of the Engine architecture (and the game) was relatively simple.



## How to play it
#### For the best experience use the <a href="https://strlen.com/square/">Square</a> font
<i>If you want to use a different font, modify the Font option in assets/Settings.txt</i>


You can either either compile it from source yourself, or download it from the <a href=https://github.com/Kat9-123/Elite/releases>Releases tab</a> just make sure that the assets folder is located in the same directory as where you launch the executable from.

I would suggest you take a look at assets/Settings.txt for things like graphics settings
or mouse settings before playing.



## Controls
- Space / LeftMouseButton - Shoot


### Translation
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
- Q - Roll right
- E - Roll left
####
####
- LeftArrow - Yaw Left
- RightArrow - Yaw Right
- UpArrow - Pitch Up
- DownArrow - Pitch Down
#### Or
- MouseMovementUp - Pitch Up
- MouseMovementDown - Pitch Down
- MouseMovementRight - Yaw Right
- MouseMovementLeft - Yaw Left

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

It also doesn't support audio which I do want to change at some point.

Features:
- Fully written in C#
- Runs in the Windows command prompt
- Keyboard and Mouse support
- 16 colours
- Object oriented architecture (I kinda dislike OOP but whatever)
- Rotation based on forward and up vectors (Is this a feature though?)
- PNG and OBJ support
- Custom font support
- Basic line box collision detection
- Easily accesible settings
- Basic lighting


## Miscellaneous
If you have any tips, optimisations, etc. Please either message me or make a pull request.

The code is still relatively messy so any help with that is also greatly appreciated.

### Perfomance
Sadly the performance really isn't great. It will probably run on most computers
but you will have to tinker a bit with the settings. The best solution for the
performance problem is <s>being a better programmer</s> probably by utilising the GPU,
but I have no idea how to do that...
