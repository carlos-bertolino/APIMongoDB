@echo off

REM 1. Cria uma nova-branch
set new_branch=%1

git checkout -b %new_branch%
