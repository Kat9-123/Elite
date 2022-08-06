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
This is a 3D engine and a simple game made. very loosely based on the original Elite. 
Everything was made by me except for the really low level console interactions.



## Screenshots


## Why?
After finishing <a href="https://github.com/Kat9-123/Asteroids">Asteroids</a> 
I thought "What if I make another game in the console, but in 3D". First I dismissed it as I thought
that I was far to incapable to do something like that. After a while of thinking about it I decided
to do some research. I stumbled upon 
<a href="https://www.youtube.com/watch?v=ih20l3pJoeU">OLC's 3D Rendering Engine</a> tutorial. Though
it was a tutorial for C++ it was pretty easy to convert the core concepts to C#. After a lot of 
tinkering I finally had a functioning renderer (rotation did take me some time to get fully functioning).
After that the rest of the Engine architecture was relatively simple. 


## How to play it
You can either either compile it from source yourself, or download it from the released tab.
Just make sure that the assets folder is where you launch the executable from

## Controls

### Translation
W - Forward
S - Backwards
A - Left
D - Right

LShift - Up
Control - Down



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
If you have any tips, optimisations, etc. Please either message me or make a contribution.
