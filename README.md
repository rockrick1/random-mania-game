# Random Mania Game
A rhythm game developed on an MVC architecture using Unity2D and C#. Very much inspired by other rhythm games like osu! and Guitar Hero.

# How to play
Notes will fall from one of three lanes. Select which lane to hit using the **left and right arrow keys**. Then, tap falling notes to the beat using **Z** and **X**.

# Sample screens
### Main menu
![](https://i.imgur.com/LFBMuNM.png)
### Song select
![](https://i.imgur.com/q6YP6GI.jpeg)
### In game
![](https://i.imgur.com/Zst1BA9.jpeg)
### Results
![](https://i.imgur.com/LBY2cFG.jpeg)
### Settings
![](https://i.imgur.com/8xOaPZK.png)

# Editor
![](https://i.imgur.com/n6nQj8T.png)
Every good rhythm game has to have one... create songs from scratch or edit existing song to your liking! Configurable attributes are:

### BPM
Set the beats per minute of the song. I recommend you use an online BPM tapper to find your songs BPM, but a built in tapper is planned.

### Approach Rate
Time in seconds it takes for a note to fall from the top of the screen to the hitter position.

### Difficulty
The time window you have to hit a perfect note, 0 is really easy, 10 is really hard.

### Starting Time
The starting time, in seconds, of the song. This is useful to determine the offset of the BPM in the song file. Scroll to the first beat of the song, more easily done using the sound wave found in the top of the screen, and click **Set current time** to fill is with the current scrolled time.

**_Don't forget to save your settings!_**

### Other utilities
**Time signature** can be configured to change how many separators you are using. Notes will always snap to the closest separator when placing them.

**Playback speed** can also be changed. Useful for understanding better where you should put some notes.

**Show sound wave** can be toggled to show and hide the sound wave, making the editor a little less laggy.

# Disclaimer
I used songs and background images from artists whose art I do not own. This project has no monetary purposes, so all credit goes to the original artists.

# Planned features
Some features like bombs, health bar and extra settings are planned.

Complete project planning can be found on the [trello page](https://trello.com/b/5tLFtw77/random-mania-game).
