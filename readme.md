SQL query:
```sql
CREATE TABLE Ogrenci(
    ID INT(10) PRIMARY KEY,
    Isim VARCHAR(100) NOT NULL,
    Soyisim VARCHAR(100),
    OdevID INT(10),
    CONSTRAINT FK_OdevId
		FOREIGN KEY (OdevID) REFERENCES Odev(ID)
);

CREATE TABLE Odev(
    ID INT(10) PRIMARY KEY,
    Baslik VARCHAR(100),
    Icerik VARCHAR(1000),
    Baslangic DATE,
    Bitis DATE
);
```

db-first command:

```
Scaffold-DbContext "connection-string" MySql.EntityFrameworkCore -OutputDir Models -Tables odev,ogrenci -f
```