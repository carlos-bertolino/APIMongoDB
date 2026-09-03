@echo off

REM 1. Comentario
set comment=%1

git add .
git commit -m "%comment%"