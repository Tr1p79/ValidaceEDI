# Aplikace pro zpracování EDI XML zprávy

Tato aplikace v .NET 8.0 slouží ke zpracování a validaci EDI zpráv ve formátu XML. Aplikace validuje XML proti předdefinovanému XSD schématu a ukládá data do databáze SQL Server pomocí Entity Framework Core.

## Požadavky

- .NET 8.0 SDK
- SQL Server (Express nebo plná verze)
- Entity Framework Core 8.0 nebo novější
- Platný XML soubor, který odpovídá XSD schématu

## Nastavení

### 1. Konfigurace
- nastavit správný 'ConnectionString' v 'EDIContext.cs' 'řádek 16.' (v komentářích jsou popsané vztahy mezi tabulkami)
- nastavit správné cesty k XML souboru a XSD schematu v 'Program.cs' 'řádek 12. a 13.'(v repozitáři nechávám testovací schéma 'Objednavka.xsd' a XML soubor 'test.xml')
- v případě že si chcete vyzkoušet své vlastní XSD schéma a XML tak poprosím zkontrolovat definovaný Namespace a přepsat ho do ''test'' v 'Program.cs' 'řádek 35 a 47'


### 2. Použití aplikace
- aplikujte migrace: pžíkazem 'dotnet ef database update' (InitialCreate je součástí repozitáře)
- spusťě aplikaci: příkazem 'dotnet run'
