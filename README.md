# Elite not very Dangerous | By: Kat9_123
#### A simple 3D game based on Elite that runs in the console. Written in C#
#### Only for Windows!
## Tabel of contents
- [What is it?](#how-to-play-it)
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
- 2 Enemies and a Boss
- Mouse and Keyboard controls
- Short distance warp ability




## Screenshots
### HUD Explanation
![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/HUD.png)

![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/Enemy.png)

![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/Planets.png)

### Boss
![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/Boss.png)

### The Boss has a massive laser attack
![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/BossLaser.png)

### Short distance warp ability
![image](https://github.com/Kat9-123/Elite/blob/master/screenshots/Warp.png)

## Why?
After finishing <a href="https://github.com/Kat9-123/Asteroids">Asteroids</a> 
I thought "What if I made Elite in the console". First I dismissed it as I thought
that I was far to incapable to do something like that. After a while of thinking about it I decided
to do some research. I stumbled upon 
<a href="https://www.youtube.com/watch?v=ih20l3pJoeU">OLC's 3D Rendering Engine</a> tutorial. Though
it was a tutorial for C++ it was pretty easy to convert the core concepts to C#. After a lot of 
tinkering I finally had a functioning renderer (rotation did take me some time to get fully functioning).
After that the rest of the Engine architecture was relatively simple.



## How to play it
#### For the best experience use <a href="https://strlen.com/square/">Square</a>
if you decide to use a different font, modify the Font option in assets/Settings.txt


You can either either compile it from source yourself, or download it from the released tab.
Just make sure that the assets folder is where you launch the executable from.

I would suggest you take a look at assets/Settings.txt for things like graphics settings,
or mouse controls



## Controls
LeftMouseButton/Space - Shoot


### Translation
- W - Forward
- S - Backwards
- A - Left
- D - Right

LShift - Up
Control - Down

C/RightMouseButton - Short distance warp

X - Break


### Rotation
Q - Roll right
E - Roll left


LeftArrow - Yaw Left
RightArrow - Yaw Right
UpArrow - Pitch Up
DownArrow - Pitch Down


MouseMovementUp - Pitch Up
MouseMovementDown - Pitch Down

MouseMovementRight - Yaw Right
MouseMovementLeft - Yaw Left

## Misc
M - Free mouse
Escape - Exit
R - Restart




## Engine
The Engine uses a basic object oriented aproach.

Though the Engine and the game are pretty closely integrated, you could definitely extract the engine
(if you really wanted to for some reason). 

The Engine itself lacks some pretty important features,
mainly true object layering and occlusion. I decided to not implement these feautures because
<s>I was too lazy</s> it wasn' t necessary for this project

Features:
- Fully written in C#
- Runs in the Windows command prompt
- Keyboard and Mouse support
- 16 colours
- Object oriented architecture (I kinda dislike OOP but whatever)
- Rotation based on forward and up vectors
- PNG and OBJ support
- Custom font support
- Basic line box collision detection
- Easily accesible settings





## Miscellaneous
If you have any tips, optimisations, etc. Please either message me or make a pull request.
The code is stil relatively messy.

### Perfomance
Sadly the performance really isn't great. It will probably run on most computers
but you will have to tinker a bit with the settings.
