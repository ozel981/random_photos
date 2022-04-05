# Random photos

## Opis

Aplikacja typu JSON API do pobierania losowych zdjęć z serwisu internetowego Reddit. API posiada 2 endpointy: <br />
1. '/random' - pobiera losowy obrazek z Reddita dla zadanego subreddita
2. '/history' - pobiera historię wylosowanych obrazków.
 <br />
Subreddit jest konfigurowalny i znajduje się w pliku appsettings.json. <br />
Zdjęcia zwracane są w postaci JSON: <br />
{<br />
"url": "https://photo.jpg", <br />
"downloadDate": "2022-04-03T23:50:55.854025" <br />
},

## Uruchomienie

W celu uruchomienia aplikacji najpierw należy stworzyć i połączyć się z bazą danych. <br />
Na systemie powinny być zainstalowane narzędzie Entity Framework Core. <br />
Na systemie powinien istnieć również serwer. <br />
W pliku appsettings.json trzeba podać poprawny DBConnection string. <br />
Następnie wykorzystująca narzędzia EF Core należy uaktualnić bazę danych. <br />
'dotnet ef database update' (https://docs.microsoft.com/pl-pl/ef/core/cli/dotnet?fbclid) <br />
Po stworzeniu bazy danych można uruchomić aplikację. <br />

## Autor

Wojciech Podmokły (https://github.com/ozel981)
