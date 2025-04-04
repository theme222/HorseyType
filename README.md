# HorseyType
> A morse code typing game in the style of monkeytype made in unity.

![Start Screen](https://github.com/theme222/HorseyType/blob/master/Images/Screenshot%20from%202025-02-21%2015-56-21.png?raw=true)

## About

`Unity version 2022.3.18f1` <br>
`Quotes from` [thequoteshub](https://thequoteshub.com/api/) (This does contain explicit quotes from horny old bastards weirdly enough)<br>

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

![Settings Screen](https://github.com/theme222/HorseyType/blob/master/Images/Screenshot%20from%202025-02-21%2015-58-38.png?raw=true)

These can be configured in game and currently do not save.

### Modes
There are currently three modes to input morse code.
- **Manual Input** : `manual`
Holding down the primary or secondary key will let you type in morse code similar to a straight key (The old timey one). 
Make sure to adjust the dash threshold to the speed you are comforatable in inputting.
- **Semi Automatic Input** : `semi`
The primary key will input a dot (.) while the secondary key will input a dash (-) once you press it. 
Holding it down will not do anything afterwords.
- **Automatic Input** : `auto`
Holding down the keys will input a string of dots (.) and dashes (-) depending on which key you are holding.
It is designed to mimic an *Iambic Paddle Keyer mode A* (Although I don't have access to one in real life so I'm imitating based on videos of it)
Input timings is based on the dash threshold.

### Dash Threshold
The time in milliseconds set for the dash (-). This is used for being the threshold for dots (.) and dashes (-) in manual input and also being the bases for standard time in semi and automatic inputs.
> **Standard units of time for morse code** <br>
> `1 unit` : `dot (.)` <br>
> `3 units` : `dash (-)` <br>
> `1 unit` : `time between dots and dashes` <br>
> `3 units` : `time between characters` <br>
> `7 units` : `time between words` <br>
> `semi mode` uses the dot and dash <br>
> `auto`  uses the dot, dash and the time between dots and dashes <br>

### Primary Key
The primary key is used for the main keying for manual input and used for dots (.) in semi and auto modes.

### Secondary Key
The secondary key is used for dashes (-) in semi and auto modes while it has the same function as the primary key in manual mode.

## Gameplay

![Gameplay](https://github.com/theme222/HorseyType/blob/master/Images/Screenshot%20from%202025-02-21%2015-58-19.png?raw=true)

Type the correct morse code based on the characters on the screen. If the combination was incorrect the keyed morse will clear. There is no WPM  (words per minute) or CPM (characters per minute) counter and finishing a qoute will reset the counter and display a new qoute.