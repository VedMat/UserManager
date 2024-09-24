# **userManager**  

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue.svg) ![SQL Server](https://img.shields.io/badge/SQL%20Server-%3E%3D2017-brightgreen) ![License](https://img.shields.io/badge/License-MIT-green)  

## **Descrizione**  
**userManager** è un'applicazione **C#** basata su **.NET 8**, progettata per gestire utenti e risorse in modo gerarchico. L'applicazione supporta tre livelli di utenti con permessi differenziati: **Admin**, **Manager** e **Client**.  

### **Ruoli e Permessi**:  
- 👑 **Admin**:  
  - Crea Manager.  
  - Visualizza tutte le risorse.  
- 🛠️ **Manager**:  
  - Crea Clienti.  
  - Visualizza solo le risorse create dai propri clienti.  
- 👤 **Client**:  
  - Crea risorse.  
  - Visualizza solo le proprie risorse.  

## **Requisiti**  
- **.NET 8 SDK**  
- **SQL Server** (versione >= 2017)  
- **xUnit** (per i test)  

## **Installazione**  
Segui questi passaggi per configurare e avviare il progetto **userManager**:  

1. **Clona il repository**:  
   ```bash  
   git clone https://github.com/VedMat/UserManager.git  
   cd userManager  
	 
2. **Configura il database**: Modifica il file `appsettings.json` con la tua stringa di connessione:
	```json
	"ConnectionStrings": {  
		"DefaultConnection": "Server=localhost;Database=userManagerDb;User Id=yourUsername;Password=yourPassword;"  
	}

3. **Applica le migrazioni**: Esegui il seguente comando per applicare le migrazioni:
	```bash
	dotnet ef database update

4. **Avvia l'applicazione**: Utilizza il comando seguente per avviare l'applicazione:
	```bash
	dotnet run

5. **Accedi con l'utente amministratore predefinito**:

Email: **Admin@example.com**

Password: **Admin@123**

## **Utilizzo**

**Gestione degli Utenti**:

- 🛠️ **Admin**: Gestione di Manager.

- 👑 **Manager**: Creazione e gestione di Clienti.

- 👤 **Client**: Creazione di risorse.

**Gestione delle Risorse**:

- 👑 **Admin**: Visualizza tutte le risorse.

- 🛠️ **Manager**: Visualizza solo le risorse dei Clienti associati.

- 👤 **Client**: Visualizza solo le proprie risorse.

## **Test**

I test sono eseguiti con **xUnit** e coprono i tre principali controller:

- **AccountController**: Test per autenticazione e recupero password.

- **UserController**: Test per la creazione utenti e gestione profilo.

- **ResourceController**: Test per la gestione delle risorse.

**Esegui i test con**:

	dotnet run 

## **Tecnologie Utilizzate**

- .NET 8

- Entity Framework Core (per le migrazioni e la gestione del database)

- SQL Server

- xUnit (per i test)

  # **App Settings example**
	```json
 	{
	  "ConnectionStrings": {
	    "DefaultConnection": "YOUR_CONNECTION_STRING"
	  },
	  "Jwt": {
	    "Key": "YUOR_SECRET_KEY",
	    "Issuer": "UserManager",
	    "Audience": "UserManager"
	  },
	  "Logging": {
	    "LogLevel": {
	      "Default": "Information",
	      "Microsoft.AspNetCore": "Warning"
	    }
	  },
	  "AllowedHosts": "*"
	}
