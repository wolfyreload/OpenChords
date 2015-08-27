Running OpenChords in Linux
=============================================

OpenChords now runs in linux and it requires the mono framework

To run OpenChords you'll need to first run the following (for Ubuntu based distros):

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
# script to install mono
sudo apt-get update
sudo apt-get install mono-complete
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Then to run OpenChords you'll need to run:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
# script to run OpenChords
cd {pathToOpenChordsAppFolder}
mono OpenChords.Linux.Gtk2.exe
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

I'm in the process of figuring out how to make deb packages to make installation easier but this have have to do for now. Not sure if/what other distros it will work in.



