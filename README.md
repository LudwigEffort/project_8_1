
# Table of Contents

-   [Lecture info](#org4d5e962)
1.  [Project 8.1](#org4addf0d)
    1.  [UML](#org0ca5115)
    2.  [Developing](#org6f84532)
        1.  [Ordine Sviluppo Web API](#orgb7ca3d2)
        2.  [Login Web API](#org786bb14)
        3.  [Lab Manager Web API](#org6dfff57)
    3.  [Tasks List](#org3bfaef6)
        1.  [General](#orga09510f)
        2.  [Login](#org0e61f5c)
        3.  [Lab](#orgc638b64)
        4.  [Comunizazione tra API](#orgdff7372)
        5.  [Frontend](#orgddee348)
        6.  [Tests](#orge1e1b1d)



<a id="org4d5e962"></a>

# Lecture info

-   Date: <span class="timestamp-wrapper"><span class="timestamp">[2023-11-07 Tue]</span></span>


<a id="org4addf0d"></a>

# Project 8.1

Per questo progetto ho decisio di trattare le due applicazioni come due microservizi indipendenti, con dei database **SQLite** dedicati.
Quindi avremo due Web API:

-   **Login Web API**, è quell&rsquo;API che si occupa di gestire i **sign up** e **login** degli utenti, avrá un **admin** che gestisce la lista degli utenti registrati.
-   **Lab Manager Web API**. è l&rsquo;API che si occupa di gestire il laboratorio, anche qui avremo un admin che gestisce le risorse dell&rsquo;API.

Le due API dovranno comunicare tra di loro, il **Login Web API** deve registrare l&rsquo;user dentro **Lab Manager Web API** se l&rsquo;user non è gia nel db del laboratorio, il **Login** deve autenticare l&rsquo;accesso al **Lab** e di conseguenza sbloccare le operazioni degli user loggati.
Entrambi le **API** sono state pensate come **Model First**, in ottica di integrazione con i database.
Qundi per ogni API ho definito i **Model** con le loro relazioni, cosí poi l&rsquo;**Entity Framework** si occupa di generare il db, con tutte le relazioni tra le tabelle.
Sto utilizzando il **Repository pattern** per nascondere il codice che si occupa di accedere ai dati del db, in questo modo se in futuro voglio cambiare il meccanismo di persistenza dei dati, per esempio utilizzando un file manager, posso farlo senza troppi problemi.


<a id="org0ca5115"></a>

## UML

Ecco l&rsquo;**UML** del progetto:

![img](../docs/img/project_8_1_uml.png "Project 8.1 UML")


<a id="org6f84532"></a>

## Developing


<a id="orgb7ca3d2"></a>

### Ordine Sviluppo Web API

1.  Creazione Model, DbContext e relazioni
2.  Creazione Seed
3.  Prima Migration e Seeding
4.  Repository Pattern
    -   CRUD
5.  Dto & AutoMapper
6.  Controller
    -   HTTP Methods


<a id="org786bb14"></a>

### Login Web API

Con la separazione dei controller, repository pattern e dei dto, possiamo svilluppare la nostra API con differenti regole di accesso, qundi i nostri **User** possono avere differenti ruoli. Nel nostro caso solo due: **client** e **admin**.

1.  User Model & Db

    -   Nel model degli **User** ho utilizzato le **DataAnotation** per dare delle specifiche alle colonne del mio database (come campi obbligatori, regex, ecc).
    -   Per il campo **email** ho voluto renderlo unico (quindi non è possibile creare utenti con la stessa email) grazie alla configurazione del **DbContext** tramite il **Fluent API**.
    -   Ho creato uno script chiamato **Seed** per popolare il db del login.

2.  Controller

    Per gestire le autorizzazioni, sto differenziando i ruoli di client e admin su due controller diversi. Quindi avremo due controller:
    
    -   **UserAdminController**
    -   **UserClientController**
    
    1.  LoginClientService
    
        Il controller **LoginClientService** ha le seguenti funzioni:
        
        -   **Sign up**, l&rsquo;user si puó registrare nel db dell&rsquo;API.
        -   **Login**, l&rsquo;user effetua il login e il controller invia il token di autenticazione.
    
    2.  UserAdminController
    
        In questo controller abbiamo le operazioni che puó effetuare l&rsquo;admin.
        Per il momento ho decisio di non inserire nessun login, successivamente bisogna inserire un login per la parte di admin (quindi eliminare la possibilitá da parte del client di registrarsi come admin).

3.  Repository Pattern

    Ho deciso di separare i **Repository Pattern** per ogno ruolo degli user.

4.  Dto & AutoMapper

    Ho deciso di separare i **Dto** per ogno ruolo degli user.


<a id="org6dfff57"></a>

### Lab Manager Web API

Per il **Lab Manager** ho pensato che la registrazine degli user nel db avviene tramite l&rsquo;**Admin**, manualmente con un form.
Il **client** si identifica con un **token** per sbloccare il controller delle prenotazioni.


<a id="org3bfaef6"></a>

## Tasks List


<a id="orga09510f"></a>

### General

-   [-] Progettazione
    -   [-] Progettazione App
    -   [-] Creazione UML by db first
    -   [X] Validazioni campi database? -> fatto cone le data anotations
-   [-] Requirements
    -   [-] User registration and authentication
    -   [-] User Admin
    -   [ ] Lab management
    -   [ ] User access
    -   [ ] Persistence
    -   [ ] User Interface (Frontend)


<a id="org0e61f5c"></a>

### Login

-   [X] Creazione Model, DbContext e relazioni
-   [X] Creazione Seed
-   [X] Prima Migration e Seeding
-   [X] Repository Pattern & CRUD
    -   [X] Admin
        -   [X] Read
        -   [X] Create
        -   [X] Update
        -   [X] Delete
    -   [X] Client
        -   [X] Read
        -   [X] Create
-   [-] Dto & AutoMapper
-   [-] Controller & HTTP Methods
    -   [-] Admin Controller
        -   [X] GET
        -   [X] POST
        -   [-] PUT
        -   [X] DELETE
    -   [-] Client Controller
        -   [X] POST (Sign up)
        -   [-] POST (Login)


<a id="orgc638b64"></a>

### Lab

-   [X] Creazione Model, DbContext e relazioni
-   [X] Creazione Seed
-   [X] Prima Migration e Seeding
-   [ ] Repository Pattern & CRUD
    -   [ ] Read
    -   [ ] Create
    -   [ ] Update
    -   [ ] Delete
-   [ ] Dto & AutoMapper
-   [ ] Controller & HTTP Methods
    -   [ ] GET
    -   [ ] POST
    -   [ ] PUT
    -   [ ] DELETE

1.  List of Lab CRUD

    -   Admin
        -   [ ] Item & Computer
        -   [ ] Lab User
        -   [ ] Software
        -   [ ] Room
        -   [ ] Reservation (Only Reading)
    -   Client
        -   [ ] Item (Only Reading)
        -   [ ] Reservation


<a id="orgdff7372"></a>

### Comunizazione tra API


<a id="orgddee348"></a>

### Frontend


<a id="orge1e1b1d"></a>

### Tests

