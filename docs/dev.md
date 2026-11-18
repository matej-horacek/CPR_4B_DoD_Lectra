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

## Databáze / EF Core

Struktura v `api/Data/`:

| Složka / soubor     | Co tam patří                                                     |
| ------------------- | ---------------------------------------------------------------- |
| `Entities/`         | entity (třídy = tabulky)                                         |
| `Configurations/`   | konfigurace entit (`IEntityTypeConfiguration`), načítají se samy |
| `Migrations/`       | generované migrace – ručně needitovat                            |
| `AppDbContext.cs`   | `DbSet` pro každou entitu                                        |

`ExampleItem` je jen ukázka – smaž ji (entitu, konfiguraci, `DbSet`) i s migrací, až přibude první reálná entita.

### Přidání nové entity

1. Entita v `api/Data/Entities/Lecture.cs`:
   ```csharp
   namespace api.Data.Entities;

   public class Lecture
   {
       public int Id { get; set; }
       public required string Title { get; set; }
   }
   ```
2. Konfigurace v `api/Data/Configurations/LectureConfiguration.cs`:
   ```csharp
   using api.Data.Entities;
   using Microsoft.EntityFrameworkCore;
   using Microsoft.EntityFrameworkCore.Metadata.Builders;

   namespace api.Data.Configurations;

   public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
   {
       public void Configure(EntityTypeBuilder<Lecture> builder)
       {
           builder.ToTable("lectures");
           builder.HasKey(e => e.Id);
           builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
       }
   }
   ```
3. `DbSet` v `AppDbContext`:
   ```csharp
   public DbSet<Lecture> Lectures => Set<Lecture>();
   ```
4. Vytvoř migraci (z kořene repa):
   ```bash
   dotnet tool restore   # jen poprvé, nainstaluje dotnet-ef
   dotnet ef migrations add AddLecture --project api --output-dir Data/Migrations
   ```
5. Migrace se aplikují automaticky při startu API (`Database.Migrate()` v `Program.cs`).
