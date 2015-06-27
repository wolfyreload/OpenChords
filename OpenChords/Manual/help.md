Overview
========

OpenChords is a song chord chart management and presentation tool for musicians. 
It allows the user to present songs with guitar chords to a computer screen, 
so printing pages and pages of guitar chords is a thing of the past. 
Its opensource, easy to use, and has a clutter-free interface.

Background
==========

After discovering a great opensource application called OpenSong, for
presenting song lyrics (and due to my distinct dislike of having to file
my music after every practice and Sunday evening service), I was
inspired to make something similar for guitarists.

I use OpenChords when I play in church and also when I do recording.
I've been developing OpenChords in my free time for for the past three
years and finally decided to share it with the rest of the world. I hope
you enjoy using it as much as I enjoyed making it.

If you wish to contact me for suggestions or to report bugs, you can
email me on <open.chords.app@gmail.com>

Editor Screen
=============

![](HelpImages/EditorScreen.png)
**1** - Your list of songs

**2** - Song filter (enter song name to filter in real time click the
search button to search for words in the song itself)

**3** - Name of the set (typically the name of the person who chose the
songs)

**4** - The songs in the set

**5** - Information on the song itself

> Title: song title
>
> Order: the order in which to display each piece of the song
>
> Author: the author
>
> Reference: any form of code for the song e.g. SOF2123

**6** - The lyrics and chords panel

"." (dot) character = a chord line

" " (space) character = a lyrics line

"\[x\]" = name of the verse

**7** - The notes panel to remind yourself how the song goes

**8** - Buttons for transposing the song

**9** - Present the song on the screen in the order that you have
selected

**10** - Present the whole set of songs

Song List
---------

This is the list of songs that you'll be managing.

To add a new song to the collection, select File &gt;&gt; New and create
your song. Note that you must give the song a title else it will not
save.

To save a song to the collection, select File &gt;&gt; Save (or ctrl+s).

To delete a song from the collection, first select the file that you
want to delete then select File &gt;&gt; Delete.

If you double click (or press enter) while any song is selected it adds
that song to the currently selected set.

If you press the Del key it will remove the last instance of the the
currently selected song (if present in the set)

**Tip1**: If you want to search for a file, just start typing the name
of the song, and the list will be filtered.

**Tip2**: If you want to get to the song in the file system just click
File &gt;&gt; Explore and it will open Windows Explorer with that song
selected.

**Tip3**: If you want to present a single song, select the song and
click on the present button.

Song Filter 
------------

There are 2 ways that you can use this field. 1) You can filter out the
song names by just typing in the names. 2) You can click the search
button (or press enter) to search within the song titles, authors and
lyrics of the songs for matching terms.

**Tip1**: Press the UP or Down keyboard keys to get back to the song
list once you are done filtering and have found the song you want to add
to your set or the song you wish to present.

**Tip2:** You can use quotes in your search if you want to search for a
specific song e.g. "My life is in You Lord" or even the author of a
specific song.

Set Name
--------

This is just the name for a list of songs. If you want to add more, just
type in the name of a new set that you would like to add and hit enter.
You can remove sets by simply, selecting the set in the list and hitting
the Del button on the keyboard.

Songs in the Set 
-----------------

This is just a list of songs in the currently selected set that you wish
to present. If you want to move a song up or down in the list, select it
and use the arrows next to the sets list. To delete a song, select it
and press the "Delete" button on your keyboard.

Finally when you ready to present your set, click on the "present set"
button.

Song Information
----------------

Title: this is just the name of your song.

Order: This is the order in which you would like the pieces of the song
to appear

e.g. V1 C V2 C2 E will display Verse 1, Chorus, Verse 2, Chorus 2,
Ending

There are a number of letters used for pieces of the song

-   I = Intro or interlude

-   C = Chorus

-   P = Pre-Chorus

-   B = Bridge

-   V = Verse

-   E = Ending

> You can use any other letters and number combinations but only the
> ones above will get converted to more user friendly names when you
> choose to present the song(s)

Chords and Lyrics
-----------------

In order to display the chords and lyrics and sections properly we use a
simple syntax

A letter with or without a number in square brackets like \[C\] or
\[V1\] tells us that there is a new piece of the song

A dot (".") tells us that that line is a line with chords

A space (or in fact any other character but rather use a space) tells us
that that line is for lyrics

Notes
-----

These notes are to remind you how to perform the song while the song is
being presented in full screen view.

The notes need to be in the same order as you have chosen your song
order. Also the \[G\] tag is a just a general tag where you put some
general notes for the song information for the song (i.e. its time
signature or a pre-set that you use for your pedal for that song.)

Song Key
--------

Here you enter in the Key for the song. If you wish to transpose the
song (increase or decrease the key) press the up and down buttons next
to the key field. The up button will increase the key of the song by a
semi tone and the down button will decrease the key by a semi tone.

Sometimes you want to use a Capo when you playing. Use the up button to
increase the fret in which you will place the capo and the down button
decrease the fret in which you will place your capo.

The Settings Screens
====================

General Settings
----------------

**Note**: you can use both relative and absolute paths.

![](HelpImages/FileAndFolderSettings.png)

**Check for updates on start -** check for updates when OpenChords
starts

**Check now** - checks for updates manually

**Portable Mode -** if this is checked, it will allow you to set the
folder in which all the settings for OpenChords is stored. It's useful
to disable portable mode if you have more than one person using the
application at the same time, each with different preferences to how
they want the song displayed (or the tablet settings for their personal
tablet computer).

