@echo off
cls
echo This script reverts all changes and removes all ignored filed from the project
echo Close this script if you don't want to do this
PAUSE
git reset --hard HEAD
git clean -d -x -f