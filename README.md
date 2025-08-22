# ProduPenguin
❄️🐧 a penguin that sits on your desktop and helps you be productive 🐧❄️

## Planned Features
<li> a penguin that sits in the corner of your screen and works/studies with you </li>
<li> built in work session planning (ie pomodoro technique for studying) </li>
<li> two idle activities for work/study blocks, ⛏️🧊 and  📚☕ </li>
<li> two idle activities for rest blocks, 🪩 and 🏀  </li>
<li> unlockable cosmetics from completing activity blocks </li>

## Features I'm thinking about
<li> igloo  </li>
<li> goal setting + reminders </li>

## Devlog
### day[#] ###

**idea stuffs** <br>
ideas for the day

<br> **dev stuffs** <br>
<li> completed ✅ </li>
<li>in progress 🔨  </li>
<li> planned 📘 </li>
 
<br> **observations** <br>

### day 0 
**idea stuffs** <br>
<li> i miss club penguin.</li>
<li>i also like the trend of cozy idle games that live on your browser.</li>
<li>i think it'd be cool to spin one of them into a productivity widget.</li>
<li>bongo cat's input counter feels nice, i think there are some game abstractions that could live on a desktop for productivity. </li>


<br> **dev stuffs** <br>
<li> a basic draggable image within transparent window. ✅ </li>
<li> a pomodoro style timer that causes the image to flash blue after timer goes off.✅</li>
<li>a timer with animated text that appears above the image representing phase of pomodoro.✅</li>

 <br> **observations** <br>
seeing the mining counter go up is decently satsifying, i think an anim of a penguin mining glaciers will be even more satsifying. 

### day 1 
**idea stuffs** <br>
<li>Not much time to work on this today, so i'm just going to add a basic menu</li>
<li>Did get the idea last night to use a render texture to display a small little 3d scene transparently - so that was done</li>

<br> **dev stuffs** <br>
<li>menu button that expands out other button ✅ </li>
<li>buttonView abstract ✅ </li>
<li>pause start button ✅ </li>

 <br> **observations** <br>
none yet.

### day 2 
**idea stuffs** <br>
<li>Work Sessions will contain loops of activities.</li>
<li>Lil mini games while resting would be fun</li>
<li>Not much time today either, smaller tasks</li>

<br> **dev stuffs** <br>
<li>Refactor activities to better fit work session abstraction. ✅</li>
<li>Start all 3 kinds of session from idle state ✅ </li>
<li>End sessions and give a recap of productivity/time spent✅</li>
<li>Pause unpause activities when they are active ✅ </li>

 <br> **observations** <br>
<li>Playing dota and seeing the action count go up was cool -> inspired a recap for sessions. (right now all progress tracking is reset when an activity ends)</li>
<li>Clicking feels weird and i'll want to have a click inside of recap state, want to make a basic watch animation for playing state and a basic mining activity to work session tmrw, will see if i can clean up clicking later tonight.</li>

### day 3

**idea stuffs** <br>
<li>ideas feel pretty set, i'd like to eventually do daily reminders but that will come after setting up "gameplay" logic.  b</li>

<br> **dev stuffs** <br>
<li> clean up render activity logic -> display 3d object with hyper basic anims and a small icon representing state 📘 </li>
<li>create a playing session state where object jumps every few seconds after an input threshold 📘  </li>
<li> create a working session state where object mines and then moves to mine next node 📘 </li>
 
<br> **observations** <br>