**Settings Folder** - this is the folder where OpenChords stores its
Display and print settings, as well as information on the selected set
and song from the previous use of the application.

**Application Data Folder** - this is the folder where OpenChords keeps
its Songs, Sets and Notes for each song.

**File Sync Tool** - if you use a utility like "Free File Sync" you can
make a link to it which you can then run in the addons &gt;&gt; File
Sync Utility.

**OpenSong Executable** - The path and executable for OpenSong

**Opensong Songs and Sets Folder** - The path that OpenSong stores its
Songs, Sets, Backgrounds and settings.

**Welcome Slide Name** - this is only if you have a specific first slide
for every OpenSong presentation. Just make a copy of the song slide and
place it in the OpenChords application data folder and give it the same
name as you've entered in this field.

**Use Welcome Slide** - check the checkbox if you want to use the
welcome slide

The following screen is used to tweak how your song will display/print

![](HelpImages/DisplaySettings.png)

**Screen width and height** - this is detected automatically but you can
set it manually if you wish.

**Dual column** - this checkbox tells the program whether you want have
1 or 2 columns on the screen at once when displaying the song

**Show notes** - this checkbox tells the program whether you want to
display the notes or not when you presenting the song.

**Show chords** - this checkbox tells the program whether you want to
display the chords or not when you presenting the song.

**Show lyrics** - this checkbox tells the program whether you want to
display the lyrics or not when you presenting the song.

You also can choose the Font, Size, Color and Font Style for almost all
the elements that are displayed or printed

**Reset button** - resets all settings and attempts to make the settings
work for whatever screen resolution you are using.

Song presentation
=================

![](HelpImages/SongDisplay.png)

**1** - The title of the currently displayed song

**2** - The order of the song. The parts in a different color are the
parts that are currently displayed on the screen

**3** - The name of a piece of the song. In this case the name of the
piece is Verse 3

**4** - The chords and lyrics of the song

**5** - The notes for the piece of the song

**6** - PTO = please turn over (indicates that there is more of the song
on pages that follow)

**Tip**: you will need to press the Escape key to leave the
presentation.

Hotkeys 
========

There are a couple of shortcuts in OpenChords that will hopefully make
your life a little easier.

 Editor shortcuts
-----------------
  Shortcut        Description
  ---------       -----------------------------------------------
  Ctrl+N          Creates a new blank song
  Ctrl+S          Saves the selected song to disk
  Ctrl+F          Attempts to fix the song formatting.
  Ctrl+O          Exports the currently selected set to OpenSong
  Ctrl+P          Exports and opens pdf of the song
  Ctrl+R          Selects a random song to the set
  F5              Refreshes the song list and set list
  F11             Presents the selected song
  F12             Presents all songs in the set
  Shift+F12       Exports current set to Tablet Pdf

Presentation shortcuts
----------------------
  Shortcut        Description
  ---------       -----------------------------------------------
  Ctrl+Left       Go to previous song                              
  Ctrl+Right      Go to next song                                  
  1..9            Goes to the song with index number 1..9          
  Ctrl+N          toggle song notes                                
  Alt+L           List of all songs in the set                     
  Ctrl+O          Decreases font size                              
  Ctrl+P          Increases font size                              
  Ctrl+0          Increases song key                               
  Crtl+9          Decreases song key                               
  Ctrl+8          Increases capo                                
  Crtl+7          Decreases capo                                
  Escape          Closes the song presentation window           
  Ctrl+M          Starts/Stops the metronome    
  
Appendix
========
Tips and tricks
---------------
### The fix formatting button

This function attempts fix all the formatting in your song and writes a
template to your notes panel following the order of the song that you
chose in order.

### Overlapping lyrics

If you find that the lyrics or chords of the song overlap while you
presenting the song you have a few things that you can do to try and
make things look a little better:

1.  You can press Ctrl+D to only display one page of the song. This
    helps a lot with netbooks and other low resolution devices.

2.  You can play with the + and - buttons on the keyboard to change the
    font size of all of the elements of the song

3.  You can go into display settings and play with the content size.

4.  You can split up the lines of your song so they don't overlap with
    the second panel. One way to do this quickly is select just before
    the word (in the lyrics line) that you want to split on and press
    the "Break/Pause" key on the keyboard to split the line and the
    chords above to the next line.

### Syncing OpenChords your songs with the rest of your music group

If you find yourself in the position that your entire music team wants
to use OpenChords, keeping all the songs in sync can be a bit of a
mission. I've been using Dropbox for a while now to solve this problem.
You simply move the OpenChords folder onto your Dropbox and run
OpenChords from your Dropbox. This way you can make sure your whole team
has the same notes and chords for each song as well as the latest
version of OpenChords.

Just be careful of the OpenChordsSettings folder. It's preferable to not
sync this folder since it holds your personalized settings, lucky
Dropbox has a "Selective Sync" feature.

Another issue is conflicts... Sadly someone will have to periodically go
through the OpenChord's data folder and remove conflicting files.

### Multimedia

You'll sometimes find that you can't remember how a song goes. To help
remember you'll often need to listen to an mp3, or video of the song. If
you place a multimedia file in the OpenChordsSettings\\media folder and
you give that file the exact same name as your song title a "Play"
button will appear on the form to allow you to easily play the song in
the default player for your system.

Feel free to use any of the following formats: mp3, mp4, avi, ogg, flv,
and mkv, provided that your system has the ability to play these
formats.
