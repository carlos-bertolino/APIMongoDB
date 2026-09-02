@echo off

REM 1. Name of Project/Folder
set project=%1

echo "Subindo arquivos do projeto: %project%" 

REM 2. Identify 
git config --global user.email "crgberto@gmail.com"
git config --global user.name "Carlos Bertolino"
  
REM 3. Turn your folder into a Git repository
git init

REM [NOVO] Garante que a pasta nao esta vazia criando um arquivo temporario se necessario
echo # %project% > README.md

REM 4. Stage all your project files
git add .

REM 5. Commit the files locally
git commit -m "Initial commit"

REM 6. Rename your default branch to 'main'
git branch -M main

REM [CORREÇÃO] Remove a rota antiga antes de adicionar a nova para evitar conflitos
git remote remove origin 2>nul

REM 7. Link your local project to GitHub (Usando a URL correta com hifen)
set "url=https://github.com/carlos-bertolino/%project%"
git remote add origin %url%

REM 8. Upload your project files
git push -u origin main
