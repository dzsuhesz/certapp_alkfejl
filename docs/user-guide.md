# X.509 Certificate Store – Felhasználói útmutató

## Tartalomjegyzék
1. [Az alkalmazásról](#az-alkalmazásról)
2. [Rendszerkövetelmények](#rendszerkövetelmények)
3. [Indítás](#indítás)
4. [Funkciók](#funkciók)
5. [API dokumentáció](#api-dokumentáció)

## Az alkalmazás

Az X.509 Certificate Store egy webalkalmazás, amely lehetővé teszi X.509 digitális tanúsítványok kezelését. Az alkalmazás két fő funkciót kínál:

- **Root tanúsítványok** kezelése (létrehozás, listázás, törlés)
- **Felhasználói tanúsítványok** kezelése (aláírás CSR alapján, listázás, törlés)

### Technológiák
- **Frontend:** Angular 21 (TypeScript)
- **Backend:** ASP.NET 10 (C#)
- **Adatbázis:** MongoDB 8

## Rendszerkövetelmények

- Docker Desktop
- (Fejlesztéshez) Node.js 20+, .NET 10 SDK, Angular CLI

## Indítás

### Docker Compose segítségével (ajánlott)

```bash
docker compose up -d
```

Az alkalmazás elérhető lesz:
- **Frontend:** http://localhost:4200
- **Backend API:** http://localhost:5000
- **Swagger UI:** http://localhost:5000/swagger

### Leállítás

```bash
docker compose down
```

## Funkciók

### 1. Root Tanúsítványok

A főoldalon (`/root-certificates`) a root tanúsítványok listája jelenik meg.

#### Új root tanúsítvány létrehozása
1. Kattints az **+ Új tanúsítvány** gombra
2. Töltsd ki a mezőket:
   - **Common Name** – pl. `My Root CA`
   - **Organization** – pl. `My Org`
   - **Country** – 2 betűs országkód, pl. `HU`
   - **Lejárat dátuma** – a tanúsítvány érvényességének vége
   - **PEM Data** – a tanúsítvány tartalma PEM formátumban
3. Kattints a **Létrehozás** gombra

#### Root tanúsítvány törlése
1. Keresd meg a törölni kívánt tanúsítványt a listában
2. Kattints a **Törlés** gombra
3. Erősítsd meg a törlést

#### Lapozás (Pagination)
- Az oldal alján található **← Előző** és **Következő →** gombokkal lehet lapozni
- Egyszerre 5 tanúsítvány jelenik meg

---

### 2. Felhasználói Tanúsítványok

A `/user-certificates` oldalon a felhasználói tanúsítványok listája jelenik meg.

#### Új felhasználói tanúsítvány létrehozása (CSR alapján)
1. Kattints az **+ Új tanúsítvány** gombra
2. Töltsd ki a mezőket:
   - **Common Name** – pl. `John Doe`
   - **Email** – pl. `john@example.com`
   - **Root Certificate ID** – annak a root tanúsítványnak a MongoDB ID-ja, amellyel alá szeretnéd írni
   - **Lejárat dátuma** – a tanúsítvány érvényességének vége
   - **CSR Data** – a tanúsítvány aláírási kérelem (Certificate Signing Request)
   - **PEM Data** – az aláírt tanúsítvány PEM formátumban
3. Kattints a **Létrehozás** gombra

#### Felhasználói tanúsítvány törlése
1. Keresd meg a törölni kívánt tanúsítványt a listában
2. Kattints a **Törlés** gombra
3. Erősítsd meg a törlést


## API dokumentáció

A Swagger UI elérhető fejlesztői módban: **http://localhost:5000/swagger**

### Root Certificates végpontok

| GET | `/api/rootcertificates` | Összes root cert lekérése (pagination)
| GET | `/api/rootcertificates/{id}` | Root cert lekérése ID alapján
| POST | `/api/rootcertificates` | Új root cert létrehozása
| DELETE | `/api/rootcertificates/{id}` | Root cert törlése

### User Certificates végpontok

| GET | `/api/usercertificates` | Összes user cert lekérése (pagination) 
| GET | `/api/usercertificates/{id}` | User cert lekérése ID alapján 
| POST | `/api/usercertificates` | Új user cert létrehozása 
| DELETE | `/api/usercertificates/{id}` | User cert törlése 