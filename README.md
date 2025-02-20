# HORSEYTYPE
---
> A morse code typing game in the style of monkeytype made in unity.

## ABOUT
`Unity version 2022.3.18f1`
This is used for personally learning morse code for myself. I might push this to Itch.io some day in the next century.
There are two buttons being the primary key (Default is the dot) and the secondary key (Default is the slash). 
* Total time spent : ~18 hours
* Chatgpt lifelines : 3 untested critical functions
* Themes copied straight from monkeytype.com : 1
* Unity crashes : 2

## How to use
1. Clone / Download the git repo to your computer and look for the folder `BuiltGame`.
2. Run the binary (If you are on Linux) or the executable (If you are on Windows)
3. This game does not save any data yet so make sure to adjust the values in the settings to your liking.

## Settings
These can be configured in game and currently do not save.
### Modes
There are currently three modes to input morse code.
- **Manual Input** 
Holding down the primary or secondary key will let you type in morse code similar to a straight key (The old timey one). 
Make sure to adjust the dash threshold to the speed you are comforatable in inputting.
- **Semi Automatic Input**
The primary key will input a dot (.) while the secondary key will input a dash (-) once you press it. 
Holding it down will not do anything afterwords.
- **Automatic Input**
Holding down the keys will input a string of dots (.) and dashes (-) depending on which key you are holding.
It is designed to mimic an *Iambic Paddle Keyer* mode A (Although I don't have access to one in real life so I'm imitating based on videos of it)
Input timings is based on the dash threshold.

### Dash Threshold
The time in milliseconds set for the dash (-). This is used for being the threshold for dots (.) and dashes (-) in manual input and also being the bases for standard time in semi and automatic inputs.
> **Standard units of time for morse code**
> `1 unit` : `dot (.)`
> `3 units` : `dash (-)`
> `1 unit` : `time between dots and dashes`
> `3 units` : `time between characters`
> `7 units` : `time between words`
> **semi mode** uses the dot and dash
> **auto mode** uses the dot, dash and the time between dots and dashes

### Primary Key
The primary key is used for the main keying for manual input and used for dots (.) in semi and auto modes.

### Secondary Key
The secondary key is used for dashes (-) in semi and auto modes while it has the same function as the primary key in manual mode.
