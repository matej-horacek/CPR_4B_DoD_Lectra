# Jak vyvíjet feature

1. Aktualizuj `dev` a vytvoř větev:
   ```bash
   git switch dev
   git pull
   git switch -c feat/co-delate
   ```
2. Napiš feature a commitni:
   ```bash
   git add .
   git commit -m "feat: popis změny"
   ```
3. Pushni větev:
   ```bash
   git push -u origin feat/co-delate
   ```
4. Vytvoř pull request na GitHubu do `dev`.
5. Počkej, až doběhne CI (a review).
6. Mergni do `dev`, smaž větev a aktualizuj si lokální `dev`:
   ```bash
   git switch dev
   git pull
   git branch -d feat/co-delate
   ```
