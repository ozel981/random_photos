# Random photos

## Opis

Aplikacji typu JSON API do pobierania losowych zdjęć z serwisu internetowego Reddit. API posiada 2 endpointy:
1. `/random` - pobiera losowy obrazek z Reddita dla zadanego subreddita
2. `/history` - pobiera historię wylosowanych obrazków.
\newline
Subreddit jest konfigurowalny i znajduje się w pliku appsettings.json. <br />
Zdjęcia zwracane są w postaci JSON: <br />
{ <br />
    "url": "https://photo.jpg", <br />
    "downloadDate": "2022-04-03T23:50:55.854025" <br />
},

## Uruchomienie

W celu uruchomienia aplikacji najpierw nalerzy stworzyć i połączyć się z bazą danych. <br />
W pliku appsettings.json trzeba podać poprawny DBConnection string.
Następnie nalerzy uruchomić polecenie Update-Database przy pomocy Package Manager Console.
Kiedy zostanie stworzona baza danych można uruchomić aplikację.

## Autor

Wojciech Podmokły (https://github.com/ozel981)
