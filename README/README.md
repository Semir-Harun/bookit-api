# BookingSystem – Oppsett av prosjekt

## Databaseoppsett

Prosjektet bruker MySQL som database. Først ble databasen opprettet lokalt i MySQL.

## Installerte pakker

Følgende NuGet-pakker ble installert i prosjektet:

* Microsoft.EntityFrameworkCore (9.0.0)
* Pomelo.EntityFrameworkCore.MySql (9.0.0)
* Microsoft.EntityFrameworkCore.Design (9.0.0)

## Migrasjoner

Første migrasjon ble opprettet med:

dotnet ef migrations add InitialCreate

Deretter ble databasen oppdatert.

## Roller (Seed Data)

To roller ble lagt til i databasen:

* Admin
* User

Dette ble gjort med:

dotnet ef migrations add SeedRoles
dotnet ef database update

## Videre utvikling

Resten av prosjektet ble utviklet i Visual Studio Code med ASP.NET Core.

## -------------------------------------------------------------------------------------

## Autentisering (JWT) + roller (Admin/User)

Prosjektet bruker JWT Bearer Authentication med rollebasert autorisasjon.

### Roller
- **Admin**: tilgang til admin-endpoints
- **User**: tilgang til user-endpoints

### Test-endpoints (for å verifisere auth)
Disse finnes kun for å demonstrere at JWT og roller fungerer:

- `GET /secure/ping` → Krever innlogging (JWT)
- `GET /secure/admin` → Krever rolle **Admin**
- `GET /secure/user` → Krever rolle **User**

I utvikling finnes også midlertidige debug-endpoints for å hente token:
- `GET /auth-debug/admin` → returnerer admin-token
- `GET /auth-debug/user` → returnerer user-token

## Debug-endpoints fjernes/skrues av når ekte `/auth/login` (DB-basert) er implementert.

---

## Slik tester du auth via PowerShell (anbefalt)

### 1) Start API-et
Kjør i prosjektmappen:
```powershell
dotnet run

## ---------------------------------------------------------------------------------

## 1 Hent Admin-token og test

$token = (Invoke-RestMethod "http://localhost:5074/auth-debug/admin").token

Invoke-RestMethod "http://localhost:5074/secure/ping"  -Headers @{ Authorization = "Bearer $token" }   # forvent: pong
Invoke-RestMethod "http://localhost:5074/secure/admin" -Headers @{ Authorization = "Bearer $token" }   # forvent: admin ok

## 2 Hent User-token og test
$token = (Invoke-RestMethod "http://localhost:5074/auth-debug/user").token

Invoke-RestMethod "http://localhost:5074/secure/user" -Headers @{ Authorization = "Bearer $token" }    # forvent: user ok

## 3 Verifiser rolle-sperre (User skal ikke få Admin)
try {
  Invoke-RestMethod "http://localhost:5074/secure/admin" -Headers @{ Authorization = "Bearer $token" }
} catch {
  $_.Exception.Response.StatusCode.value__
}

--> Forventet statuskode: 403