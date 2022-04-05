# Random photos

## Opis

Aplikacja typu JSON API do pobierania losowych zdjęć z serwisu internetowego Reddit. API posiada 2 endpointy:
1. '/random' - pobiera losowy obrazek z Reddita dla zadanego subreddita
2. '/history' - pobiera historię wylosowanych obrazków.

Subreddit jest konfigurowalny i znajduje się w pliku appsettings.json.
Zdjęcia zwracane są w postaci JSON:
{
"url": "https://photo.jpg",
"downloadDate": "2022-04-03T23:50:55.854025"
},

## Uruchomienie

W celu uruchomienia aplikacji najpierw należy stworzyć i połączyć się z bazą danych.
Na systemie powinny być zainstalowane narzędzie Entity Framework Core.
Na systemie powinien istnieć również serwer.
W pliku appsettings.json trzeba podać poprawny DBConnection string.
Następnie wykorzystująca narzędzia EF Core należy uaktualnić bazę danych.
'dotnet ef database update' (https://docs.microsoft.com/pl-pl/ef/core/cli/dotnet?fbclid)
Po stworzeniu bazy danych można uruchomić aplikację.

## Autor

Wojciech Podmokły (https://github.com/ozel981)
