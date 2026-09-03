@echo off

REM 1. Enviar uma nova branch, pela primeira vez
set new_branch=%1

git push --set-upstream origin %new_branch%
