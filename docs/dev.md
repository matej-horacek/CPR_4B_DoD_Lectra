# Vývoj

## První spuštění

Potřebuješ [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) a Docker.

1. Naklonuj repozitář a přepni se na `dev`:
   ```bash
   git clone git@github.com:matej-horacek/CPR_4B_DoD_Lectora.git
   cd CPR_4B_DoD_Lectora
   git switch dev
   ```
2. Vytvoř si lokální `.env` (necommituje se):
   ```bash
   cp .env.example .env
   ```
3. Spusť databázi:
   ```bash
   docker compose up -d
   ```
4. Spusť API:
   ```bash
   dotnet run --project api
   ```
   Swagger běží na <http://localhost:5125/swagger>.

## Pojmenování větví

Větev se jmenuje `typ/kratky-popis` – malá písmena, bez diakritiky, slova oddělená pomlčkou.

| Prefix      | Kdy ho použít                              | Příklad                  |
| ----------- | ------------------------------------------ | ------------------------ |
| `feat/`     | nová funkce                                | `feat/prihlaseni`        |
| `fix/`      | oprava chyby                               | `fix/validace-emailu`    |
| `docs/`     | jen dokumentace                            | `docs/navod-na-spusteni` |
| `refactor/` | úprava kódu beze změny chování             | `refactor/user-service`  |
| `chore/`    | nastavení projektu, CI, závislosti         | `chore/ci-workflow`      |

Větve `main` a `dev` jsou stálé, nepushuje se do nich napřímo.

## Jak vyvíjet feature

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
