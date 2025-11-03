# GitHub Setup Guide - ValheimClock

## Steg 1: Skapa GitHub-konto (om du inte har ett)
1. Gå till https://github.com
2. Klicka "Sign up"
3. Följ registreringsprocessen
4. Bekräfta din email

## Steg 2: Skapa nytt repository
1. Gå till https://github.com/new
   ELLER klicka på "+" uppe till höger → "New repository"

2. Fyll i:
   - Repository name: **ValheimClock**
   - Description: **A simple analog clock for Valheim showing in-game time**
   - Välj: **Public** (så andra kan se det)
   - **VIKTIGT**: Markera INTE dessa:
     ☐ Add a README file
     ☐ Add .gitignore
     ☐ Choose a license
   
   (Vi har redan dessa filer lokalt!)

3. Klicka **"Create repository"**

## Steg 3: Koppla och ladda upp
När repositoryt är skapat visar GitHub instruktioner. Ignorera dem och kör DESSA kommandon istället:

```powershell
cd C:\Projects\valheimplugin
git remote add origin https://github.com/Killerbob/ValheimClock.git
git branch -M main
git push -u origin main
```

## Steg 4: Verifiera
Gå till https://github.com/Killerbob/ValheimClock
Du ska se alla dina filer!

## Vanliga problem:

**Problem: "remote origin already exists"**
Lösning:
```powershell
git remote remove origin
git remote add origin https://github.com/Killerbob/ValheimClock.git
```

**Problem: "Authentication failed"**
Lösning: Du behöver ett Personal Access Token
1. Gå till GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Generate new token (classic)
3. Välj scope: **repo** (full control)
4. Kopiera token
5. Använd token istället för lösenord när du pushar

**Problem: Användarnamnet är inte Killerbob**
Ersätt "Killerbob" i URL:en med ditt faktiska GitHub-användarnamn

---

## När det är klart:
✅ Din kod finns på GitHub
✅ Andra kan se och ladda ner
✅ Du kan länka till det från Thunderstore och Nexus
✅ Du kan spåra ändringar och ta emot contributions

Lycka till!
