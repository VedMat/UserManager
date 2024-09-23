userManager
Descrizione
userManager è un'applicazione basata su .NET 8 per la gestione di utenti e risorse, con un sistema di ruoli che permette ad amministratori, manager e clienti di gestire risorse e account in maniera sicura e strutturata. Il progetto include un sistema di autenticazione e gestione delle risorse, test automatizzati con xUnit e un database SQL Server gestito tramite migrations.

Ruoli e Permessi:
Admin: Può creare Manager e vedere tutte le risorse.
Manager: Può creare Clienti e vedere solo le risorse dei clienti creati da lui.
Client: Può creare risorse e vedere solo le proprie risorse.
Requisiti
.NET 8 SDK
SQL Server
xUnit (per i test)
Installazione
Clona il repository:

bash
Copy code
git clone https://github.com/tuo-repo/userManager.git
cd userManager
Configura il database:

Assicurati che SQL Server sia in esecuzione.
Configura la stringa di connessione nel file appsettings.json:
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=userManagerDb;User Id=yourUsername;Password=yourPassword;"
}
Esegui le migrazioni per configurare il database:

bash
Copy code
dotnet ef database update
Avvia l'applicazione:

bash
Copy code
dotnet run
Al primo avvio, verrà creato un utente Admin con le seguenti credenziali:

Email: Admin@example.com
Password: Admin@123
Utilizzo
Gestione degli Utenti
Gli Admin possono creare e gestire utenti con ruolo di Manager.
I Manager possono creare e gestire utenti con ruolo di Cliente.
I Clienti possono creare risorse e gestire le proprie informazioni.
Gestione delle Risorse
Gli Admin possono visualizzare e gestire tutte le risorse.
I Manager possono visualizzare solo le risorse create dai clienti a loro associati.
I Clienti possono visualizzare e gestire solo le risorse che hanno creato.
Test
Il progetto include test automatici per verificare il funzionamento dei controller chiave:

AccountController: Test per il login e la gestione delle password.
UserController: Test per la creazione di utenti e la gestione del profilo.
ResourceController: Test per la gestione delle risorse.
Per eseguire i test, utilizza il seguente comando:

bash
Copy code
dotnet test
Tecnologie utilizzate
.NET 8
SQL Server (gestito tramite Entity Framework migrations)
xUnit (per i test unitari)
Licenza
Questo progetto è distribuito sotto licenza MIT. Vedi il file LICENSE per i dettagli.

Autori
Nicholas
